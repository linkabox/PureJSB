using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SharpKit.JavaScript;
using UnityEngine;
using Object = UnityEngine.Object;

public class JSDataExchangeMgr
{
    #region Delegates

    public delegate T DGetV<T>();

    #endregion

    public enum eGetType
    {
        GetARGV,
        GetARGVRefOut,
        GetJSFUNRET,
        Jsval
    }

    public enum eSetType
    {
        SetRval,
        UpdateARGVRefOut,
        Jsval
        //GetJSFUNRET
    }

    private static readonly Dictionary<string, Type> typeCache = new Dictionary<string, Type>();

    public static Type GetTypeByJsParam(int e)
    {
        string typeFullName = JSApi.getStringS(e);
        return GetTypeByName(typeFullName);
    }

    public static Type GetTypeByName(string typeName, Type defaultType = null)
    {
        if (string.IsNullOrEmpty(typeName))
            return null;

        Type t = null;
        if (!typeCache.TryGetValue(typeName, out t))
        {
            t = Type.GetType(typeName);
            if (t == null)
            {
                foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
                {
                    t = a.GetType(typeName);
                    if (t != null)
                        break;
                }
            }

            //忽略带JsType属性的类型
            if (t != null)
            {
                string fullName = t.FullName;
                if (JSCache.IsJsComTypeName(fullName))
                {
                    t = typeof (JSComponent);
                }
                else if (t.IsDefined(typeof(JsTypeAttribute), false) || JSCache.IsJsTypeName(t.FullName))
                {
                    t = null;
                }
            }

            typeCache[typeName] = t; // perhaps null
            //if (t == null)
            //{
            //    Debug.LogError("GetType of \"" + typeName + "\" is null. Did you export that class to JavaScript?");
            //}
        }
        if (t == null)
        {
            return defaultType; // typeof(CSRepresentedObject);
        }
        return t;
    }


    /*
    // Runtime Only
    // type: class type
    // methodName: method name
    // TCount: generic parameter count
    // vc: JSVCall instance
    public static MethodInfo MakeGenericConstructor(Type type, int TCount, int paramCount, JSVCall vc)
    {
        // Get generic method by name and param count.
        ConstructorInfo conT = JSDataExchangeMgr.GetGenericConstructorInfo(type, TCount, paramCount);
        if (conT == null)
        {
            return null;
        }

        // get T types
        Type[] genericTypes = new Type[TCount];
        for (int i = 0; i < TCount; i++)
        {
            // Get generic types from js.
            System.Type t = JSDataExchangeMgr.GetTypeByName(JSMgr.datax.getString(JSDataExchangeMgr.eGetType.GetARGV));
            genericTypes[i] = t;
            if (t == null)
            {
                return null;
            }
        }

        // Make generic method.
        MethodInfo method = methodT.MakeGenericMethod(genericTypes);
        return method;
    }
    // Runtime Only
    // called by MakeGenericConstructor
    // get generic Constructor by matching TCount,paramCount, if more than 1 match, return null.
    static ConstructorInfo GetGenericConstructorInfo(Type type, int TCount, int paramCount)
    {
        ConstructorInfo[] constructors = type.GetConstructors();
        if (constructors == null || constructors.Length == 0)
        {
            return null;
        }

        ConstructorInfo con = null;
        for (int i = 0; i < constructors.Length; i++)
        {
            if (constructors[i].IsGenericMethodDefinition &&
                constructors[i].GetGenericArguments().Length == TCount &&
                constructors[i].GetParameters().Length == paramCount)
            {
                if (con == null)
                    con = constructors[i];
                else
                {
                    Debug.LogError("More than 1 Generic Constructor found!!! " + GetTypeFullName(type) + "." + name);
                    return null;
                }
            }
        }
        if (con == null)
        {
            Debug.LogError("No generic constructor found! " + GetTypeFullName(type));
        }
        return con;
    }*/

    public static ConstructorInfo makeGenericConstructor(Type type, ConstructorID constructorID)
    {
        int tCount = type.GetGenericArguments().Length;
        var genericTypes = new Type[tCount];
        for (int i = 0; i < tCount; i++)
        {
            string typeName = JSApi.getStringS((int) JSApi.GetType.Arg);
            var t = GetTypeByName(typeName, typeof (CSRepresentedObject));
            genericTypes[i] = t;
            if (t == null)
            {
                return null;
            }
        }

        var exactType = type.MakeGenericType(genericTypes);
        var exactConstructor = GenericTypeCache.getConstructor(exactType, constructorID);
        return exactConstructor;
    }

