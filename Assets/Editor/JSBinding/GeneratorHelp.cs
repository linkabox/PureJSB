using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

/// <summary>
/// </summary>
public static class GeneratorHelp
{
    public static List<ATypeInfo> allTypeInfo = new List<ATypeInfo>();

    public static BindingFlags BindingFlagsMethod =
        BindingFlags.Public
        | BindingFlags.Instance
        | BindingFlags.Static
        | BindingFlags.DeclaredOnly;

    public static BindingFlags BindingFlagsProperty =
        BindingFlags.Public
        | BindingFlags.GetProperty
        | BindingFlags.SetProperty
        | BindingFlags.Instance
        | BindingFlags.Static
        | BindingFlags.DeclaredOnly;

    public static BindingFlags BindingFlagsField =
        BindingFlags.Public
        | BindingFlags.GetField
        | BindingFlags.SetField
        | BindingFlags.Instance
        | BindingFlags.Static
        | BindingFlags.DeclaredOnly;

    public static void ClearTypeInfo()
    {
        //         CallbackInfo cbi = new CallbackInfo();
        //         cbi.fields = new List<CSCallbackField>();
        //         cbi.fields.Add(Vector3Generated.Vector3_x);

        allTypeInfo.Clear();
    }

    public static int AddTypeInfo(Type type)
    {
        var tiOut = new ATypeInfo();
        return AddTypeInfo(type, out tiOut);
    }

    public static int AddTypeInfo(Type type, out ATypeInfo tiOut)
    {
        var ti = new ATypeInfo();
        ti.fields = type.GetFields(BindingFlagsField);
        ti.properties = type.GetProperties(BindingFlagsProperty);
        ti.methods = type.GetMethods(BindingFlagsMethod);
        ti.constructors = type.GetConstructors();
        if (JSBCodeGenSettings.NeedGenDefaultConstructor(type))
        {
            // null means it's default constructor
            var l = new List<ConstructorInfo>();
            l.Add(null);
            l.AddRange(ti.constructors);
            ti.constructors = l.ToArray();
        }
        //ti.howmanyConstructors = ti.constructors.Length;

        FilterTypeInfo(type, ti);

        int slot = allTypeInfo.Count;
        allTypeInfo.Add(ti);
        tiOut = ti;
        return slot;
    }

    public static bool IsMemberObsolete(MemberInfo mi)
    {
        var attrs = mi.GetCustomAttributes(true);
        for (int j = 0; j < attrs.Length; j++)
        {
            if (attrs[j] is ObsoleteAttribute)
            {
                return true;
            }
        }
        return false;
    }

    public static int MethodInfoComparison(MethodInfoAndIndex mi1, MethodInfoAndIndex mi2)
    {
        var m1 = mi1.method;
        var m2 = mi2.method;

        if (!m1.IsStatic && m2.IsStatic)
            return -1;
        if (m1.IsStatic && !m2.IsStatic)
            return 1;
        if (m1.Name != m2.Name)
            return string.Compare(m1.Name, m2.Name);
        int max1 = 0;
        {
            var ps = m1.GetParameters();
            if (ps.Length > 0) max1 = ps.Length;
            for (int i = ps.Length - 1; i >= 0; i--)
            {
                if (!ps[i].IsOptional)
                    break;
                max1--;
            }
        }
        int max2 = 0;
        {
            var ps = m2.GetParameters();
            if (ps.Length > 0) max2 = ps.Length;
            for (int i = ps.Length - 1; i >= 0; i--)
            {
                if (!ps[i].IsOptional)
                    break;
                max2--;
            }
        }
        if (max1 > max2) return -1;
        if (max2 > max1) return 1;
        return 0;
    }

