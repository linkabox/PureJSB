using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using cg;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public static class CSGenerator
{
    private const string REGISTER_HEADER = @"using UnityEngine;
public class CSGenerateRegister
[[
    public static void RegisterAll()
    [[
        if (JSMgr.allCallbackInfo.Count != 0)
        [[
            Debug.LogError(""Has already register CS binding"");
        ]]
{0}
    ]]
]]
";
    public static string _curTypeFileName;

    private static readonly string registerFile = Application.dataPath + "/Standard Assets/JSBinding/Source/CSGenerateRegister.cs";

    private static readonly Dictionary<string, bool> _manualFuncDic = new Dictionary<string, bool>();
    private static readonly StringBuilder _paramTypeInfoBuilder = new StringBuilder();

    public static HashSet<Type> ExportTypeSet { get; private set; }
    public static HashSet<Type> ExportEnumSet { get; private set; }

    public static void OnBegin()
    {
        GeneratorHelp.ClearTypeInfo();
        _paramTypeInfoBuilder.Length = 0;

        if (Directory.Exists(JSPathSettings.csGeneratedDir))
        {
            var files = Directory.GetFiles(JSPathSettings.csGeneratedDir);
            for (int i = 0; i < files.Length; i++)
            {
                File.Delete(files[i]);
            }
        }
        else
        {
            Directory.CreateDirectory(JSPathSettings.csGeneratedDir);
        }
    }

    public static void OnEnd()
    {
        if (_paramTypeInfoBuilder.Length > 0)
        {
            Debug.LogError("Warning! Has invalidated params\n" + _paramTypeInfoBuilder);
        }
    }

    public static string SharpKitTypeName(Type type)
    {
        string name = string.Empty;
        if (type.IsByRef)
        {
            name = SharpKitTypeName(type.GetElementType());
        }
        else if (type.IsArray)
        {
            while (type.IsArray)
            {
                var subt = type.GetElementType();
                name += SharpKitTypeName(subt) + '$';
                type = subt;
            }
            name += "Array";
        }
        else if (type.IsGenericType)
        {
            name = type.Name;
            var ts = type.GetGenericArguments();
            for (int i = 0; i < ts.Length; i++)
            {
                name += "$" + SharpKitTypeName(ts[i]);
            }
        }
        else
        {
            if (type == typeof(Object))
                name = "UE" + type.Name;
            else
                name = type.Name;
        }
        return name;
    }

    public static string SharpKitMethodName(string methodName, ParameterInfo[] paramS, bool overloaded, int TCounts = 0)
    {
        string name = methodName;
        if (overloaded)
        {
            if (TCounts > 0)
                name += "T" + TCounts;
            for (int i = 0; i < paramS.Length; i++)
            {
                var type = paramS[i].ParameterType;
                name += "$$" + SharpKitTypeName(type);
            }
            name = name.Replace("`", "T");
        }
        name = name.Replace("$", "_");
        return name;
    }

    /// <summary>
    /// 检查导出的接口参数列表是否包含不支持的参数类型
    /// </summary>
    public static void CheckParamList(string typeName, string methodName, ParameterInfo[] ps)
    {
        if (ps == null || ps.Length == 0) return;

        foreach (var parameterInfo in ps)
        {
            var pType = parameterInfo.ParameterType;
            if (typeof(System.Array).IsAssignableFrom(pType))
                continue;

            if (typeof(IList) == (pType)
                || typeof(IDictionary) == (pType)
                || typeof(List<>) == (pType)
                || typeof(Dictionary<,>) == (pType)
                || typeof(HashSet<>) == (pType))
                _paramTypeInfoBuilder.AppendLine(string.Format("type:{0}   method:{1}   parameter:{2} {3}",
                    typeName,
                    methodName,
                    parameterInfo.Position,
                    parameterInfo.ParameterType.Name));
        }
    }

    public static StringBuilder BuildFields(Type type, FieldInfo[] fields, int[] fieldsIndex, ClassCallbackNames ccbn)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < fields.Length; i++)
        {
            //var sbCall = new StringBuilder();

            var field = fields[i];
            if (field.DeclaringType != null) ccbn.nameSpaces.Add(field.DeclaringType.Namespace);
            bool isDelegate = JSDataExchangeEditor.IsDelegateDerived(field.FieldType);
            // (typeof(System.Delegate).IsAssignableFrom(field.FieldType));
            if (isDelegate)
            {
                sb.Append(JSDataExchangeEditor.Build_DelegateFunction(type, field, field.FieldType, i, 0));
            }
            bool bGenericT = type.IsGenericTypeDefinition;
            if (bGenericT)
            {
                sb.AppendFormat("public static FieldID fieldID{0} = new FieldID(\"{1}\");\n", i, field.Name);
            }


            JSDataExchangeEditor.MemberFeature features = 0;
            if (field.IsStatic) features |= JSDataExchangeEditor.MemberFeature.Static;

            StringBuilder sbt = null;
            if (bGenericT)
            {
                sbt = new StringBuilder();

                sbt.AppendFormat(
                    "    FieldInfo member = GenericTypeCache.getField(vc.csObj.GetType(), fieldID{0}); \n", i);
                sbt.AppendFormat("    if (member == null) return;\n");
                sbt.Append("\n");
            }

            string functionName = JSNameMgr.HandleFunctionName(type.Name + "_" + field.Name);
            sb.AppendFormat("static void {0}(JSVCall vc)\n[[\n", functionName);

            if (bGenericT)
            {
                sb.Append(sbt);
            }

            bool bReadOnly = field.IsInitOnly || field.IsLiteral;
            if (!bReadOnly)
            {
                sb.Append("    if (vc.bGet) [[\n");
            }

            // Debug.Log("FIELD " + type.Name + "." + field.Name);

            sb.Append(JSDataExchangeEditor.BuildCallString(type, field, "" /* argList */,
                features | JSDataExchangeEditor.MemberFeature.Get));

            sb.AppendFormat("        {0}\n", JSDataExchangeEditor.Get_Return(field.FieldType, "result"));

            // set
            if (!bReadOnly)
            {
                sb.Append("    ]]\n    else [[\n");

                if (!isDelegate)
                {
                    var paramHandler = JSDataExchangeEditor.Get_ParamHandler(field);
                    sb.Append("        " + paramHandler.getter + "\n");

                    sb.Append(JSDataExchangeEditor.BuildCallString(type, field, "" /* argList */,
                        features | JSDataExchangeEditor.MemberFeature.Set, paramHandler.argName));
                }
                else
                {
                    string getDelegateFuncitonName = JSDataExchangeEditor.GetMethodArg_DelegateFuncionName(type,
                        field.Name, i, 0);

                    //                     sb.Append(JSDataExchangeEditor.BuildCallString(type, field, "" /* argList */,
                    //                                 features | JSDataExchangeEditor.MemberFeature.Set, getDelegateFuncitonName + "(vc.getJSFunctionValue())"));

                    string getDelegate = JSDataExchangeEditor.Build_GetDelegate(getDelegateFuncitonName, field.FieldType);
                    sb.Append(JSDataExchangeEditor.BuildCallString(type, field, "" /* argList */,
                        features | JSDataExchangeEditor.MemberFeature.Set, getDelegate));
                }
                sb.Append("    ]]\n");
            }

            sb.AppendFormat("]]\n");
            ccbn.fields.Add(functionName);
        }

        return sb;
    }


    public static StringBuilder BuildProperties(Type type, PropertyInfo[] properties, int[] propertiesIndex,
        ClassCallbackNames ccbn)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < properties.Length; i++)
        {
            var sbCall = new StringBuilder();

            var property = properties[i];
            if (property.DeclaringType != null) ccbn.nameSpaces.Add(property.DeclaringType.Namespace);
            var accessors = property.GetAccessors();
            bool isStatic = accessors[0].IsStatic;
            JSDataExchangeEditor.MemberFeature features = 0;
            if (isStatic) features |= JSDataExchangeEditor.MemberFeature.Static;

            bool bGenericT = type.IsGenericTypeDefinition;
            StringBuilder sbt = null;

            bool isDelegate = JSDataExchangeEditor.IsDelegateDerived(property.PropertyType);
            ; // (typeof(System.Delegate).IsAssignableFrom(property.PropertyType));
            if (isDelegate)
            {
                sb.Append(JSDataExchangeEditor.Build_DelegateFunction(type, property, property.PropertyType, i, 0));
            }

            // functionName
            var ps = property.GetIndexParameters();
            bool bIndexer = ps.Length > 0;
            if (bIndexer) features |= JSDataExchangeEditor.MemberFeature.Indexer;
            var argActual = new args();
            var paramHandlers = new JSDataExchangeEditor.ParamHandler[ps.Length];
            for (int j = 0; j < ps.Length; j++)
            {
                paramHandlers[j] = JSDataExchangeEditor.Get_ParamHandler(ps[j].ParameterType, j, false, false);
                argActual.Add(paramHandlers[j].argName);
            }

            string functionName = type.Name + "_" + property.Name;
            if (bIndexer)
            {
                foreach (var p in ps)
                {
                    functionName += "_" + p.ParameterType.Name;
                }
            }
            functionName = JSNameMgr.HandleFunctionName(functionName);
            bool isManulFunc = IsManualFunction(functionName);

            // PropertyID
            if (bGenericT && !isManulFunc)
            {
                var arg = new args();
                arg.AddFormat("\"{0}\"", property.Name);

                arg.AddFormat("\"{0}\"", property.PropertyType.Name);
                if (property.PropertyType.IsGenericParameter)
                {
                    arg.Add("TypeFlag.IsT");
                }
                else
                {
                    arg.Add("TypeFlag.None");
                }

                var arg1 = new args();
                var arg2 = new args();

                foreach (var p in property.GetIndexParameters())
                {
                    var argFlag = ParameterInfo2TypeFlag(p);

                    arg1.AddFormat("\"{0}\"", p.ParameterType.Name);
                    arg2.Add(argFlag.Format(args.ArgsFormat.Flag));
                }

                if (arg1.Count > 0)
                    arg.AddFormat("new string[]{0}", arg1.Format(args.ArgsFormat.Brace));
                else
                    arg.Add("null");
                if (arg2.Count > 0)
                    arg.AddFormat("new TypeFlag[]{0}", arg2.Format(args.ArgsFormat.Brace));
                else
                    arg.Add("null");
                sb.AppendFormat("public static PropertyID propertyID{0} = new PropertyID({1});\n", i, arg);

                sbt = new StringBuilder();
                sbt.AppendFormat(
                    "    PropertyInfo member = GenericTypeCache.getProperty(vc.csObj.GetType(), propertyID{0}); \n", i);
                sbt.AppendFormat("    if (member == null) return;\n");
                sbt.Append("\n");
            }

            //
            // check to see if this is a indexer
            //
            sb.AppendFormat("static void {0}(JSVCall vc)\n[[\n", functionName);

            if (!isManulFunc)
            {
                if (bGenericT)
                {
                    sb.Append(sbt);
                }
                for (int j = 0; j < ps.Length; j++)
                {
                    sb.Append("        " + paramHandlers[j].getter + "\n");
                }

                bool bReadOnly = !property.CanWrite || property.GetSetMethod() == null;
                sbCall.Append(JSDataExchangeEditor.BuildCallString(type, property,
                    argActual.Format(args.ArgsFormat.OnlyList),
                    features | JSDataExchangeEditor.MemberFeature.Get));

                if (!bReadOnly)
                {
                    sb.Append("    if (vc.bGet)\n");
                    sb.Append("    [[ \n");
                }

                //if (type.IsValueType && !field.IsStatic)
                //    sb.AppendFormat("{0} argThis = ({0})vc.csObj;", type.Name);

                if (property.CanRead)
                {
                    if (property.GetGetMethod() != null)
                    {
                        sb.Append(sbCall);
                        sb.AppendFormat("        {0}\n", JSDataExchangeEditor.Get_Return(property.PropertyType, "result"));
                    }
                    else
                    {
                        Debug.Log(type.Name + "." + property.Name + " 'get' is ignored because it's not public.");
                    }
                }
                if (!bReadOnly)
                {
                    sb.Append("    ]]\n");
                }

                // set
                if (!bReadOnly)
                {
                    sb.Append("    else\n");
                    sb.Append("    [[ \n");

                    if (!isDelegate)
                    {
                        int ParamIndex = ps.Length;

                        var paramHandler = JSDataExchangeEditor.Get_ParamHandler(property.PropertyType, ParamIndex, false,
                            false);
                        sb.Append("        " + paramHandler.getter + "\n");

                        sb.Append(JSDataExchangeEditor.BuildCallString(type, property,
                            argActual.Format(args.ArgsFormat.OnlyList),
                            features | JSDataExchangeEditor.MemberFeature.Set, paramHandler.argName));
                    }
                    else
                    {
                        string getDelegateFuncitonName = JSDataExchangeEditor.GetMethodArg_DelegateFuncionName(type,
                            property.Name, i, 0);

                        //                     sb.Append(JSDataExchangeEditor.BuildCallString(type, field, "" /* argList */,
                        //                                 features | JSDataExchangeEditor.MemberFeature.Set, getDelegateFuncitonName + "(vc.getJSFunctionValue())"));

                        string getDelegate = JSDataExchangeEditor.Build_GetDelegate(getDelegateFuncitonName,
                            property.PropertyType);
                        sb.Append(JSDataExchangeEditor.BuildCallString(type, property, "" /* argList */,
                            features | JSDataExchangeEditor.MemberFeature.Set, getDelegate));
                    }
                    sb.Append("    ]]\n");
                }
            }
            else
            {
                sb.AppendFormat("        UnityEngineManual.{0}(vc);\n", functionName);
            }

            sb.Append("]]\n");

            ccbn.properties.Add(functionName);
        }
        return sb;
    }

    //private static StringBuilder GenListCSParam2(ParameterInfo[] ps)
    //{
    //    var sb = new StringBuilder();

    //    for (int i = 0; i < ps.Length; i++)
    //    {
    //        var p = ps[i];
    //        var t = p.ParameterType;
    //        sb.AppendFormat("new JSVCall.CSParam({0}, {1}, {2}, {3}{4}, {5}), ", t.IsByRef ? "true" : "false", p.IsOptional ? "true" : "false",
    //            t.IsArray ? "true" : "false", "typeof(" + JSNameMgr.GetTypeFullName(t) + ")",
    //            t.IsByRef ? ".MakeByRefType()" : "", "null");
    //    }
    //    var sbX = new StringBuilder();
    //    sbX.AppendFormat("new JSVCall.CSParam[][[{0}]]", sb);
    //    return sbX;
    //}

    public static StringBuilder BuildSpecialFunctionCall(ParameterInfo[] ps, string className, string methodName,
        bool bStatic, bool returnVoid, Type returnType)
    {
        var sb = new StringBuilder();
        var paramHandlers = new JSDataExchangeEditor.ParamHandler[ps.Length];
        for (int i = 0; i < ps.Length; i++)
        {
            paramHandlers[i] = JSDataExchangeEditor.Get_ParamHandler(ps[i], i);
            sb.Append("    " + paramHandlers[i].getter + "\n");
        }

        string strCall = string.Empty;

        // must be static
        if (methodName == "op_Addition")
            strCall = paramHandlers[0].argName + " + " + paramHandlers[1].argName;
        else if (methodName == "op_Subtraction")
            strCall = paramHandlers[0].argName + " - " + paramHandlers[1].argName;
        else if (methodName == "op_Multiply")
            strCall = paramHandlers[0].argName + " * " + paramHandlers[1].argName;
        else if (methodName == "op_Division")
            strCall = paramHandlers[0].argName + " / " + paramHandlers[1].argName;
        else if (methodName == "op_Equality")
            strCall = paramHandlers[0].argName + " == " + paramHandlers[1].argName;
        else if (methodName == "op_Inequality")
            strCall = paramHandlers[0].argName + " != " + paramHandlers[1].argName;

        else if (methodName == "op_UnaryNegation")
            strCall = "-" + paramHandlers[0].argName;

        else if (methodName == "op_LessThan")
            strCall = paramHandlers[0].argName + " < " + paramHandlers[1].argName;
        else if (methodName == "op_LessThanOrEqual")
            strCall = paramHandlers[0].argName + " <= " + paramHandlers[1].argName;
        else if (methodName == "op_GreaterThan")
            strCall = paramHandlers[0].argName + " > " + paramHandlers[1].argName;
        else if (methodName == "op_GreaterThanOrEqual")
            strCall = paramHandlers[0].argName + " >= " + paramHandlers[1].argName;
        else if (methodName == "op_Implicit")
            strCall = "(" + JSNameMgr.GetTypeFullName(returnType) + ")" + paramHandlers[0].argName;
        else
            Debug.LogError("Unknown special name: " + methodName);

        string ret = JSDataExchangeEditor.Get_Return(returnType, strCall);
        sb.Append("    " + ret);
        return sb;
    }

    public static StringBuilder BuildEventFunctionCall(
        Type type,
        int methodTag,
        ParameterInfo[] ps,
        string methodName,
        bool bStatic,
        bool bAdd)
    {
        var sb = new StringBuilder();

        sb.AppendFormat("    int len = argc;\n");

        var sbGetParam = new StringBuilder();
        var sbActualParam = new StringBuilder();

        if (ps.Length > 0)
        {
            var p = ps[0];
            string delegateGetName = JSDataExchangeEditor.GetMethodArg_DelegateFuncionName(type,
                methodName, methodTag, 0);

            sbGetParam.AppendFormat("        {0} action = {1};\n",
                JSNameMgr.GetTypeFullName(p.ParameterType), // [0]
                JSDataExchangeEditor.Build_GetDelegate(delegateGetName, p.ParameterType) // [1]
                );

            sbActualParam.Append("action");
        }

        /*
         * 0 parameters count
         * 1 class name
         * 2 function name
         * 3 actual parameters
         */
        var sbCall = new StringBuilder();
        string opStr = bAdd ? "+=" : "-=";
        string eventName = bAdd ? methodName.Replace("add_", "") : methodName.Replace("remove_", "");
        if (bStatic)
        {
            sbCall.AppendFormat("{0}.{1} {2} {3};", JSNameMgr.GetTypeFullName(type),
                eventName, opStr, sbActualParam);
        }
        else
        {
            sbCall.AppendFormat("(({0})vc.csObj).{1} {2} {3};", JSNameMgr.GetTypeFullName(type),
                eventName, opStr, sbActualParam);
        }

        sb.Append("    if (len == 1) \n");
        sb.Append("    [[\n");
        sb.Append(sbGetParam);
        sb.Append("        ").Append(sbCall).Append("\n");
        sb.Append("    ]]\n");

        return sb;
    }

    public static StringBuilder BuildNormalFunctionCall(
        Type type,
        int methodTag,
        ParameterInfo[] ps,
        string methodName,
        bool bStatic,
        Type returnType,
        bool bConstructor,
        int TCount = 0)
    {
        var sb = new StringBuilder();

        if (bConstructor)
        {
            sb.Append("    int _this = JSApi.getObject((int)JSApi.GetType.Arg);\n");
            sb.Append("    JSApi.attachFinalizerObject(_this);\n");
            sb.Append("    --argc;\n\n");
        }

        if (bConstructor)
        {
            if (type.IsGenericTypeDefinition)
            {
                // Not generic method, but is generic type
                var sbt = new StringBuilder();

                sbt.AppendFormat(
                    "    ConstructorInfo constructor = JSDataExchangeMgr.makeGenericConstructor(typeof({0}), constructorID{1}); \n",
                    JSNameMgr.GetTypeFullName(type), methodTag);

                //sbMethodHitTest.AppendFormat("GenericTypeCache.getConstructor(typeof({0}), {2}.constructorID{1});\n", JSNameMgr.GetTypeFullName(type), methodTag, JSNameMgr.GetTypeFileName(type));

                sbt.AppendFormat("    if (constructor == null) return true;\n");
                sbt.Append("\n");

                sb.Append(sbt);
            }
        }

        else if (TCount > 0)
        {
            var sbt = new StringBuilder();
            sbt.Append("    // Get generic method by name and param count.\n");

            if (!bStatic) // instance method
            {
                sbt.AppendFormat(
                    "    MethodInfo method = JSDataExchangeMgr.makeGenericMethod(vc.csObj.GetType(), methodID{0}, {1}); \n",
                    methodTag,
                    TCount);
            }
            else // static method
            {
                sbt.AppendFormat(
                    "    MethodInfo method = JSDataExchangeMgr.makeGenericMethod(typeof({0}), methodID{1}, {2}); \n",
                    JSNameMgr.GetTypeFullName(type),
                    methodTag,
                    TCount);
            }
            sbt.AppendFormat("    if (method == null) return true;\n");
            sbt.Append("\n");

            sb.Append(sbt);
        }
        else if (type.IsGenericTypeDefinition)
        {
            // not generic method, but is generic type
            var sbt = new StringBuilder();
            sbt.Append("    // Get generic method by name and param count.\n");

            if (!bStatic) // instance method
            {
                sbt.AppendFormat(
                    "    MethodInfo method = GenericTypeCache.getMethod(vc.csObj.GetType(), methodID{0}); \n", methodTag);
            }
            else // static method
            {
                // Debug.LogError("=================================ERROR");
                sbt.AppendFormat("    MethodInfo method = GenericTypeCache.getMethod(typeof({0}), methodID{1}); \n",
                    JSNameMgr.GetTypeFullName(type), // [0]
                    methodTag);
            }
            sbt.AppendFormat("    if (method == null) return true;\n");
            sbt.Append("\n");

            sb.Append(sbt);
        }
        else if (type.IsGenericType)
        {
            /////////////////////
            /// ERROR ///////////
            /////////////////////
        }

        var paramHandlers = new JSDataExchangeEditor.ParamHandler[ps.Length];
        for (int i = 0; i < ps.Length; i++)
        {
            if (true /* !ps[i].ParameterType.IsGenericParameter */)
            {
                // use original method's parameterinfo
                if (!JSDataExchangeEditor.IsDelegateDerived(ps[i].ParameterType))
                    paramHandlers[i] = JSDataExchangeEditor.Get_ParamHandler(ps[i], i);
                //                if (ps[i].ParameterType.IsGenericParameter)
                //                {
                //                    paramHandlers[i].getter = "    JSMgr.datax.setTemp(method.GetParameters()[" + i.ToString() + "].ParameterType);\n" + paramHandlers[i].getter;
                //                }
            }
        }

        // minimal params needed
        int minNeedParams = 0;
        for (int i = 0; i < ps.Length; i++)
        {
            if (ps[i].IsOptional)
            {
                break;
            }
            minNeedParams++;
        }


        if (bConstructor && type.IsGenericTypeDefinition)
            sb.AppendFormat("    int len = argc - {0};\n", type.GetGenericArguments().Length);
        else if (TCount == 0)
            sb.AppendFormat("    int len = argc;\n");
        else
            sb.AppendFormat("    int len = argc - {0};\n", TCount);

        for (int j = minNeedParams; j <= ps.Length; j++)
        {
            var sbGetParam = new StringBuilder();
            var sbActualParam = new StringBuilder();
            var sbUpdateRefParam = new StringBuilder();

            // receive arguments first
            for (int i = 0; i < j; i++)
            {
                var p = ps[i];
                //if (typeof(System.Delegate).IsAssignableFrom(p.ParameterType))
                if (JSDataExchangeEditor.IsDelegateDerived(p.ParameterType))
                {
                    //string delegateGetName = JSDataExchangeEditor.GetFunctionArg_DelegateFuncionName(className, methodName, methodIndex, i);
                    string delegateGetName = JSDataExchangeEditor.GetMethodArg_DelegateFuncionName(type,
                        methodName, methodTag, i);

                    //if (p.ParameterType.IsGenericType)
                    if (p.ParameterType.ContainsGenericParameters)
                    {
                        // cg.args ta = new cg.args();
                        // sbGetParam.AppendFormat("foreach (var a in method.GetParameters()[{0}].ParameterType.GetGenericArguments()) ta.Add();");


                        sbGetParam.AppendFormat("object arg{0} = JSDataExchangeMgr.GetJSArg<object>(()=>[[\n", i);
                        sbGetParam.AppendFormat("    if (JSApi.isFunctionS((int)JSApi.GetType.Arg)) [[\n");
                        sbGetParam.AppendFormat(
                            "        var getDelegateFun{0} = typeof({1}).GetMethod(\"{2}\").MakeGenericMethod\n", i,
                            _curTypeFileName, delegateGetName);
                        sbGetParam.AppendFormat(
                            "            (method.GetParameters()[{0}].ParameterType.GetGenericArguments());\n", i);
                        sbGetParam.AppendFormat(
                            "        return getDelegateFun{0}.Invoke(null, new object[][[{1}]]);\n", i,
                            "JSApi.getFunctionS((int)JSApi.GetType.Arg)");
                        sbGetParam.Append("    ]]\n");
                        sbGetParam.Append("    else\n");
                        sbGetParam.AppendFormat("        return JSMgr.datax.getObject((int)JSApi.GetType.Arg);\n");
                        sbGetParam.Append("]]);\n");
                    }
                    else
                    {
                        sbGetParam.AppendFormat("        {0} arg{1} = {2};\n",
                            JSNameMgr.GetTypeFullName(p.ParameterType), // [0]
                            i, // [1]
                            JSDataExchangeEditor.Build_GetDelegate(delegateGetName, p.ParameterType) // [2]
                            );
                    }
                }
                else
                {
                    sbGetParam.Append("        " + paramHandlers[i].getter + "\n");
                }

                // value type array
                // no 'out' nor 'ref'
                if ((p.ParameterType.IsByRef || p.IsOut) && !p.ParameterType.IsArray)
                    sbActualParam.AppendFormat("{0} arg{1}{2}", p.IsOut ? "out" : "ref", i, i == j - 1 ? "" : ", ");
                else
                    sbActualParam.AppendFormat("arg{0}{1}", i, i == j - 1 ? "" : ", ");

                // updater
                sbUpdateRefParam.Append(paramHandlers[i].updater);
            }

            /*
             * 0 parameters count
             * 1 class name
             * 2 function name
             * 3 actual parameters
             */
            if (bConstructor)
            {
                var sbCall = new StringBuilder();

                if (!type.IsGenericTypeDefinition)
                    sbCall.AppendFormat("new {0}({1})", JSNameMgr.GetTypeFullName(type), sbActualParam);
                else
                {
                    sbCall.AppendFormat("constructor.Invoke(null, new object[][[{0}]])", sbActualParam);
                }

                string callAndReturn =
                    new StringBuilder().AppendFormat("        JSMgr.addJSCSRel(_this, {0});", sbCall).ToString();

                sb.AppendFormat("    {0}if (len == {1})\n", j == minNeedParams ? "" : "else ", j);
                sb.Append("    [[\n");
                sb.Append(sbGetParam);
                sb.Append(callAndReturn).Append("\n");
                if (sbUpdateRefParam.Length > 0)
                    sb.Append(sbUpdateRefParam);
                sb.Append("    ]]\n");
            }
            else
            {
                var sbCall = new StringBuilder();
                var sbActualParamT_arr = new StringBuilder();
                //StringBuilder sbUpdateRefT = new StringBuilder();

                if (TCount == 0 && !type.IsGenericTypeDefinition)
                {
                    if (bStatic)
                        sbCall.AppendFormat("{0}.{1}({2})", JSNameMgr.GetTypeFullName(type), methodName,
                            sbActualParam);
                    else if (!type.IsValueType)
                        sbCall.AppendFormat("(({0})vc.csObj).{1}({2})", JSNameMgr.GetTypeFullName(type),
                            methodName, sbActualParam);
                    else
                        sbCall.AppendFormat("argThis.{0}({1})", methodName, sbActualParam);
                }
                else
                {
                    if (ps.Length > 0)
                    {
                        sbActualParamT_arr.AppendFormat("object[] arr_t = new object[][[ {0} ]];", sbActualParam);
                        // reflection call doesn't need out or ref modifier
                        sbActualParamT_arr.Replace(" out ", " ").Replace(" ref ", " ");
                    }
                    else
                    {
                        sbActualParamT_arr.Append("object[] arr_t = null;");
                    }

                    if (bStatic)
                        sbCall.AppendFormat("method.Invoke(null, arr_t)");
                    else if (!type.IsValueType)
                        sbCall.AppendFormat("method.Invoke(vc.csObj, arr_t)");
                    else
                        sbCall.AppendFormat("method.Invoke(vc.csObj, arr_t)");
                }

                string callAndReturn = JSDataExchangeEditor.Get_Return(returnType, sbCall.ToString());

                StringBuilder sbStruct = null;
                if (type.IsValueType && !bStatic && TCount == 0 && !type.IsGenericTypeDefinition)
                {
                    sbStruct = new StringBuilder();
                    sbStruct.AppendFormat("{0} argThis = ({0})vc.csObj;", JSNameMgr.GetTypeFullName(type));
                }

                sb.AppendFormat("    {0}if (len == {1}) \n", j == minNeedParams ? "" : "else ", j);
                sb.Append("    [[\n");
                sb.Append(sbGetParam);
                if (sbActualParamT_arr.Length > 0)
                {
                    sb.Append("        ").Append(sbActualParamT_arr).Append("\n");
                }

                // if it is Struct, get argThis first
                if (type.IsValueType && !bStatic && TCount == 0 && !type.IsGenericTypeDefinition)
                {
                    sb.Append(sbStruct);
                }

                sb.Append("        ").Append(callAndReturn).Append("\n");

                // if it is Struct, update 'this' object
                if (type.IsValueType && !bStatic && TCount == 0 && !type.IsGenericTypeDefinition)
                {
                    sb.Append("        JSMgr.changeJSObj(vc.jsObjID, argThis);\n");
                }
                sb.Append(sbUpdateRefParam);
                sb.Append("    ]]\n");
            }
        }

        return sb;
    }

    public static StringBuilder BuildConstructors(Type type, ConstructorInfo[] constructors, int[] constructorsIndex,
        ClassCallbackNames ccbn)
    {
        /*
        * methods
        * 0 function name
        * 1 list<CSParam> generation
        * 2 function call
        */
        string fmt = @"
static bool {0}(JSVCall vc, int argc)
[[
{1}
    return true;
]]
";
        var sb = new StringBuilder();
        /*if (constructors.Length == 0 && JSBindingSettings.IsGeneratedDefaultConstructor(type) &&
            (type.IsValueType || (type.IsClass && !type.IsAbstract && !type.IsInterface)))
        {
            int olIndex = 1;
            bool returnVoid = false;
            string functionName = type.Name + "_" + type.Name +
                (olIndex > 0 ? olIndex.ToString() : "") + "";// (cons.IsStatic ? "_S" : "");
            sb.AppendFormat(fmt, functionName,
                BuildNormalFunctionCall(0, new ParameterInfo[0], type.Name, type.Name, false, returnVoid, null, true));

            ccbn.constructors.Add(functionName);
            ccbn.constructorsCSParam.Add(GenListCSParam2(new ParameterInfo[0]).ToString());        
        }*/

        // increase index if adding default constructor
        //         int deltaIndex = 0;
        if (JSBCodeGenSettings.NeedGenDefaultConstructor(type))
        {
            //             deltaIndex = 1;
        }

        for (int i = 0; i < constructors.Length; i++)
        {
            var cons = constructors[i];

            if (cons == null)
            {
                sb.AppendFormat("public static ConstructorID constructorID{0} = new ConstructorID({1});\n", i,
                    "null, null");

                // this is default constructor
                //bool returnVoid = false;
                //string functionName = type.Name + "_" + type.Name + "1";
                int olIndex = i + 1; // for constuctors, they are always overloaded
                string functionName =
                    JSNameMgr.HandleFunctionName(type.Name + "_" + type.Name + (olIndex > 0 ? olIndex.ToString() : ""));

                sb.AppendFormat(fmt, functionName,
                    BuildNormalFunctionCall(type, 0, new ParameterInfo[0], type.Name, false, null, true));

                ccbn.constructors.Add(functionName);
                //ccbn.constructorsCSParam.Add(GenListCSParam2(new ParameterInfo[0]).ToString());
            }
            else
            {
                var paramS = cons.GetParameters();
                CheckParamList(type.Name, cons.Name, paramS);
                int olIndex = i + 1; // for constuctors, they are always overloaded
                int methodTag = i /* + deltaIndex*/;

                for (int j = 0; j < paramS.Length; j++)
                {
                    var pInfo = paramS[j];
                    ccbn.nameSpaces.Add(pInfo.ParameterType.Namespace);
                    if (JSDataExchangeEditor.IsDelegateDerived(pInfo.ParameterType))
                    {
                        var sbD = JSDataExchangeEditor.Build_DelegateFunction(type, cons,
                            pInfo.ParameterType, methodTag, j);
                        sb.Append(sbD);
                    }
                }

                // ConstructorID
                if (type.IsGenericTypeDefinition)
                {
                    var arg = new args();
                    var arg1 = new args();
                    var arg2 = new args();

                    foreach (var p in cons.GetParameters())
                    {
                        var argFlag = ParameterInfo2TypeFlag(p);
                        arg1.AddFormat("\"{0}\"", p.ParameterType.Name);
                        arg2.Add(argFlag.Format(args.ArgsFormat.Flag));
                    }

                    if (arg1.Count > 0)
                        arg.AddFormat("new string[]{0}", arg1.Format(args.ArgsFormat.Brace));
                    else
                        arg.Add("null");
                    if (arg2.Count > 0)
                        arg.AddFormat("new TypeFlag[]{0}", arg2.Format(args.ArgsFormat.Brace));
                    else
                        arg.Add("null");
                    sb.AppendFormat("public static ConstructorID constructorID{0} = new ConstructorID({1});\n", i, arg);
                }

                string functionName =
                    JSNameMgr.HandleFunctionName(type.Name + "_" + type.Name + (olIndex > 0 ? olIndex.ToString() : "") +
                                                 (cons.IsStatic ? "_S" : ""));

                sb.AppendFormat(fmt, functionName,
                    BuildNormalFunctionCall(type, methodTag, paramS, cons.Name, cons.IsStatic, null, true, 0));

                ccbn.constructors.Add(functionName);
                //ccbn.constructorsCSParam.Add(GenListCSParam2(paramS).ToString());
            }
        }
        return sb;
    }

    public static StringBuilder BuildMethods(Type type, MethodInfo[] methods, int[] methodsIndex, int[] olInfo,
        ClassCallbackNames ccbn)
    {
        /*
        * methods
        * 0 function name
        * 1 list<CSParam> generation
        * 2 function call
        */
        string fmt = @"
static bool {0}(JSVCall vc, int argc)
[[
{1}
    return true;
]]
";
        var sb = new StringBuilder();
        for (int i = 0; i < methods.Length; i++)
        {
            var method = methods[i];
            var paramS = method.GetParameters();
            CheckParamList(type.Name, method.Name, paramS);

            for (int j = 0; j < paramS.Length; j++)
            {
                var pInfo = paramS[j];
                ccbn.nameSpaces.Add(pInfo.ParameterType.Namespace);
                //                 if (pInfo.ParameterType == typeof(DaikonForge.Tween.TweenAssignmentCallback<Vector3>))
                //                 {
                //                     Debug.Log("yes");
                //                
                //if (typeof(System.Delegate).IsAssignableFrom(pInfo.ParameterType))
                if (JSDataExchangeEditor.IsDelegateDerived(pInfo.ParameterType))
                {
                    // StringBuilder sbD = JSDataExchangeEditor.BuildFunctionArg_DelegateFunction(type.Name, method.Name, pInfo.ParameterType, i, j);
                    var sbD = JSDataExchangeEditor.Build_DelegateFunction(type, method,
                        pInfo.ParameterType, i, j);

                    sb.Append(sbD);
                }
            }

            // MethodID
            if (type.IsGenericTypeDefinition || method.IsGenericMethodDefinition)
            {
                var arg = new args();
                arg.AddFormat("\"{0}\"", method.Name);

                arg.AddFormat("\"{0}\"", method.ReturnType.Name);
                if (method.ReturnType.IsGenericParameter)
                {
                    arg.Add("TypeFlag.IsT");
                }
                else
                {
                    arg.Add("TypeFlag.None");
                }

                var arg1 = new args();
                var arg2 = new args();

                foreach (var p in method.GetParameters())
                {
                    // flag of a parameter
                    var argFlag = ParameterInfo2TypeFlag(p);

                    arg1.AddFormat("\"{0}\"", p.ParameterType.Name);
                    arg2.Add(argFlag.Format(args.ArgsFormat.Flag));
                }

                if (arg1.Count > 0)
                    arg.AddFormat("new string[]{0}", arg1.Format(args.ArgsFormat.Brace));
                else
                    arg.Add("null");
                if (arg2.Count > 0)
                    arg.AddFormat("new TypeFlag[]{0}", arg2.Format(args.ArgsFormat.Brace));
                else
                    arg.Add("null");
                sb.AppendFormat("public static MethodID methodID{0} = new MethodID({1});\n", i, arg);
            }

            int olIndex = olInfo[i];
            bool returnVoid = method.ReturnType == typeof(void);

            string functionName = type.Name + "_" + method.Name + (olIndex > 0 ? olIndex.ToString() : "") +
                                  (method.IsStatic ? "_S" : "");

            int TCount = 0;
            if (method.IsGenericMethodDefinition)
            {
                TCount = method.GetGenericArguments().Length;
            }

            // if you change functionName
            // also have to change code in 'Manual/' folder
            functionName =
                JSNameMgr.HandleFunctionName(type.Name + "_" + SharpKitMethodName(method.Name, paramS, true, TCount));
            if (method.IsSpecialName && method.Name == "op_Implicit" && paramS.Length > 0)
            {
                functionName += "_to_" + method.ReturnType.Name;
            }
            if (IsManualFunction(functionName))
            {
                sb.AppendFormat(fmt, functionName, "    UnityEngineManual." + functionName + "(vc, argc);");
            }
            else if (!JSBCodeGenSettings.IsSupportByDotNet2SubSet(functionName))
            {
                sb.AppendFormat(fmt, functionName,
                    "    UnityEngine.Debug.LogError(\"This method is not supported by .Net 2.0 subset.\");");
            }
            else
            {
                bool isSpecialFunc = method.IsSpecialName;
                StringBuilder sbFuncBlock = null;
                if (isSpecialFunc)
                {
                    bool isEventMethod = method.Name.StartsWith("add_") || method.Name.StartsWith("remove_");
                    if (isEventMethod)
                    {
                        bool addEvent = method.Name.StartsWith("add_");
                        sbFuncBlock = BuildEventFunctionCall(type, i, paramS, method.Name, method.IsStatic, addEvent);
                    }
                    else
                    {
                        sbFuncBlock = BuildSpecialFunctionCall(paramS, type.Name, method.Name, method.IsStatic,
                            returnVoid,
                            method.ReturnType);
                    }
                }
                else
                {
                    sbFuncBlock = BuildNormalFunctionCall(type, i, paramS, method.Name,
                        method.IsStatic,
                        method.ReturnType,
                        false,
                        TCount);
                }
                sb.AppendFormat(fmt, functionName, sbFuncBlock);
            }

            ccbn.methods.Add(functionName);
            //ccbn.methodsCSParam.Add(GenListCSParam2(paramS).ToString());
        }
        return sb;
    }

    public static StringBuilder BuildClass(Type type, StringBuilder sbFields, StringBuilder sbProperties,
        StringBuilder sbMethods, StringBuilder sbConstructors, StringBuilder sbRegister)
    {
        /*
        * class
        * 0 class name
        * 1 fields
        * 2 properties
        * 3 methods
        * 4 constructors
        */
        string fmt = @"
////////////////////// {0} ///////////////////////////////////////
// constructors
{4}
// fields
{1}
// properties
{2}
// methods
{3}

//register
{5}
";
        var sb = new StringBuilder();
        sb.AppendFormat(fmt, type.Name, sbFields, sbProperties, sbMethods, sbConstructors, sbRegister);
        return sb;
    }

    private static StringBuilder BuildRegisterFunction(ClassCallbackNames ccbn, GeneratorHelp.ATypeInfo ti)
    {
        string fmt = @"
public static void __Register()
[[
    JSMgr.CallbackInfo ci = new JSMgr.CallbackInfo();
    ci.type = typeof({0});
    ci.fields = new JSMgr.CSCallbackField[]
    [[
{1}
    ]];
    ci.properties = new JSMgr.CSCallbackProperty[]
    [[
{2}
    ]];
    ci.constructors = new JSMgr.MethodCallBackInfo[]
    [[
{3}
    ]];
    ci.methods = new JSMgr.MethodCallBackInfo[]
    [[
{4}
    ]];
    JSMgr.allCallbackInfo.Add(ci);
]]
";
        var sb = new StringBuilder();

        var sbField = new StringBuilder();
        var sbProperty = new StringBuilder();
        var sbCons = new StringBuilder();
        var sbMethod = new StringBuilder();

        for (int i = 0; i < ccbn.fields.Count; i++)
            sbField.AppendFormat("        {0},\n", ccbn.fields[i]);
        for (int i = 0; i < ccbn.properties.Count; i++)
            sbProperty.AppendFormat("        {0},\n", ccbn.properties[i]);
        for (int i = 0; i < ccbn.constructors.Count; i++)
        {
            if (ccbn.constructors.Count == 1 && ti.constructors.Length == 0) // no constructors   add a default  so ...
                sbCons.AppendFormat("        new JSMgr.MethodCallBackInfo({0}, '{1}'),\n",
                    ccbn.constructors[i],
                    ccbn.type.Name);
            else
                sbCons.AppendFormat("        new JSMgr.MethodCallBackInfo({0}, '{1}'),\n",
                    ccbn.constructors[i],
                    ti.constructors[i] == null ? ".ctor" : ti.constructors[i].Name);
        }
        for (int i = 0; i < ccbn.methods.Count; i++)
        {
            // if method is not overloaded
            // don's save the cs param array
            sbMethod.AppendFormat("        new JSMgr.MethodCallBackInfo({0}, '{1}'),\n",
                ccbn.methods[i],
                ti.methods[i].Name);
        }

        sb.AppendFormat(fmt, JSNameMgr.GetTypeFullName(ccbn.type), sbField, sbProperty, sbCons, sbMethod);
        return sb;
    }

    public static void GenerateRegisterAll()
    {
        var sbA = new StringBuilder();
        foreach (var type in ExportTypeSet)
        {
            sbA.AppendFormat("        {0}.__Register();\n",
                JSNameMgr.GetTypeFileName(type));
        }
        var sb = new StringBuilder();
        sb.AppendFormat(REGISTER_HEADER, sbA);
        HandleStringFormat(sb);

        sb.Replace("\r\n", "\n");

        var writer2 = OpenFile(registerFile, false);
        writer2.Write(sb.ToString());
        writer2.Close();
    }

    public static void GenerateClass(Type type)
    {
        /*if (type.IsInterface)
        {
            Debug.Log("Interface: " + type.ToString() + " ignored.");
            return;
        }*/

        GeneratorHelp.ATypeInfo ti;
        /*int slot = */
        GeneratorHelp.AddTypeInfo(type, out ti);

        var ccbn = new ClassCallbackNames();
        {
            ccbn.type = type;
            ccbn.fields = new List<string>(ti.fields.Length);
            ccbn.properties = new List<string>(ti.properties.Length);
            ccbn.constructors = new List<string>(ti.constructors.Length);
            ccbn.methods = new List<string>(ti.methods.Length);

            //ccbn.constructorsCSParam = new List<string>(ti.constructors.Length);
            //ccbn.methodsCSParam = new List<string>(ti.methods.Length);
            ccbn.nameSpaces = new HashSet<string>
            {
                "UnityEngine",
                "System",
                "System.Collections",
                "System.Collections.Generic",
                "System.Reflection"
            };
        }

        _curTypeFileName = JSNameMgr.GetTypeFileName(type);

        var sbFields = BuildFields(type, ti.fields, ti.fieldsIndex, ccbn);
        var sbProperties = BuildProperties(type, ti.properties, ti.propertiesIndex, ccbn);
        var sbMethods = BuildMethods(type, ti.methods, ti.methodsIndex, ti.methodsOLInfo, ccbn);
        var sbCons = BuildConstructors(type, ti.constructors, ti.constructorsIndex, ccbn);
        var sbRegister = BuildRegisterFunction(ccbn, ti);
        var sbClass = BuildClass(type, sbFields, sbProperties, sbMethods, sbCons, sbRegister);

        var sbFile = new StringBuilder();
        var nsBuilder = new StringBuilder();
        if (type.Namespace != null)
        {
            ccbn.nameSpaces.Add(type.Namespace);
        }
        //Generate Using NameSpace
        foreach (string ns in ccbn.nameSpaces)
        {
            if (!string.IsNullOrEmpty(ns))
                nsBuilder.AppendLine("using " + ns + ";");
        }
        sbFile.AppendFormat(@"
//------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by CSGenerator.
// </auto-generated>
//------------------------------------------------------------------------------
{2}
using jsval = JSApi.jsval;

public class {0}
[[
{1}
]]
", _curTypeFileName, sbClass, nsBuilder);
        HandleStringFormat(sbFile);

        sbFile.Replace("\r\n", "\n");

        string fileName = JSPathSettings.csGeneratedDir + "/" +
                          JSNameMgr.GetTypeFileName(type) +
                          ".cs";
        var writer2 = OpenFile(fileName, false);
        writer2.Write(sbFile.ToString());
        writer2.Close();
    }

    private static StreamWriter OpenFile(string fileName, bool bAppend = false)
    {
        return new StreamWriter(fileName, bAppend, Encoding.UTF8);
    }

    private static void HandleStringFormat(StringBuilder sb)
    {
        sb.Replace("[[", "{");
        sb.Replace("]]", "}");
        sb.Replace("'", "\"");
    }

    public static void GetExportTypeSet(out HashSet<Type> typeSet, out HashSet<Type> enumSet)
    {
        typeSet = new HashSet<Type>();
        enumSet = new HashSet<Type>();
        var whitList = JSBCodeGenSettings.TypeWhiteSet;
        var blackList = JSBCodeGenSettings.TypeBlackSet;
        //Filter Custom Assembly Type
        foreach (var assemblyInfo in JSBCodeGenSettings.CustomAssemblyConfig)
        {
            try
            {
                var assembly = Assembly.Load(assemblyInfo.Key);
                var namespaces = assemblyInfo.Value;
                var types = assembly.GetExportedTypes();
                foreach (var type in types)
                {
                    //不导出dll中的接口类型
                    if (type.IsInterface)
                        continue;
                    if (type.IsGenericType && !type.IsGenericTypeDefinition)
                        continue;
                    if (type.IsDefined(typeof(ObsoleteAttribute), false))
                        continue;
                    if (namespaces != null)
                    {
                        //判断该类型是否到指定Namespace列表中
                        if (!namespaces.Contains(type.Namespace))
                            continue;

                        if (FilterType(type, whitList, blackList))
                            typeSet.Add(type);
                    }
                    else
                    {
                        if (FilterType(type, whitList, blackList))
                            typeSet.Add(type);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        foreach (var type in JSBCodeGenSettings.CustomTypeConfig)
        {
            if (typeSet.Contains(type))
            {
                Debug.LogError(string.Format("<{0}> is already existed", type));
            }
            else
            {
                //不能导出Enum类型
                if (type.IsEnum)
                    enumSet.Add(type);
                else if (FilterType(type, null, null))
                    typeSet.Add(type);
            }
        }
    }

    /// <summary>
    ///     返回true，代表导出此类型
    /// </summary>
    /// <param name="type"></param>
    /// <param name="whiteList"></param>
    /// <param name="blackList"></param>
    /// <returns></returns>
    public static bool FilterType(Type type, HashSet<Type> whiteList, List<string> blackList)
    {
        if (whiteList != null && whiteList.Contains(type))
            return true;
        if (blackList != null)
        {
            string fullName = type.FullName;
            if (blackList.Any(s => fullName.Contains(s)))
                return false;
        }

        //不能导出Enum类型
        if (type.IsEnum)
        {
            return false;
        }
        //不能导出Delegate类型
        if (typeof(Delegate).IsAssignableFrom(type))
        {
            return false;
        }
        //不能导出Attribute类型
        if (typeof(Attribute).IsAssignableFrom(type))
        {
            return false;
        }
        //不能导出带有JsType属性的类型
        if (JSSerializerEditor.WillTypeBeTranslatedToJavaScript(type))
        {
            return false;
        }
        //不能导出具体的泛型类，如List<string>,只能导出List<>
        if (type.IsGenericType && !type.IsGenericTypeDefinition)
        {
            return false;
        }

        return true;
    }

    public static void CheckClassBindings()
    {
        var clrLibrary = new HashSet<Type>
        {
            //
            // these types are defined in clrlibrary.javascript
            //
            typeof (object),
            typeof (Exception),
            typeof (SystemException),
            typeof (ValueType)
        };

        HashSet<Type> exportTypeSet;
        HashSet<Type> exportEnumSet;
        GetExportTypeSet(out exportTypeSet, out exportEnumSet);
        var logBuilder = new StringBuilder();
        var missingTypeSet = new HashSet<Type>();
        // Is BaseType exported?
        //检查是否有导出父类
        foreach (var type in exportTypeSet)
        {
            var baseType = type.BaseType;
            if (baseType == null) continue;
            if (baseType.IsGenericType) baseType = baseType.GetGenericTypeDefinition();
            // System.Object is already defined in SharpKit clrlibrary
            if (!clrLibrary.Contains(baseType) && !exportTypeSet.Contains(baseType))
            {
                logBuilder.AppendFormat("\"{0}\"\'s base type \"{1}\" must also be exported.\n",
                    type, baseType);
                missingTypeSet.Add(baseType);
            }

            // 检查 interface 有没有配置		
            var interfaces = type.GetInterfaces();
            for (int i = 0; i < interfaces.Length; i++)
            {
                var iType = interfaces[i];
                if (iType.IsPublic || iType.IsNestedPublic)
                {
                    string tiFullName = JSNameMgr.GetTypeFullName(iType);

                    // 这个检查有点奇葩
                    // 有些接口带 <>，这里直接忽略，不检查他
                    if (tiFullName.Contains("<") || tiFullName.Contains(">"))
                        continue;

                    if (!clrLibrary.Contains(iType) && !exportTypeSet.Contains(iType))
                    {
                        logBuilder.AppendFormat("\"{0}\"\'s interface \"{1}\" must also be exported.\n",
                            type, iType);
                        missingTypeSet.Add(iType);
                    }
                }
            }
        }

        if (logBuilder.Length > 0)
            Debug.LogError(logBuilder);
        exportTypeSet.UnionWith(missingTypeSet);
        ExportTypeSet = exportTypeSet;
        ExportEnumSet = exportEnumSet;
    }

    public static void GenerateClassBindings()
    {
        OnBegin();

        foreach (var type in ExportTypeSet)
        {
            GenerateClass(type);
        }
        GenerateRegisterAll();

        OnEnd();
    }

    public static void Type2TypeFlag(Type type, args argFlag)
    {
        if (type.IsByRef)
        {
            argFlag.Add("TypeFlag.IsRef");
            type = type.GetElementType();
        }

        if (type.IsGenericParameter)
        {
            argFlag.Add("TypeFlag.IsT");
        }
        else if (type.IsGenericType)
        {
            argFlag.Add("TypeFlag.IsGenericType");
        }

        if (type.IsArray)
            argFlag.Add("TypeFlag.IsArray");
    }

    public static args ParameterInfo2TypeFlag(ParameterInfo p)
    {
        var argFlag = new args();

        Type2TypeFlag(p.ParameterType, argFlag);

        if (p.IsOut)
            argFlag.Add("TypeFlag.IsOut");

        if (argFlag.Count == 0)
            argFlag.Add("TypeFlag.None");

        return argFlag;
    }

    #region Manual Function

    public static void InitManualFuncInfo()
    {
        _manualFuncDic.Clear();
    }

    public static bool IsManualFunction(string functionName)
    {
        bool ret;
        if (_manualFuncDic.TryGetValue(functionName, out ret))
            return ret;

        var method = typeof(UnityEngineManual).GetMethod(functionName);
        ret = method != null;
        _manualFuncDic[functionName] = ret;
        return ret;
    }

    public static void PrintManualFunctionInfo()
    {
        var sb = new StringBuilder();
        foreach (var v in _manualFuncDic)
        {
            if (v.Value)
                sb.AppendFormat("Manual: {0}\n", v.Key);
        }
        if (sb.Length > 0)
            Debug.Log(sb);
    }

    #endregion

    #region Nested type: ClassCallbackNames

    // used for record information
    public class ClassCallbackNames
    {
        public List<string> constructors;

        // genetated, generating CSParam code
        //public List<string> constructorsCSParam;
        public List<string> fields;
        public List<string> methods;
        //public List<string> methodsCSParam;

        public HashSet<string> nameSpaces;
        public List<string> properties;
        // class type
        public Type type;
    }

    #endregion

    #region Menu Option

    [MenuItem("JSB/Generate JS and CS Bindings", false, 1)]
    public static void GenerateJSCSBindings()
    {
        if (EditorApplication.isCompiling)
        {
            EditorUtility.DisplayDialog("Tip:",
                "please wait EditorApplication Compiling",
                "OK"
                );
            return;
        }

        CheckClassBindings();

        bool bContinue = EditorUtility.DisplayDialog("TIP",
            "Files in these directories will all be deleted and re-created: \n" +
            JSPathSettings.csGeneratedDir + "\n",
            "OK",
            "Cancel");
        if (!bContinue)
        {
            Debug.Log("Operation cancelled");
            return;
        }

        JSDataExchangeEditor.Reset();
        InitManualFuncInfo();
        GenerateClassBindings();
        JSGenerator.GenerateJsTypeBindings(ExportTypeSet, ExportEnumSet);
        PrintManualFunctionInfo();

        Debug.Log(string.Format("<color={1}>Generate CS Bindings OK. total = {0}</color>", ExportTypeSet.Count,
            "orange"));
        AssetDatabase.Refresh();
    }

    [MenuItem("JSB/Delete JS and CS Bindings", false, 1)]
    public static void DeleteAllJSCSBindings()
    {
        bool bContinue = EditorUtility.DisplayDialog("TIP",
            "Files in these directories will all be deleted: \n" +
            //JSBindingSettings.jsGeneratedDir + "\n" + 
            JSPathSettings.csGeneratedDir + "\n",
            "OK",
            "Cancel");
        if (!bContinue)
        {
            Debug.Log("Operation cancelled");
            return;
        }

        //删除所有C#导出类
        if (Directory.Exists(JSPathSettings.csGeneratedDir))
        {
            var files = Directory.GetFiles(JSPathSettings.csGeneratedDir);
            for (int i = 0; i < files.Length; i++)
            {
                File.Delete(files[i]);
            }
        }
        else
        {
            Directory.CreateDirectory(JSPathSettings.csGeneratedDir);
        }

        //清空Register接口注册
        var sb = new StringBuilder();
        sb.AppendFormat(REGISTER_HEADER, "");
        HandleStringFormat(sb);
        sb.Replace("\r\n", "\n");

        var fileWriter = OpenFile(registerFile, false);
        fileWriter.Write(sb.ToString());
        fileWriter.Close();

        //清空CSExportTypes.javascript脚本内容
        fileWriter = OpenFile(JSPathSettings.csExportJsFile, false);
        fileWriter.Write("this.Enum = {};\n");
        fileWriter.Close();

        AssetDatabase.Refresh();
    }

    #endregion
}