    /// <summary>
    ///     Makes the generic method.
    ///     not run in Editor mode, only runtime.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="methodID">The method identifier.</param>
    /// <param name="TCount">The t count.</param>
    /// <returns></returns>
    public static MethodInfo makeGenericMethod(Type type, MethodID methodID, int TCount)
    {
        var methodT = GenericTypeCache.getMethod(type, methodID);
        if (methodT == null)
        {
            return null;
        }

        var genericTypes = new Type[TCount];
        for (int i = 0; i < TCount; i++)
        {
            string typeName = JSApi.getStringS((int) JSApi.GetType.Arg);
            var t = GetTypeByName(typeName, typeof (CSRepresentedObject));
            genericTypes[i] = GetGenericTypeBaseType(t);
            if (t == null)
            {
                return null;
            }
        }

        var method = methodT.MakeGenericMethod(genericTypes);
        return method;
    }

    /// <summary>
    ///     递归遍历获取带泛型参数类型的基类，例如：TweenerCore<T1, T2, TPlugOptions>,返回Tweener
    /// </summary>
    /// <returns>The generic type base type.</returns>
    /// <param name="type">Type.</param>
    public static Type GetGenericTypeBaseType(Type type)
    {
        if (!type.ContainsGenericParameters)
            return type;

        var baseType = type.BaseType;
        if (baseType == null)
        {
            return typeof (object);
        }

        return GetGenericTypeBaseType(baseType);
    }

    //
    // 
    //
    public static Type[] RecursivelyGetGenericParameters(Type type, List<Type> lst = null)
    {
        if (lst == null)
            lst = new List<Type>();

        if (type.ContainsGenericParameters)
        {
            if (type.IsGenericParameter)
            {
                lst.Add(type);
            }
            else if (type.HasElementType)
            {
                RecursivelyGetGenericParameters(type.GetElementType(), lst);
            }
            else if (type.IsGenericType)
            {
                var genericArguments = type.GetGenericArguments();
                for (int i = 0; i < genericArguments.Length; i++)
                {
                    RecursivelyGetGenericParameters(genericArguments[i], lst);
                }
            }
        }
        return lst.ToArray();
    }

    public static T GetJSArg<T>(DGetV<T> del)
    {
        return del();
    }

    #region Get Operation

    public object getObject(int e)
    {
        int jsObjID = JSApi.getObject(e);
        if (jsObjID == 0)
        {
            // no error
            return null;
        }

        var csObj = JSMgr.getCSObj(jsObjID);
        if (csObj == null)
        {
            csObj = new CSRepresentedObject(jsObjID);
        }
        return csObj;
    }

    /// <summary>
    ///     Gets object by type.
    ///     for concrete type e.g. setInt32 setString, they know how to return object
    ///     but for T parameters, type is unknown until runtime
    ///     so this function needs a 'Type' argument
    ///     which is passed through mTempObj
    /// </summary>
    /// <param name="eType">Type of the e.</param>
    /// <returns></returns>
    public object getByType(JSApi.GetType eType)
    {
        int e = (int) eType;
        Type type = null;
        if (type.IsByRef)
            type = type.GetElementType();

        if (type == typeof (string))
            return JSApi.getStringS(e);
        if (type.IsEnum)
            return JSApi.getEnum(e);
        if (type.IsPrimitive)
        {
            if (type == typeof (bool))
                return JSApi.getBooleanS(e);
            if (type == typeof (char))
                return JSApi.getChar(e);
            if (type == typeof (byte))
                return JSApi.getByte(e);
            if (type == typeof (sbyte))
                return JSApi.getSByte(e);
            if (type == typeof (ushort))
                return JSApi.getUInt16(e);
            if (type == typeof (short))
                return JSApi.getInt16(e);
            if (type == typeof (uint))
                return JSApi.getUInt32(e);
            if (type == typeof (int))
                return JSApi.getInt32(e);
            if (type == typeof (ulong))
                return JSApi.getUInt64(e);
            if (type == typeof (long))
                return JSApi.getInt64(e);
            if (type == typeof (float))
                return JSApi.getSingle(e);
            if (type == typeof (double))
                return JSApi.getDouble(e);
            if (type == typeof (IntPtr))
                return JSApi.getIntPtr(e);
            Debug.LogError("Unknown primitive type" + type.Name);
        }
        else if (type == typeof (Vector2))
        {
            return JSApi.getVector2S(e);
        }
        else if (type == typeof (Vector3))
        {
            return JSApi.getVector3S(e);
        }
        else
        {
            return JSApi.getObject(e);
        }
        return null;
    }