    public static void FilterTypeInfo(Type type, ATypeInfo ti)
    {
        bool bIsStaticClass = type.IsClass && type.IsAbstract && type.IsSealed;
        bool bIsAbstractClass = type.IsClass && type.IsAbstract;

        var lstCons = new List<ConstructorInfoAndIndex>();
        var lstField = new List<FieldInfoAndIndex>();
        var lstPro = new List<PropertyInfoAndIndex>();
        var pMethodNameSet = new HashSet<string>();
        var lstMethod = new List<MethodInfoAndIndex>();

        for (int i = 0; i < ti.constructors.Length; i++)
        {
            if (bIsAbstractClass)
                continue;

            if (ti.constructors[i] == null)
            {
                lstCons.Add(new ConstructorInfoAndIndex(null, i));
                continue;
            }

            // don't generate MonoBehaviour constructor
            if (type == typeof (MonoBehaviour))
            {
                continue;
            }

            if (!IsMemberObsolete(ti.constructors[i]) &&
                !JSBCodeGenSettings.IsDiscardMemberInfo(type, ti.constructors[i]))
            {
                lstCons.Add(new ConstructorInfoAndIndex(ti.constructors[i], i));
            }
        }

        for (int i = 0; i < ti.fields.Length; i++)
        {
            if (typeof (Delegate).IsAssignableFrom(ti.fields[i].FieldType.BaseType))
            {
                //Debug.Log("[field]" + type.ToString() + "." + ti.fields[i].Name + "is delegate!");
            }
            if (ti.fields[i].FieldType.ContainsGenericParameters)
                continue;

            if (!IsMemberObsolete(ti.fields[i]) && !JSBCodeGenSettings.IsDiscardMemberInfo(type, ti.fields[i]))
            {
                lstField.Add(new FieldInfoAndIndex(ti.fields[i], i));
            }
        }


        for (int i = 0; i < ti.properties.Length; i++)
        {
            var pro = ti.properties[i];

            if (typeof (Delegate).IsAssignableFrom(pro.PropertyType.BaseType))
            {
                // Debug.Log("[property]" + type.ToString() + "." + pro.Name + "is delegate!");
            }

            var accessors = pro.GetAccessors();
            foreach (var v in accessors)
            {
                if (!pMethodNameSet.Contains(v.Name))
                    pMethodNameSet.Add(v.Name);
            }


            //            if (pro.GetIndexParameters().Length > 0)
            //                continue;
            //            if (pro.Name == "Item") //[] not support
            //                continue;

            // Skip Obsolete
            if (IsMemberObsolete(pro))
                continue;

            if (JSBCodeGenSettings.IsDiscardMemberInfo(type, pro))
                continue;

            lstPro.Add(new PropertyInfoAndIndex(pro, i));
        }


        for (int i = 0; i < ti.methods.Length; i++)
        {
            var method = ti.methods[i];

            // skip non-static method in static class
            if (bIsStaticClass && !method.IsStatic)
            {
                continue;
            }

            // skip property accessor
            if (method.IsSpecialName &&
                pMethodNameSet.Contains(method.Name))
            {
                continue;
            }

            if (method.IsSpecialName)
            {
                if (method.Name == "op_Addition" ||
                    method.Name == "op_Subtraction" ||
                    method.Name == "op_UnaryNegation" ||
                    method.Name == "op_Multiply" ||
                    method.Name == "op_Division" ||
                    method.Name == "op_Equality" ||
                    method.Name == "op_Inequality" ||
                    method.Name == "op_LessThan" ||
                    method.Name == "op_LessThanOrEqual" ||
                    method.Name == "op_GreaterThan" ||
                    method.Name == "op_GreaterThanOrEqual" ||
                    method.Name == "op_Implicit")
                {
                    if (!method.IsStatic)
                    {
                        Debug.Log("IGNORE not-static special-name function: " + type.Name + "." + method.Name);
                        continue;
                    }
                }
                else if (method.Name.StartsWith("add_") || method.Name.StartsWith("remove_"))
                {
                }
                else
                {
                    Debug.Log("IGNORE special-name function:" + type.Name + "." + method.Name);
                    continue;
                }
            }

            // Skip Obsolete
            if (IsMemberObsolete(method))
                continue;

            ParameterInfo[] ps;
            bool bDiscard = false;

            //
            // ignore static method who contains T coming from class type
            // because there is no way to call it
            // SharpKit doesn't give c# the type of T
            //
            if (method.IsGenericMethodDefinition /* || method.IsGenericMethod*/
                && method.IsStatic)
            {
                ps = method.GetParameters();
                for (int k = 0; k < ps.Length; k++)
                {
                    if (ps[k].ParameterType.ContainsGenericParameters)
                    {
                        var Ts = JSDataExchangeMgr.RecursivelyGetGenericParameters(ps[k].ParameterType);
                        foreach (var t in Ts)
                        {
                            if (t.DeclaringMethod == null)
                            {
                                bDiscard = true;
                                break;
                            }
                        }
                        if (bDiscard)
                            break;
                    }
                }
                if (bDiscard)
                {
                    Debug.LogWarning("Ignore static method " + type.Name + "." + method.Name);
                    continue;
                }
            }

            // does it have unsafe parameter?
            bDiscard = false;
            ps = method.GetParameters();
            for (int k = 0; k < ps.Length; k++)
            {
                var pt = ps[k].ParameterType;
                while (true)
                {
                    if (pt.IsPointer)
                    {
                        bDiscard = true;
                        break;
                    }
                    if (pt.HasElementType)
                        pt = pt.GetElementType();
                    else
                        break;
                }

                if (bDiscard)
                    break;
            }
            if (bDiscard)
            {
                Debug.Log(type.Name + "." + method.Name + " was discard because it has unsafe parameter.");
                continue;
            }


            if (JSBCodeGenSettings.IsDiscardMemberInfo(type, method))
                continue;

            lstMethod.Add(new MethodInfoAndIndex(method, i));
        }

        if (lstMethod.Count == 0)
            ti.methodsOLInfo = null;
        else
        {
            // sort methods
            lstMethod.Sort(MethodInfoComparison);
            ti.methodsOLInfo = new int[lstMethod.Count];
        }

        int overloadedIndex = 1;
        bool bOL = false;
        for (int i = 0; i < lstMethod.Count; i++)
        {
            ti.methodsOLInfo[i] = 0;
            if (bOL)
            {
                ti.methodsOLInfo[i] = overloadedIndex;
            }

            if (i < lstMethod.Count - 1 && lstMethod[i].method.Name == lstMethod[i + 1].method.Name &&
                ((lstMethod[i].method.IsStatic && lstMethod[i + 1].method.IsStatic) ||
                 (!lstMethod[i].method.IsStatic && !lstMethod[i + 1].method.IsStatic)))
            {
                if (!bOL)
                {
                    overloadedIndex = 1;
                    bOL = true;
                    ti.methodsOLInfo[i] = overloadedIndex;
                }
                overloadedIndex++;
            }
            else
            {
                bOL = false;
                overloadedIndex = 1;
            }
        }

        ti.constructors = new ConstructorInfo[lstCons.Count];
        ti.constructorsIndex = new int[lstCons.Count];
        for (int k = 0; k < lstCons.Count; k++)
        {
            ti.constructors[k] = lstCons[k].method;
            ti.constructorsIndex[k] = lstCons[k].index;
        }

        // ti.fields = lstField.ToArray();
        ti.fields = new FieldInfo[lstField.Count];
        ti.fieldsIndex = new int[lstField.Count];
        for (int k = 0; k < lstField.Count; k++)
        {
            ti.fields[k] = lstField[k].method;
            ti.fieldsIndex[k] = lstField[k].index;
        }

        // ti.properties = lstPro.ToArray();
        ti.properties = new PropertyInfo[lstPro.Count];
        ti.propertiesIndex = new int[lstPro.Count];
        for (int k = 0; k < lstPro.Count; k++)
        {
            ti.properties[k] = lstPro[k].method;
            ti.propertiesIndex[k] = lstPro[k].index;
        }

        ti.methods = new MethodInfo[lstMethod.Count];
        ti.methodsIndex = new int[lstMethod.Count];

        for (int k = 0; k < lstMethod.Count; k++)
        {
            ti.methods[k] = lstMethod[k].method;
            ti.methodsIndex[k] = lstMethod[k].index;
        }
    }

