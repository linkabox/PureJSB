using System;
using System.Text;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Reflection;

public class MemberID
{
    // -1: initial 
    // >=0: success
    public int index = -1;
}

public enum TypeFlag
{
    None = 0,
    IsRef = 1, // only for parameter
    IsOut = 2, // only for parameter
    IsT = 4, // for example: T Load<T>();  the return type is T
    IsGenericType = 8,
    IsArray = 16,
}
public class ConstructorID : MemberID
{
    public string[] paramTypeNames; // can be null
    public TypeFlag[] paramFlags; // can be null
    // constructor
    public ConstructorID(string[] paramTypes, TypeFlag[] paramFlags)
    {
        this.paramTypeNames = paramTypes;
        this.paramFlags = paramFlags;
    }
}
public class FieldID : MemberID
{
    public string name; //memberName
    // property method
    public FieldID(string name)
    {
        this.name = name;
    }
}
public class PropertyID : ConstructorID
{
    public string name; //memberName

    // these 2 are not used for now
    public string retTypeName;
    public TypeFlag retTypeFlag;

    public PropertyID(string name, string returnType, TypeFlag returnTypeFlag, string[] parameterTypes, TypeFlag[] typeFlags)
        : base(parameterTypes, typeFlags)
    {
        this.name = name;
        this.retTypeName = returnType;
        this.retTypeFlag = returnTypeFlag;
    }
}
public class MethodID : PropertyID
{
    public MethodID(string name, string retTypeName, TypeFlag returnTypeFlag, string[] parameterTypes, TypeFlag[] typeFlags)
        : base(name, retTypeName, returnTypeFlag, parameterTypes, typeFlags)
    {
    }
}

class GenericTypeCache
{
    class TypeMembers
    {
        public ConstructorInfo[] cons = null;
        public FieldInfo[] fields = null;
        public PropertyInfo[] properties = null;
        public MethodInfo[] methods = null;
    }

    //
    //  can not use DeclaredOnly !!!
    //  Generic Class may be object's base class
    //
    static BindingFlags BindingFlagsMethod =
        BindingFlags.Public
        | BindingFlags.Instance
        | BindingFlags.Static
        /*| BindingFlags.DeclaredOnly*/;

    static BindingFlags BindingFlagsProperty =
        BindingFlags.Public
        | BindingFlags.GetProperty
        | BindingFlags.SetProperty
        | BindingFlags.Instance
        | BindingFlags.Static
        /*| BindingFlags.DeclaredOnly*/;

    static BindingFlags BindingFlagsField =
        BindingFlags.Public
        | BindingFlags.GetField
        | BindingFlags.SetField
        | BindingFlags.Instance
        | BindingFlags.Static
        /*| BindingFlags.DeclaredOnly*/;

    static Dictionary<Type, TypeMembers> dict = new Dictionary<Type, TypeMembers>();
    static TypeMembers getMembers(Type type)
    {
        TypeMembers tm;
        if (dict.TryGetValue(type, out tm))
        {
            return tm;
        }
        tm = new TypeMembers();
        tm.cons = type.GetConstructors();
        tm.fields = type.GetFields(GenericTypeCache.BindingFlagsField);
        tm.properties = type.GetProperties(GenericTypeCache.BindingFlagsProperty);
        tm.methods = type.GetMethods(GenericTypeCache.BindingFlagsMethod);

        dict.Add(type, tm);
        return tm;
    }
    static bool matchReturnType(Type returnType, string typeName, bool isGenericType)
    {
        //返回值不是泛型参数，或者不是泛型类时，判断返回值类型名是否一致
        if (!isGenericType)
            return (returnType.Name == typeName);

        return true;
    }
    static bool matchParameters(ParameterInfo[] pi, string[] paramTypeNames, TypeFlag[] typeFlags)
    {
        int paramLen = (paramTypeNames == null ? 0 : paramTypeNames.Length);
        int flagLen = (typeFlags == null ? 0 : typeFlags.Length);

        if (paramLen == 0)
        {
            if (!(pi == null || pi.Length == 0))
            {
                return false;
            }
        }

        if (pi.Length != paramLen)
        {
            return false;
        }

        for (var i = 0; i < pi.Length; i++)
        {
            Type real_t = pi[i].ParameterType;
            TypeFlag flag = (flagLen > i ? typeFlags[i] : TypeFlag.None);

            bool byRef = 0 != (flag & TypeFlag.IsRef);
            if ((byRef && !real_t.IsByRef) || (!byRef && real_t.IsByRef))
                return false;

            bool isOut = 0 != (flag & TypeFlag.IsOut);
            if ((isOut && !pi[i].IsOut) || (!isOut && pi[i].IsOut))
                return false;

            if (byRef || isOut)
            {
                real_t = real_t.GetElementType();
            }

            bool isArray = 0 != (flag & TypeFlag.IsArray);
            if (isArray)
            {
                if (!pi[i].ParameterType.IsArray)
                    return false;
            }
            else
            {
                bool isGT = 0 != (flag & TypeFlag.IsGenericType);
                if (isGT)
                {
                    if (!real_t.IsGenericType)
                        return false;
                    if (real_t.GetGenericTypeDefinition().Name != paramTypeNames[i])
                        return false;
                }
                else
                {
                    bool isT = 0 != (flag & TypeFlag.IsT);
                    if (!isT)
                    {
                        if (real_t.Name != paramTypeNames[i])
                            return false;
                    }
                }
            }
        }
        return true;
    }