    public object getWhatever(int e)
    {
        uint tag = JSApi.getTag(e);
        if (JSApi.jsval.isNullOrUndefined(tag))
            return null;
        if (JSApi.jsval.isBoolean(tag))
            return JSApi.getBooleanS(e);
        if (JSApi.jsval.isInt32(tag))
            return JSApi.getInt32(e);
        if (JSApi.jsval.isDouble(tag))
            return JSApi.getSingle(e);
        if (JSApi.jsval.isString(tag))
            return JSApi.getStringS(e);
        if (JSApi.jsval.isObject(tag))
        {
            if (JSApi.isVector2S(e))
            {
                return JSApi.getVector2S(e);
            }
            if (JSApi.isVector3S(e))
            {
                return JSApi.getVector3S(e);
            }
            return getObject(e);
        }
        return null;
    }

    public object getDelegate(eGetType e)
    {
        switch (e)
        {
            case eGetType.GetARGV:
            {
                // TODO check: index must ++
                int jsObjID = JSApi.getObject((int) JSApi.GetType.Arg);
                if (jsObjID == 0)
                    return null;

                var csObj = JSMgr.getCSObj(jsObjID);
                return csObj;

//                     IntPtr jsObj = JSApi.JSh_ArgvObject(JSMgr.cx, vc.vp, vc.currIndex++);
//                     if (jsObj == IntPtr.Zero)
//                         return null;
// 
//                     object csObj = JSMgr.getCSObj(jsObj);
//                     return csObj;
            }
            // break; !!
//             case eGetType.GetARGVRefOut:
//                 {
//                     jsval val = new jsval();
//                     JSApi.JSh_SetJsvalUndefined(ref val);
//                     getJSValueOfParam(ref val, vc.currIndex++);
// 
//                     IntPtr jsObj = JSApi.JSh_GetJsvalObject(ref val);
//                     if (jsObj == IntPtr.Zero)
//                         return null;
// 
//                     JSApi.JSh_GetUCProperty(JSMgr.cx, jsObj, "__nativeObj", -1, ref val);
//                     IntPtr __nativeObj = JSApi.JSh_GetJsvalObject(ref val);
//                     if (__nativeObj == IntPtr.Zero)
//                         return null;
// 
//                     object csObj = JSMgr.getCSObj(__nativeObj);
//                     return csObj;
//                 }
//                 break;
//             case eGetType.Jsval:
//                 {
//                     jsval val = new jsval();
//                     JSApi.JSh_SetJsvalUndefined(ref val);
// 
//                     IntPtr jsObj = JSApi.JSh_GetJsvalObject(ref vc.valTemp);
//                     if (jsObj == IntPtr.Zero)
//                         return null;
//
//                     object csObj = JSMgr.getCSObj(jsObj);
//                     return csObj;
//                 }
//                 break;
            default:
                Debug.LogError("Not Supported");
                break;
        }
        return null;
    }

    #endregion

    #region Set Operation

    // for generic type
    // type is assigned during runtime
    public void setWhatever(int e, object obj)
    {
//         Type type = (Type)mTempObj;
//         if (type.IsByRef)
//             type = type.GetElementType();

        // ?? TODO use mTempObj or not?

        // TODO check
        if (obj == null)
        {
            JSApi.setUndefined(e);
            return;
        }

        if (obj is Object)
        {
            if (obj.Equals(null))
            {
                JSApi.setUndefined(e);
                return;
            }
        }

        var type = obj.GetType();

        if (type == typeof (string))
            JSApi.setStringS(e, (string) obj);
        else if (type.IsEnum)
            JSApi.setEnum(e, (int) obj);
        else if (type.IsPrimitive)
        {
            if (type == typeof (bool))
                JSApi.setBooleanS(e, (bool) obj);
            else if (type == typeof (char))
                JSApi.setChar(e, (char) obj);
            else if (type == typeof (byte))
                JSApi.setByte(e, (byte) obj);
            else if (type == typeof (sbyte))
                JSApi.setSByte(e, (sbyte) obj);
            else if (type == typeof (ushort))
                JSApi.setUInt16(e, (ushort) obj);
            else if (type == typeof (short))
                JSApi.setInt16(e, (short) obj);
            else if (type == typeof (uint))
                JSApi.setUInt32(e, (uint) obj);
            else if (type == typeof (int))
                JSApi.setInt32(e, (int) obj);
            else if (type == typeof (ulong))
                JSApi.setUInt64(e, (ulong) obj);
            else if (type == typeof (long))
                JSApi.setInt64(e, (long) obj);
            else if (type == typeof (float))
                JSApi.setSingle(e, (float) obj);
            else if (type == typeof (double))
                JSApi.setDouble(e, (double) obj);
            else
                Debug.LogError("Unknown primitive type");
        }
        else if (type == typeof (Vector3))
        {
            JSApi.setVector3S(e, (Vector3) obj);
        }
        else if (type == typeof (Vector2))
        {
            JSApi.setVector2S(e, (Vector2) obj);
        }
        else
        {
            setObject(e, obj);
        }
    }