    /// <summary>
    ///     Method is overloaded or not?
    ///     Used in JSGenerator2
    ///     No matter it's static or not
    ///     including NonPublic methods
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="methodName">Name of the method.</param>
    /// <returns></returns>
    public static bool MethodIsOverloaded(Type type, MethodInfo methodInfo)
    {
        string methodName = methodInfo.Name;
        bool ret = HasOverloadedMethod(type, methodName, BindingFlags.Public
                                                         | BindingFlags.Static
                                                         | BindingFlags.Instance);
        if (!ret)
        {
            ret = HasOverloadedMethod(type, methodName, BindingFlags.Public
                                                        | BindingFlags.Static
                                                        | BindingFlags.FlattenHierarchy);
        }

        if (ret)
        {
            Debug.Log("NEW OVERLOAD " + type.Name + "." + methodName);
        }
        return ret;
    }

    private static bool HasOverloadedMethod(Type type, string methodName, BindingFlags flag)
    {
        var methods = type.GetMethods(flag);
        int count = 0;
        foreach (var m in methods)
        {
            if (m.Name == methodName)
            {
                count++;
                if (count >= 2)
                    return true;
            }
        }
        return false;
    }

    #region Nested type: ATypeInfo

    /// <summary>
    ///     usage
    ///     1 used for generating javascript code
    ///     2 used for generating c# code
    /// </summary>
    public class ATypeInfo
    {
        public ConstructorInfo[] constructors;
        public int[] constructorsIndex;
        public FieldInfo[] fields;
        public int[] fieldsIndex;

        public MethodInfo[] methods;

        public int[] methodsIndex;
            // index of the method in array of type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);

        public int[] methodsOLInfo; //0 not overloaded >0 overloaded index

        public PropertyInfo[] properties;
        public int[] propertiesIndex;
        //public int howmanyConstructors;//how many constructors actually, (before filtering).
    }

    #endregion

    #region Nested type: ConstructorInfoAndIndex

    public struct ConstructorInfoAndIndex
    {
        public ConstructorInfo method;
        public int index;

        public ConstructorInfoAndIndex(ConstructorInfo _m, int _i)
        {
            method = _m;
            index = _i;
        }
    }

    #endregion

    #region Nested type: FieldInfoAndIndex

    public struct FieldInfoAndIndex
    {
        public FieldInfo method;
        public int index;

        public FieldInfoAndIndex(FieldInfo _m, int _i)
        {
            method = _m;
            index = _i;
        }
    }

    #endregion

    #region Nested type: MethodInfoAndIndex

    public struct MethodInfoAndIndex
    {
        public MethodInfo method;
        public int index;

        public MethodInfoAndIndex(MethodInfo _m, int _i)
        {
            method = _m;
            index = _i;
        }
    }

    #endregion

    #region Nested type: PropertyInfoAndIndex

    public struct PropertyInfoAndIndex
    {
        public PropertyInfo method;
        public int index;

        public PropertyInfoAndIndex(PropertyInfo _m, int _i)
        {
            method = _m;
            index = _i;
        }
    }

    #endregion
}