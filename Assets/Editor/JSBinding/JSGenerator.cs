using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using cg;
using UnityEditor;
using UnityEngine;

public static class JSGenerator
{
    private static StreamWriter _streamWriter;

    public static Dictionary<Type, Dictionary<string, CsExportedMethodAttribute>> CsExportedMethodDic
    {
        get;
        private set;
    }

    public static void GenerateJsTypeBindings(HashSet<Type> exportTypes, HashSet<Type> exportEnums)
    {
        OnBegin();

        // <typeName,members>
        var allExportInfoDic = new Dictionary<string, List<string>>();

        // classes
        foreach (var type in exportTypes)
        {
            List<string> memberNames;
            GenerateClass(type, out memberNames);
            allExportInfoDic.Add(JSNameMgr.GetJSTypeFullName(type), memberNames);
        }

        foreach (var type in exportEnums)
        {
            GenerateEnum(type);
        }

        OnEnd();

        var sb = new StringBuilder();
        foreach (var item in allExportInfoDic)
        {
            sb.AppendFormat("[{0}]\r\n", item.Key);

            var lst = item.Value;
            foreach (string l in lst)
            {
                sb.AppendFormat("    {0}\r\n", l);
            }
            sb.Append("\r\n");
        }
        File.WriteAllText(JSAnalyzer.GetAllExportedMembersFile(), sb.ToString());
    }

    //[MenuItem("JSB/TestGenerate")]
    //public static void TestGenerate()
    //{
    //    GenerateJsTypeBindings(new HashSet<Type>
    //    {
    //        typeof (PerTest)
    //    });
    //}

    #region Generate Flow

    public static void OnBegin()
    {
        GeneratorHelp.ClearTypeInfo();

        //读取JSBCodeGenSetting下所有CsExportedMethodAttribute属性
        var eportedMethodAtrs = typeof(JSGenerator).Assembly.GetCustomAttributes(typeof(CsExportedMethodAttribute),
            false);
        CsExportedMethodDic = new Dictionary<Type, Dictionary<string, CsExportedMethodAttribute>>();
        foreach (var obj in eportedMethodAtrs)
        {
            var methodAttribute = obj as CsExportedMethodAttribute;
            if (!CsExportedMethodDic.ContainsKey(methodAttribute.TargetType))
            {
                CsExportedMethodDic[methodAttribute.TargetType] = new Dictionary<string, CsExportedMethodAttribute>();
            }
            CsExportedMethodDic[methodAttribute.TargetType].Add(methodAttribute.TargetMethodName, methodAttribute);
        }

        _streamWriter = OpenFile(JSPathSettings.csExportJsFile);
        _streamWriter.Write(@"
if (typeof(JsTypes) == ""undefined"")
    var JsTypes = [];

//this.Enum = {};
");
    }

    public static void OnEnd()
    {
        _streamWriter.Close();
    }

    public static void GenerateEnum(Type type)
    {
        string format = @"var {0} = [[
    fullname: '{1}',
    staticDefinition: [[
{2}
    ]],
    Kind: 'Enum'
]];
JsTypes.push({0});
";
        var sbEnum = new StringBuilder();
        string jsDefName = GetJsTypeDefinition(type);
        var enumDef = new StringBuilder();
        var enumNames = Enum.GetNames(type);
        var enums = Enum.GetValues(type);
        for (int i = 0; i < enums.Length; i++)
        {
            string name = enumNames[i];
            int e = Convert.ToInt32(enums.GetValue(i));
            if (i == enums.Length - 1)
                enumDef.Append("        " + name + ": " + e);
            else
                enumDef.AppendLine("        " + name + ": " + e + ",");
        }

        sbEnum.AppendFormat(format, jsDefName, type.FullName, enumDef);
        HandleStringFormat(sbEnum);