    /// <summary>
    ///     Should return base type object?
    /// </summary>
    /// <param name="typeName">Name of the type.</param>
    /// <returns></returns>
    /// <summary>
    ///     Sets the object.
    ///     if e == UpdateRefARGV, currIndex must be set before this function
    ///     operation of this function:
    ///     1) if JavaScript already exists, return that JavaScript object.
    ///     2) else, create a new JavaScript object and return it.
    /// </summary>
    /// <param name="e">The e.</param>
    /// <param name="csObj">The cs object.</param>
    /// <returns></returns>
    public int setObject( /* JSApi.SetType */ int e, object csObj)
    {
        // http://answers.unity3d.com/questions/1087158/unityengineobject-is-null-but-systemobject-is-not.html
        if (csObj != null && csObj is Object)
        {
            if (csObj.Equals(null))
                csObj = null;
        }

        int jsObjID = 0;
        if (csObj != null)
        {
            var csType = csObj.GetType();
            var typeInfo = JSCache.GetTypeInfo(csType);

            if (typeInfo.IsClass)
            {
                jsObjID = JSMgr.getJSObj(csObj, typeInfo);
            }
            if (jsObjID == 0)
            {
                if (csObj is CSRepresentedObject)
                {
                    jsObjID = ((CSRepresentedObject) csObj).jsObjID;
                }
                else if (csObj is IEnumerator)
                {
                    //如果是实现IEnumerator接口的类型，直接创建IEnumerator对应的js对象
                    string typeName = "System.Collections.IEnumerator";
                    jsObjID = JSApi.createJSClassObject(typeName);
                    if (jsObjID != 0)
                        JSMgr.addJSCSRel(jsObjID, csObj);
                    else
                        Debug.LogError("Return a \"" + typeName +
                                       "\" to JS failed. Did you forget to export that class?");
                }
                else
                {
                    if (typeInfo.IsDelegate)
                    {
                        jsObjID = JSMgr.getFunIDByDelegate((Delegate) csObj);
                    }
                    if (jsObjID == 0)
                    {
                        string typeName = string.Empty;
                        // create a JSRepresentedObject object in JS to represent a C# delegate object
                        if (typeInfo.IsDelegate)
                        {
                            typeName = "JSRepresentedObject";
                            jsObjID = JSApi.createJSClassObject(typeName);
                        }
                        else
                        {
                            typeName = typeInfo.JSTypeFullName;
                            jsObjID = JSApi.createJSClassObject(typeName);
                            if (jsObjID == 0)
                            {
                                var baseType = csType.BaseType;
                                var baseTypeInfo = JSCache.GetTypeInfo(baseType);
                                if (baseType != null)
                                {
                                    string baseTypeName = baseTypeInfo.JSTypeFullName;
                                    jsObjID = JSApi.createJSClassObject(baseTypeName);
                                    if (jsObjID != 0)
                                    {
                                        Debug.LogWarning("WARNING: Return a \"" + typeName +
                                                         "\" to JS failed. Return base type\"" + baseTypeName +
                                                         "\" instead.");
                                    }
                                }
                            }
                        }

                        if (jsObjID != 0)
                            JSMgr.addJSCSRel(jsObjID, csObj);
                        else
                            Debug.LogError("Return a \"" + typeName +
                                           "\" to JS failed. Did you forget to export that class?");
                    }
                }
            }
        }

        JSApi.setObject(e, jsObjID);
        return jsObjID;
    }

