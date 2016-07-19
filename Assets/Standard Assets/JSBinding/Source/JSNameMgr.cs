using System;
using System.Collections.Generic;

//using UnityEditor;

public static class JSNameMgr
{
    private static readonly Dictionary<Type, string> _customJsTypeNames = new Dictionary<Type, string>
    {
        {typeof (List<>), "CSList"},
        {typeof (Dictionary<,>), "CSDictionary"},
        {typeof (KeyValuePair<,>), "CSKeyValuePair"},
        {typeof (Dictionary<,>.KeyCollection), "CSKeyCollection"},
        {typeof (Dictionary<,>.ValueCollection), "CSValueCollection"},
        {typeof (HashSet<>), "CSHashSet"},
    };

    public static readonly string[] GenericSuffix = { "`1", "`2", "`3", "`4", "`5" };
    public static readonly string[] GenericSuffixReplace = { "<>", "<,>", "<,,>", "<,,,>", "<,,,,>" };


    public static string GetTypeFileName(Type type)
    {
        string fullName = type.FullName.Replace('.', '_').Replace('`', '_').Replace('+', '_');
        return "JSB_" + fullName;
    }

    public static string HandleFunctionName(string functionName)
    {
        return functionName.Replace('<', '7').Replace('>', '7').Replace('`', 'A').Replace('.', '_');
    }

    public static string GetTypeFullName(Type type, bool withT = false)
    {
        if (type == null) return "";

        if (type.IsByRef)
            type = type.GetElementType();

        //泛型参数 T
        if (type.IsGenericParameter)
        {
            return type.Name;
        }

        //常规类型
        if (!type.IsGenericType && !type.IsGenericTypeDefinition)
        {
            string fullName = type.FullName;
            if (fullName == null)
            {
                fullName = ">>>>>>>>>>>?????????????????/";
            }
            fullName = fullName.Replace('+', '.');
            return fullName;
        }

        //泛型类定义 List<T>
        int length = 0;
        if (type.IsGenericTypeDefinition)
        {
            var t = type.IsGenericTypeDefinition ? type : type.GetGenericTypeDefinition();
            string fullName = t.FullName;
            if (!withT)
            {
                for (var i = 0; i < GenericSuffix.Length; i++)
                    fullName = fullName.Replace(GenericSuffix[i], GenericSuffixReplace[i]);
                return fullName.Replace('+', '.');
            }
            length = fullName.Length;
            if (length > 2 && fullName[length - 2] == '`')
            {
                fullName = fullName.Substring(0, length - 2);
                Type[] ts = type.GetGenericArguments();
                fullName += "<";
                for (int i = 0; i < ts.Length; i++)
                {
                    fullName += GetTypeFullName(ts[i]); // it's T
                    if (i != ts.Length - 1)
                    {
                        fullName += ", ";
                    }
                }
                fullName += ">";
            }
            return fullName.Replace('+', '.');
        }
        string parentName = string.Empty;
        if (type.IsNested)
        {
            parentName = GetTypeFullName(type.DeclaringType, withT) + ".";
        }

        string Name = type.Name;
        length = Name.Length;
        if (length > 2 && Name[length - 2] == '`')
        {
            Name = Name.Substring(0, length - 2);
            Type[] ts = type.GetGenericArguments();
            Name += "<";
            for (int i = 0; i < ts.Length; i++)
            {
                Name += GetTypeFullName(ts[i]); // it's T
                if (i != ts.Length - 1)
                {
                    Name += ", ";
                }
            }
            Name += ">";
        }
        return (parentName + Name).Replace('+', '.');
    }

    public static string GetJSTypeFullName(Type type)
    {
        if (type == null) return "";

        if (type.IsByRef)
            type = type.GetElementType();

        //泛型参数 T
        if (type.IsGenericParameter)
        {
            return "object";
        }

        //常规类型
        if (!type.IsGenericType && !type.IsGenericTypeDefinition)
        {
            string fullName = type.FullName;
            if (string.IsNullOrEmpty(fullName))
            {
                return "object";
            }
            fullName = fullName.Replace('+', '.');
            return fullName;
        }

        //泛型类型
        if (type.IsGenericTypeDefinition || type.IsGenericType)
        {
            // ATTENSION
            // typeof(List<>).FullName    == System.Collections.Generic.List`1
            // typeof(List<int>).FullName == Systcem.Collections.Generic.List`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]

            Type genericType = type.IsGenericTypeDefinition ? type : type.GetGenericTypeDefinition();
            if (_customJsTypeNames.ContainsKey(genericType))
            {
                return _customJsTypeNames[genericType];
            }
            return genericType.FullName.Replace('`', '$').Replace('+', '.');
        }

        string fatherName = type.Name.Substring(0, type.Name.Length - 2);
        Type[] ts = type.GetGenericArguments();
        fatherName += "<";
        for (int i = 0; i < ts.Length; i++)
        {
            fatherName += ts[i].Name;
            if (i != ts.Length - 1)
                fatherName += ", ";
        }
        fatherName += ">";
        fatherName.Replace('+', '.');
        return fatherName;
    }
}