        _streamWriter.Write(sbEnum.ToString());
    }

    public static void GenerateClass(Type type, out List<string> memberNames)
    {
        memberNames = new List<string>();

        GeneratorHelp.ATypeInfo ti;
        int slot = GeneratorHelp.AddTypeInfo(type, out ti);
        string jsDefName = GetJsTypeDefinition(type);

        var sbClass = new StringBuilder();

        var sbDefinition = new StringBuilder();
        var sbStaticDefinition = new StringBuilder();
        BuildConstructors(type, ti.constructors, slot, sbDefinition, memberNames);
        BuildProperties(type, ti.properties, slot, sbDefinition, sbStaticDefinition, memberNames);
        BuildMethods(type, ti.methods, slot, sbDefinition, sbStaticDefinition, memberNames);
        var sbAllDefinition = new StringBuilder();
        if (sbStaticDefinition.Length > 0)
        {
            sbAllDefinition.AppendFormat(@"
    staticDefinition: [[{0}
    ]],", sbStaticDefinition);
        }

        if (sbDefinition.Length > 0)
        {
            sbAllDefinition.AppendFormat(@"
    definition: [[{0}
    ]],", sbDefinition);
        }

        sbClass.AppendFormat(@"
// {0}
var {1} = 
[[
    assemblyName: '{2}',
    fullname: '{3}',
    Kind: '{4}',{5}{6}{7}
]];
jsb_ReplaceOrPushJsType({1});
", type.FullName, jsDefName, type.Assembly.FullName, JSNameMgr.GetJSTypeFullName(type),
            GetJsTypeKind(type),
            GetJsTypeInheritInfo(type),
            BuildFields(type, ti.fields, slot, memberNames),
            sbAllDefinition);
        HandleStringFormat(sbClass);

        _streamWriter.Write(sbClass.ToString());
    }

    public static string BuildFields(Type type, FieldInfo[] fields, int slot, List<string> memberNames)
    {
        if (fields == null || fields.Length == 0)
            return "";

        var sbStaticField = new StringBuilder();
        var sbField = new StringBuilder();

        for (int i = 0; i < fields.Length; i++)
        {
            var field = fields[i];
            memberNames.Add((field.IsStatic ? "Static_" : "") + field.Name);

            if (field.IsStatic)
            {
                sbStaticField.AppendFormat(@"
        {0}: [[
            get: function() [[ return CS.Call({1}, {3}, {4}, true); ]], 
            set: function(v) [[ return CS.Call({2}, {3}, {4}, true, v); ]]
        ]],",
                    field.Name, // [0]
                    (int)JSVCall.Oper.GET_FIELD, // [1]
                    (int)JSVCall.Oper.SET_FIELD, // [2]
                    slot, //[3]
                    i);
            }
            else
            {
                sbField.AppendFormat(@"
        {0}: [[
            get: function() [[ return CS.Call({1}, {3}, {4}, false, this); ]], 
            set: function(v) [[ return CS.Call({2}, {3}, {4}, false, this, v); ]]
        ]],",
                    field.Name, // [0]
                    (int)JSVCall.Oper.GET_FIELD, // [1]
                    (int)JSVCall.Oper.SET_FIELD, // [2]
                    slot, //[3]
                    i);
            }
        }

        var sb = new StringBuilder();
        if (sbStaticField.Length > 0)
        {
            sb.AppendFormat(@"
    staticFields: [[{0}
    ]],", sbStaticField);
        }

        if (sbField.Length > 0)
        {
            sb.AppendFormat(@"
    fields: [[{0}
    ]],", sbField);
        }

        return sb.ToString();
    }

    public static void BuildConstructors(Type type, ConstructorInfo[] constructors, int slot,
        StringBuilder sbDefinition, List<string> memberNames)
    {
        var argActual = new args();
        var argFormal = new args();
        bool overloaded = constructors.Length > 1;

        for (int i = 0; i < constructors.Length; i++)
        {
            var con = constructors[i];
            var ps = con == null ? new ParameterInfo[0] : con.GetParameters();

            argActual.Clear().Add(
                (int)JSVCall.Oper.CONSTRUCTOR, // OP
                slot,
                i, // NOTICE
                "true", // IsStatics                
                "this"
                );

            argFormal.Clear();

            // add T to formal param
            if (type.IsGenericTypeDefinition)
            {
                // TODO check
                int TCount = type.GetGenericArguments().Length;
                for (int j = 0; j < TCount; j++)
                {
                    argFormal.Add("t" + j + "");
                    argActual.Add("t" + j + ".getNativeType()");
                }
            }

            for (int j = 0; j < ps.Length; j++)
            {
                argFormal.Add("a" + j);
                var par = ps[j];
                if (par.ParameterType == typeof(Type))
                {
                    //如果是System.Type类型参数需要传递其FullName回来
                    //在C#层通过JSDataExchangeMgr.GetTypeByName获取其类型对象
                    argActual.Add(string.Format("a{0}.get_FullName()", j));
                }
                else if (par.ParameterType.IsArray && par.ParameterType.GetElementType() == typeof(Type))
                {
                    //如果是System.Type类型数组参数，通过转换获取其类型全名数组
                    argActual.Add(string.Format("jsb_convertTypeParamsArray(a{0})", j));
                }
                else
                {
                    argActual.Add("a" + j);
                }
            }

            string ctorName = GetOverloadedMethodSuffix("ctor", ps, overloaded);
            memberNames.Add(ctorName);

            sbDefinition.AppendFormat(@"
        {0}: function({1}) [[ CS.Call({2}); ]],",
                ctorName,
                argFormal,
                argActual);
        }
    }

    public static void BuildProperties(Type type, PropertyInfo[] properties, int slot,
        StringBuilder sbDefinition, StringBuilder sbStaticDefinition, List<string> memberNames)
    {
        for (int i = 0; i < properties.Length; i++)
        {
            var property = properties[i];
            var accessors = property.GetAccessors();
            bool isStatic = accessors[0].IsStatic;
            string propertyName = GetJsPropertyName(property);
            memberNames.Add((isStatic ? "Static_" : "") + "get_" + propertyName);
            memberNames.Add((isStatic ? "Static_" : "") + "set_" + propertyName);

            //如果是get_Item和set_Item属性需要用到以下参数
            var ps = property.GetIndexParameters();
            string indexerParamA = string.Empty;
            string indexerParamB = string.Empty;
            string indexerParamC = string.Empty;
            for (int j = 0; j < ps.Length; j++)
            {
                indexerParamA += "ind" + j;
                indexerParamB += "ind" + j + ", ";
                if (j < ps.Length - 1) indexerParamA += ", ";
                indexerParamC += ", ind" + j;
            }

            if (isStatic)
            {
                sbStaticDefinition.AppendFormat(@"
        get_{0}: function({6}) [[ return CS.Call({1}, {3}, {4}, true{5}); ]],
        set_{0}: function({7}v) [[ return CS.Call({2}, {3}, {4}, true{5}, v); ]],",
                    propertyName,
                    (int)JSVCall.Oper.GET_PROPERTY,
                    (int)JSVCall.Oper.SET_PROPERTY,
                    slot,
                    i,
                    indexerParamC,
                    indexerParamA,
                    indexerParamB);
            }
            else
            {
                sbDefinition.AppendFormat(@"
        get_{0}: function({6}) [[ return CS.Call({1}, {3}, {4}, false, this{5}); ]],
        set_{0}: function({7}v) [[ return CS.Call({2}, {3}, {4}, false, this{5}, v); ]],",
                    propertyName,
                    (int)JSVCall.Oper.GET_PROPERTY,
                    (int)JSVCall.Oper.SET_PROPERTY,
                    slot,
                    i,
                    indexerParamC,
                    indexerParamA,
                    indexerParamB);
            }
        }
    }

    public static void BuildMethods(Type type, MethodInfo[] methods, int slot,
        StringBuilder sbDefinition, StringBuilder sbStaticDefinition, List<string> memberNames)
    {
        for (int index = 0; index < methods.Length; index++)
        {
            var method = methods[index];
            string methodName = method.Name;
            if (methodName == "ToString")
            {
                methodName = "toString";
            }

            bool isStatic = method.IsStatic;
            bool overloaded = (index > 0 && method.Name == methods[index - 1].Name) ||
                              (index < methods.Length - 1 && method.Name == methods[index + 1].Name);
            if (!overloaded)
            {
                if (GeneratorHelp.MethodIsOverloaded(type, method))
                {
                    overloaded = true;
                    //Debug.Log("$$$ " + type.Name + "." + method.Name + (method.IsStatic ? " true" : " false"));
                }
            }

            var sbFormalParam = new StringBuilder();
            var sbActualParam = new StringBuilder();
            var paramS = method.GetParameters();
            var sbInitGenericParam = new StringBuilder();

            // add T to formal param
            int genericArgsCount = 0; //包含泛型参数个数
            if (method.IsGenericMethodDefinition)
            {
                genericArgsCount = method.GetGenericArguments().Length;
                for (int j = 0; j < genericArgsCount; j++)
                {
                    sbFormalParam.AppendFormat("t{0}", j);
                    if (j < genericArgsCount - 1 || paramS.Length > 0)
                        sbFormalParam.Append(", ");


                    sbInitGenericParam.AppendFormat("\n            var native_t{0} = t{0}.getNativeType();", j);
                    sbActualParam.AppendFormat(", native_t{0}", j);
                }
            }

            string jsMethodName = GetOverloadedMethodSuffix(methodName, paramS, overloaded, genericArgsCount);
            memberNames.Add((isStatic ? "Static_" : "") + jsMethodName);

            //判断该方法是否有定义CsExportedMethodAttribute属性
            CsExportedMethodAttribute csExportedAttr = null;
            if (method.IsDefined(typeof(CsExportedMethodAttribute), false))
            {
                var attributes = method.GetCustomAttributes(typeof(CsExportedMethodAttribute), false);
                csExportedAttr = attributes[0] as CsExportedMethodAttribute;
            }
            if (CsExportedMethodDic != null && CsExportedMethodDic.ContainsKey(type))
            {
                CsExportedMethodDic[type].TryGetValue(method.Name, out csExportedAttr);
            }
            if (csExportedAttr != null)
            {
                string jsCode = csExportedAttr.JsCode;
                if (isStatic)
                {
                    sbStaticDefinition.Append(jsCode);
                }
                else
                {
                    sbDefinition.Append(jsCode);
                }
                continue;
            }

            int paramLength = paramS.Length;
            for (int j = 0; j < paramLength; j++)
            {
                sbFormalParam.AppendFormat("a{0}/*{1}*/{2}", j, paramS[j].ParameterType.Name,
                    j == paramLength - 1 ? "" : ", ");

                var par = paramS[j];
                if (par.ParameterType.IsArray && par.GetCustomAttributes(typeof(ParamArrayAttribute), false).Length > 0)
                {
                    sbActualParam.AppendFormat(", jsb_formatParamsArray({0}, a{0}, arguments)", j);
                }
                else if (par.ParameterType.IsArray && par.ParameterType.GetElementType() == typeof(Type))
                {
                    //如果是System.Type类型数组参数，通过转换获取其类型全名数组
                    sbActualParam.AppendFormat(", jsb_convertTypeParamsArray(a{0})", j);
                }
                else if (par.ParameterType == typeof(Type))
                {
                    //如果是System.Type类型参数需要传递其FullName回来
                    //在C#层通过JSDataExchangeMgr.GetTypeByName获取其类型对象
                    sbActualParam.AppendFormat(", a{0}.get_FullName()", j);
                }
                else
                {
                    sbActualParam.AppendFormat(", a{0}", j);
                }
            }

            if (isStatic)
            {
                sbStaticDefinition.AppendFormat(@"
        {0}: function({1}) [[ {2}
            return CS.Call({3}, {4}, {5}, true{6}); //Ret: {7}
        ]],",
                    jsMethodName,
                    sbFormalParam,
                    sbInitGenericParam,
                    (int)JSVCall.Oper.METHOD,
                    slot,
                    index,
                    sbActualParam,
                    method.ReturnType.Name);
            }
            else
            {
                sbDefinition.AppendFormat(@"
        {0}: function({1}) [[ {2}
            return CS.Call({3}, {4}, {5}, false, this{6}); //Ret: {7}
        ]],",
                    jsMethodName,
                    sbFormalParam,
                    sbInitGenericParam,
                    (int)JSVCall.Oper.METHOD,
                    slot,
                    index,
                    sbActualParam,
                    method.ReturnType.Name);
            }
        }
    }

    public static string GetJsTypeKind(Type type)
    {
        if (type.IsClass)
        {
            return "Class";
        }
        if (type.IsEnum)
        {
            return "Enum";
        }
        if (type.IsValueType)
        {
            return "Struct";
        }
        if (type.IsInterface)
        {
            return "Interface";
        }
        return "Unknown";
    }

    /// <summary>
    ///     获取JsType临时变量名称
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static string GetJsTypeDefinition(Type type)
    {
        string fullName = type.FullName;
        return fullName.Replace('.', '$').Replace('+', '$').Replace('`', '$');
    }

    /// <summary>
    ///     获取当前类型继承关系信息
    /// </summary>
    /// <returns></returns>
    public static StringBuilder GetJsTypeInheritInfo(Type type)
    {
        var sb = new StringBuilder();
        //生成BaseType信息
        string baseTypeName = JSNameMgr.GetJSTypeFullName(type.BaseType);
        if (!string.IsNullOrEmpty(baseTypeName))
        {
            sb.Append("\n    baseTypeName: '" + baseTypeName + "',");
        }
        //生成Interface信息
        var interfaces = type.GetInterfaces();
        if (interfaces.Length > 0)
        {
            sb.Append("\n    interfaceNames: [");
            for (int i = 0; i < interfaces.Length; i++)
            {
                var iType = interfaces[i];
                if (iType.IsPublic || iType.IsNestedPublic)
                {
                    sb.Append("'" + JSNameMgr.GetJSTypeFullName(interfaces[i]) + "'");
                    if (i < interfaces.Length - 1)
                        sb.Append(", ");
                }
            }
            sb.Append("],");
        }
        return sb;
    }

    #endregion

    #region Helper

    private static StreamWriter OpenFile(string fileName)
    {
        // IMPORTANT
        // Bom (byte order mark) is not needed
        Encoding utf8NoBom = new UTF8Encoding(false);
        return new StreamWriter(fileName, false, utf8NoBom);
    }

    public static string GetJsTypeName(Type type)
    {
        if (type == null)
            return "";
        string name = string.Empty;
        if (type.IsByRef)
        {
            name = GetJsTypeName(type.GetElementType());
        }
        else if (type.IsArray)
        {
            while (type.IsArray)
            {
                var subt = type.GetElementType();
                name += GetJsTypeName(subt) + '$';
                type = subt;
            }
            name += "Array";
        }
        else if (type.IsGenericTypeDefinition)
        {
            // never come here
            name = type.Name;
        }
        else if (type.IsGenericType)
        {
            name = type.Name;
            var ts = type.GetGenericArguments();

            bool hasGenericParameter = false;
            for (int i = 0; i < ts.Length; i++)
            {
                if (ts[i].IsGenericParameter)
                {
                    hasGenericParameter = true;
                    break;
                }
            }

            if (!hasGenericParameter)
            {
                for (int i = 0; i < ts.Length; i++)
                {
                    name += "$" + GetJsTypeName(ts[i]);
                }
            }
        }
        else
        {
            name = type.Name;
        }
        return name;
    }

    public static string GetJsPropertyName(PropertyInfo property)
    {
        string name = property.Name;
        var ps = property.GetIndexParameters();
        if (ps.Length > 0)
        {
            for (int i = 0; i < ps.Length; i++)
            {
                var type = ps[i].ParameterType;
                name += "$$" + GetJsTypeName(type);
            }
            name = name.Replace("`", "$");
        }
        return name;
    }

    public static string GetOverloadedMethodSuffix(string methodName, ParameterInfo[] paramS, bool overloaded,
        int TCounts = 0)
    {
        //         if (!overloaded && TCounts > 0)
        //         {
        //             Debug.Log("");
        //         }

        string name = methodName;
        if (TCounts > 0)
            name += "$" + TCounts;

        if (overloaded)
        {
            for (int i = 0; i < paramS.Length; i++)
            {
                var type = paramS[i].ParameterType;
                name += "$$" + GetJsTypeName(type);
            }
            name = name.Replace("`", "$");
        }
        return name;
    }

    private static void HandleStringFormat(StringBuilder sb)
    {
        sb.Replace("[[", "{");
        sb.Replace("]]", "}");
        sb.Replace("'", "\"");
    }

    #endregion
}