    // TODO what?
//     public void setDelegate(eSetType e, object csObj)
//     {
//         switch (e)
//         {
//             case eSetType.Jsval:
//             case eSetType.SetRval:
//                 {
//                     JSApi.JSh_SetJsvalUndefined(ref vc.valReturn);
//                     if (csObj != null)
//                     {
//                         IntPtr jsObj = IntPtr.Zero;
//                         Type csType = csObj.GetType();
//                         if (csType.IsClass && (jsObj = JSMgr.getJSObj(csObj)) != IntPtr.Zero)
//                         {
//                             JSApi.JSh_SetJsvalObject(ref vc.valReturn, jsObj);
//                         }
//                         else
//                         {
//                             string typeName = JSNameMgr.GetJSTypeFullName(csType);
//                             IntPtr jstypeObj = JSDataExchangeMgr.GetJSObjectByname(typeName);
//                             if (jstypeObj != IntPtr.Zero)
//                             {
//                                 jsObj = JSApi.JSh_NewObjectAsClass(JSMgr.cx, jstypeObj, "ctor", null /*JSMgr.mjsFinalizer*/);
// 
//                                 // __nativeObj
//                                 IntPtr __nativeObj = JSApi.JSh_NewMyClass(JSMgr.cx, JSMgr.mjsFinalizer);
//                                 JSMgr.addJSCSRelation(jsObj, __nativeObj, csObj);
// 
//                                 // jsObj.__nativeObj = __nativeObj
//                                 jsval val = new jsval();
//                                 JSApi.JSh_SetJsvalObject(ref val, __nativeObj);
//                                 JSApi.JSh_SetUCProperty(JSMgr.cx, jsObj, "__nativeObj", -1, ref val);
// 
//                                 JSApi.JSh_SetJsvalObject(ref vc.valReturn, jsObj);
//                             }
//                             else
//                             {
//                                 Debug.LogError("Return a \"" + typeName + "\" to JS failed. Did you forget to export that class?");
//                             }
//                         }
//                     }
// 
//                     if (e == eSetType.Jsval)
//                         vc.valTemp = vc.valReturn;
//                     else if (e == eSetType.SetRval)
//                         JSApi.JSh_SetRvalJSVAL(JSMgr.cx, vc.vp, ref vc.valReturn);
//                 }
//                 break;
//             case eSetType.UpdateARGVRefOut:
//                 {
//                     jsval val = new jsval(); val.asBits = 0;
//                     IntPtr argvJSObj = JSApi.JSh_ArgvObject(JSMgr.cx, vc.vp, vc.currIndex);
//                     if (argvJSObj != IntPtr.Zero)
//                     {
//                         bool success = false;
// 
//                         IntPtr jsObj = IntPtr.Zero;
//                         Type csType = csObj.GetType();
//                         if (csType.IsClass && (jsObj = JSMgr.getJSObj(csObj)) != IntPtr.Zero)
//                         {
//                             // 3)
//                             // argvObj.Value = jsObj
//                             //
//                             JSApi.JSh_SetJsvalObject(ref val, jsObj);
//                             JSApi.JSh_SetUCProperty(JSMgr.cx, argvJSObj, "Value", -1, ref val);
//                             success = true;
//                         }
//                         else
//                         {
//                             // csObj must not be null
//                             IntPtr jstypeObj = JSDataExchangeMgr.GetJSObjectByname(JSNameMgr.GetTypeFullName(csObj.GetType()));
//                             if (jstypeObj != IntPtr.Zero)
//                             {
//                                 // 1)
//                                 // jsObj: prototype  
//                                 // __nativeObj: csObj + finalizer
//                                 // 
//                                 jsObj = JSApi.JSh_NewObjectAsClass(JSMgr.cx, jstypeObj, "ctor", null /*JSMgr.mjsFinalizer*/);
//                                 // __nativeObj
//                                 IntPtr __nativeObj = JSApi.JSh_NewMyClass(JSMgr.cx, JSMgr.mjsFinalizer);
//                                 JSMgr.addJSCSRelation(jsObj, __nativeObj, csObj);
// 
//                                 //
//                                 // 2)
//                                 // jsObj.__nativeObj = __nativeObj
//                                 //
//                                 JSApi.JSh_SetJsvalObject(ref val, __nativeObj);
//                                 JSApi.JSh_SetUCProperty(JSMgr.cx, jsObj, "__nativeObj", -1, ref val);
// 
//                                 // 3)
//                                 // argvObj.Value = jsObj
//                                 //
//                                 JSApi.JSh_SetJsvalObject(ref val, jsObj);
//                                 JSApi.JSh_SetUCProperty(JSMgr.cx, argvJSObj, "Value", -1, ref val);
//                                 success = true;
//                             }
//                             else
//                             {
//                                 Debug.LogError("Return a \"" + JSNameMgr.GetTypeFullName(csObj.GetType()) + "\" to JS failed. Did you forget to export that class?");
//                             }
//                         }
// 
//                         if (!success)
//                         {
//                             JSApi.JSh_SetJsvalUndefined(ref val);
//                             JSApi.JSh_SetUCProperty(JSMgr.cx, argvJSObj, "Value", -1, ref val);
//                         }
//                     }
//                 }
//                 break;
//             default:
//                 Debug.LogError("Not Supported");
//                 break;
//         }
//     }

