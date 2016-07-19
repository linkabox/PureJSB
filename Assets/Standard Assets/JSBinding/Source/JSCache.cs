using System;
using System.Text;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SharpKit.JavaScript;
using LITJson;

public class JSCache
{
    #region MonoBehaviour -> JSComponent Name
    // MonoBehaviour 对应的 JSComponent 名字
    private static Dictionary<string, string> _mono2JsComDic = new Dictionary<string, string>();
    private static HashSet<string> _jsTypeNameSet = new HashSet<string>();
    private static Type monoBehaviourType = typeof(MonoBehaviour);

    // 根据 脚本名获得JSComponent名
    public static string GetMono2JsComName(string monoBehaviourName)
    {
        string ret;
        if (_mono2JsComDic.TryGetValue(monoBehaviourName, out ret))
            return ret;
        // 没找到返回  Empty
        return string.Empty;
    }

    public static bool IsJsComTypeName(string jsTypeName)
    {
        return _mono2JsComDic.ContainsKey(jsTypeName);
    }

    public static bool IsJsTypeName(string jsTypeName)
    {
        return _jsTypeNameSet.Contains(jsTypeName);
    }

    /// <summary>
    /// 加载 Mono2JsComConfig.json,JsTypeInfoConfig.json
    /// </summary>
    public static void InitJsTypeConfig()
    {
        InitJsTypeInfoConfig();
        InitMono2JsComConfig();
    }

    private static void InitJsTypeInfoConfig()
    {
        _jsTypeNameSet.Clear();

        var jsonBytes = JSFileLoader.LoadJSSync(JSPathSettings.JsTypeInfoConfig);
		var list = JsonMapper.ToObject<List<string>>(Encoding.UTF8.GetString(jsonBytes));
        if (list != null)
        {
            foreach (string item in list)
            {
                _jsTypeNameSet.Add(item);
            }
        }
        else
        {
            Debug.LogError("Read JsTypeInfo Config Error");
        }

        Debug.Log("JSCache.JsTypeInfo OK, total: " + _jsTypeNameSet.Count);
    }

    private static void InitMono2JsComConfig()
    {
        _mono2JsComDic.Clear();

        var jsonBytes = JSFileLoader.LoadJSSync(JSPathSettings.Mono2JsComConfig);
		var table = JsonMapper.ToObject<Dictionary<string,string>>(Encoding.UTF8.GetString(jsonBytes));
        if (table != null)
        {
			_mono2JsComDic = table;
        }
        else
        {
            Debug.LogError("Read Mono2JsCom Config Error");
        }

        Debug.Log("JSCache.Mono2JsCom OK, total: " + _mono2JsComDic.Count);
    }

    #endregion MonoBehaviour -> JSComponent Name

    #region MonoBehaviour Inheritance Relation
    // 对继承关系做缓存

    static Dictionary<string, bool> dictClassInheritanceRel = new Dictionary<string, bool>();

    public static bool IsInheritanceRel(string baseClassName, string subClassName)
    {
        string key = baseClassName + "|" + subClassName;

        bool ret = false;
        if (dictClassInheritanceRel.TryGetValue(key, out ret))
        {
            return ret;
        }

        ret = false;
        if (JSMgr.vCall.CallJSFunctionName(0 /*global*/, "jsb_IsInheritanceRel", baseClassName, subClassName))
        {
            ret = (System.Boolean)JSApi.getBooleanS((int)JSApi.GetType.JSFunRet);
        }
        dictClassInheritanceRel.Add(key, ret);
        return ret;
    }

    #endregion MonoBehaviour Inheritance Relation

    #region Type -> TypeInfo

    public class TypeInfo
    {
        Type type;
        public TypeInfo(Type t) { this.type = t; }

        bool? isValueType = null;
        bool? isClass = null;
        bool? isDelegate = null;
        bool? isCSMonoBehaviour = null;
        string jsTypeFullName = null;

        // public bool IsNull { get { return type == null; } }

        public bool IsValueType
        {
            get
            {
                if (isValueType == null)
                    isValueType = type.IsValueType;
                return (bool)isValueType;
            }
        }
        public bool IsClass
        {
            get
            {
                if (isClass == null)
                    isClass = type.IsClass;
                return (bool)isClass;
            }
        }
        public bool IsDelegate
        {
            get
            {
                if (isDelegate == null)
                    isDelegate = typeof(System.Delegate).IsAssignableFrom(type);
                return (bool)isDelegate;
            }
        }
        public bool IsCSMonoBehaviour
        {
            get
            {
                if (isCSMonoBehaviour == null)
                {
                    //这里的判断顺序不能变
                    //Unity Component > MonoBehaviour child > Mono2Js Com > JsTypeAtrribute
                    if (type == null || type == typeof(JSComponent))
                        isCSMonoBehaviour = false;
                    else if (type.Namespace != null && type.Namespace == monoBehaviourType.Namespace)
                        isCSMonoBehaviour = true;
                    else if (!monoBehaviourType.IsAssignableFrom(type))
                        isCSMonoBehaviour = false;
                    //else if (_mono2JsComDic.ContainsKey(JSNameMgr.GetTypeFullName(type, false)))
                    //    isCSMonoBehaviour = false;
                    // This is useful if source c# file still exists in project
                    else if (type.IsDefined(typeof(JsTypeAttribute), false))
                        isCSMonoBehaviour = false;
                    else if (IsJsTypeName(type.FullName))
                        isCSMonoBehaviour = false;
                    else
                        isCSMonoBehaviour = true;
                }
                return (bool)isCSMonoBehaviour;
            }
        }
        public string JSTypeFullName
        {
            get
            {
                if (jsTypeFullName == null)
                    jsTypeFullName = JSNameMgr.GetJSTypeFullName(type);
                return jsTypeFullName;
            }
        }

    }
    static Dictionary<Type, TypeInfo> dictType2TypeInfo = new Dictionary<Type, TypeInfo>();
    static TypeInfo nullTypeInfo = new TypeInfo(null);

    public static TypeInfo GetTypeInfo(Type type)
    {
        if (type == null)
            return nullTypeInfo;

        TypeInfo ti;
        if (dictType2TypeInfo.TryGetValue(type, out ti))
            return ti;

        ti = new TypeInfo(type);
        dictType2TypeInfo.Add(type, ti);
        return ti;
    }
    #endregion
}