    public static ConstructorInfo getConstructor(Type type, ConstructorID id)
    {
        TypeMembers tmember = getMembers(type);
        var arr = tmember.cons;
        if (arr != null)
        {
            for (var i = -1; i < arr.Length; i++)
            {
                ConstructorInfo curr;
                if (i == -1)
                {
                    if (id.index >= 0 && arr.Length > id.index)
                        curr = arr[id.index];
                    else
                        continue;
                }
                else
                {
                    curr = arr[i];
                }
                if (matchParameters(curr.GetParameters(), id.paramTypeNames, id.paramFlags))
                {
                    if (i != -1)
                        id.index = i;
                    return curr;
                }
            }
        }
        Debug.LogError(new StringBuilder().AppendFormat("GenericTypeCache.getConstructor({0}) fail", type.Name));
        return null;
    }
    public static FieldInfo getField(Type type, FieldID id)
    {
        TypeMembers tmember = getMembers(type);
        var arr = tmember.fields;
        if (arr != null)
        {
            for (var i = -1; i < arr.Length; i++)
            {
                FieldInfo curr;
                if (i == -1)
                {
                    if (id.index >= 0 && arr.Length > id.index)
                        curr = arr[id.index];
                    else
                        continue;
                }
                else
                {
                    curr = arr[i];
                }

                if (curr.Name == id.name)
                {
                    if (i != -1)
                        id.index = i;
                    return curr;
                }
            }
        }
        Debug.LogError(new StringBuilder().AppendFormat("GenericTypeCache.getField({0}, {1}) fail", type.Name, id.name));
        return null;
    }
    public static PropertyInfo getProperty(Type type, PropertyID id)
    {
        TypeMembers tmember = getMembers(type);
        var arr = tmember.properties;
        if (arr != null)
        {
            for (var i = -1; i < arr.Length; i++)
            {
                PropertyInfo curr;
                if (i == -1)
                {
                    if (id.index >= 0 && arr.Length > id.index)
                        curr = arr[id.index];
                    else
                        continue;
                }
                else
                {
                    curr = arr[i];
                }
                if (curr.Name == id.name)
                {
                    if (matchReturnType(curr.PropertyType, id.retTypeName, curr.DeclaringType.IsGenericType) &&
                        matchParameters(curr.GetIndexParameters(), id.paramTypeNames, id.paramFlags))
                    {
                        if (i != -1)
                            id.index = i;
                        return curr;
                    }
                }
            }
        }
        Debug.LogError(new StringBuilder().AppendFormat("GenericTypeCache.getProperty({0}, {1}) fail", type.Name, id.name));
        return null;
    }
    public static MethodInfo getMethod(Type type, MethodID id)
    {
        TypeMembers tmember = getMembers(type);
        var arr = tmember.methods;
        if (arr != null)
        {
            for (var i = -1; i < arr.Length; i++)
            {
                MethodInfo curr;
                if (i == -1)
                {
                    if (id.index >= 0 && arr.Length > id.index)
                        curr = arr[id.index];
                    else
                        continue;
                }
                else
                {
                    curr = arr[i];
                }

                if (curr.Name == id.name) // method name
                {
                    if (matchReturnType(curr.ReturnType, id.retTypeName, curr.DeclaringType.IsGenericType) &&
                        matchParameters(curr.GetParameters(), id.paramTypeNames, id.paramFlags))
                    {
                        if (i != -1)
                            id.index = i;
                        return curr;
                    }
                }
            }
        }
        Debug.LogError(new StringBuilder().AppendFormat("GenericTypeCache.getMethod({0}, {1}) fail", type.Name, id.name));
        return null;
    }
}