    #endregion
}

/// <summary>
///     CSRepresentedObject
///     if we have a JavaScript Message, like this:
///     var Message =
///     {
///     id: 4,
///     str: "hello, world"
///     }
///     when it comes to C#, (e.g. store in a C# version List<>), there is no C# type matching it
///     so we make a CSRepresentedObject to wrap it
/// </summary>
public class CSRepresentedObject
{
    public static int s_objCount;
    public static int s_funCount;
    private readonly int jsEngineRound;
    public bool bFunction;

    public int jsObjID;

//     public static bool operator !=(CSRepresentedObject x, CSRepresentedObject y)
//     {
//         return !(x == y);
//     }
//     public static bool operator ==(CSRepresentedObject x, CSRepresentedObject y)
//     {
//         // If both are null, or both are same instance, return true.
//         if (System.Object.ReferenceEquals(x, y))
//         {
//             return true;
//         }
//         return x.jsObjID == y.jsObjID;
//     }
//     public override bool Equals(object obj)
//     {
//         // If parameter is null return false.
//         if (obj == null)
//         {
//             return false;
//         }
// 
//         // If parameter cannot be cast to Point return false.
//         CSRepresentedObject p = obj as CSRepresentedObject;
//         if ((System.Object)p == null)
//         {
//             return false;
//         }
//         return this.jsObjID == p.jsObjID;
//     }

    // don't create this object directly, should use JSDataExchangeMgr.getObject
    public CSRepresentedObject(int jsObjID, bool bFunction = false)
    {
        jsEngineRound = JSMgr.jsEngineRound;
        this.jsObjID = jsObjID;
        this.bFunction = bFunction;
        JSMgr.addJSCSRel(jsObjID, this, true);

        if (bFunction)
            s_funCount++;
        else
            s_objCount++;

        // !
        // inc 之后 refCount 可能 > 1
        // getCSObj 可能检查 WeakReference.Target == null，表明 ~CSRepresentedObject 未被调用
        // 此时我们继续创建另一个 CSRepresentedObject 对象
        // 那么 refCount 就会 > 1

        //int refCount = 
        JSApi.incRefCount(jsObjID);
        //Debug.Log(new StringBuilder().AppendFormat("+ CSRepresentedObject {0} Ref[{1}] Fun[{1}]", jsObjID, refCount, bFunction ? 1 : 0));
    }

    ~CSRepresentedObject()
    {
        if (JSMgr.IsShutDown)
            return;

        var action = CreateDestructAction(bFunction, jsObjID, jsEngineRound);
        JSEngine.inst.DoThreadSafeAction(action);

        //Debug.Log(new StringBuilder().AppendFormat("- CSRepresentedObject {0} Ref[{1}] Fun[{1}]", jsObjID, refCount, bFunction ? 1 : 0));
    }

    private Action CreateDestructAction(bool bFunction, int jsObjID, int round)
    {
        return () =>
        {
            if (bFunction)
                s_funCount--;
            else
                s_objCount--;

            int refCount = JSApi.decRefCount(jsObjID);
            if (refCount <= 0)
            {
                JSMgr.removeJSCSRel(jsObjID, round);
                if (bFunction)
                {
                    JSMgr.removeJSFunCSDelegateRel(jsObjID);
                }
            }
            //else
            //{
            //    Debug.LogError(";;;//IIL.x&");
            //}
        };
    }
}