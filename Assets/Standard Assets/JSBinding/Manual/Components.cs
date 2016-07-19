using System;
using System.Collections.Generic;
using UnityEngine;

public partial class UnityEngineManual
{
    private static string _typeString;
    private static Type _type;
    private static JSCache.TypeInfo _typeInfo;

    private static GameObject _curGo;
    public static GameObject _goFromComponent;

    private static void help_retComArr(JSVCall vc, Component[] arrRet)
    {
        int Count = arrRet.Length;
        for (int i = 0; i < arrRet.Length; i++)
        {
            JSMgr.datax.setObject((int) JSApi.SetType.SaveAndTempTrace, arrRet[i]);
            JSApi.moveSaveID2Arr(i);
        }
        JSApi.setArrayS((int) JSApi.SetType.Rval, Count, true);
    }

    private static void help_searchAndRetCom(JSVCall vc, JSComponent[] jsComs, string typeString)
    {
        int id = 0;
        foreach (var jsCom in jsComs)
        {
            if (jsCom.jsClassName == typeString ||
                JSCache.IsInheritanceRel(typeString, jsCom.jsClassName))
            {
                id = jsCom.GetJSObjID();
                break;
            }
        }
        JSApi.setObject((int) JSApi.SetType.Rval, id);
    }

    private static void help_searchAndRetComs(JSVCall vc, JSComponent[] com, string typeString)
    {
        var lst = new List<JSComponent>();
        foreach (var c in com)
        {
            if (c.jsClassName == typeString ||
                JSCache.IsInheritanceRel(typeString, c.jsClassName))
            {
                lst.Add(c);
            }
        }
        for (int i = 0; i < lst.Count; i++)
        {
            int jsObjID = lst[i].GetJSObjID();
            JSApi.setObject((int) JSApi.SetType.SaveAndTempTrace, jsObjID);
            JSApi.moveSaveID2Arr(i);
        }
        JSApi.setArrayS((int) JSApi.SetType.Rval, lst.Count, true);

        //         var arrVal = new JSApi.jsval[lst.Count];
        //         for (int i = 0; i < lst.Count; i++)
        //         {
        //             JSApi.JSh_SetJsvalObject(ref arrVal[i], lst[i].jsObj);
        //         }
        //         JSMgr.datax.setArray(JSDataExchangeMgr.eSetType.SetRval, arrVal);
    }

    private static void help_getGoAndType(JSVCall vc)
    {
        _curGo = _goFromComponent;
        if (_curGo == null)
        {
            _curGo = (GameObject) vc.csObj;
        }
        _typeString = JSApi.getStringS((int) JSApi.GetType.Arg);
        _type = JSDataExchangeMgr.GetTypeByName(_typeString);
        _typeInfo = JSCache.GetTypeInfo(_type);
    }

    private static void help_getComponentGo(JSVCall vc)
    {
        _goFromComponent = ((Component) vc.csObj).gameObject;
    }

    public static bool GameObject_AddComponent__Type(JSVCall vc, int count)
    {
        return GameObject_AddComponentT1(vc,count);
    }

    /* 
     * GameObject.AddComponent<T>()
     */

    public static bool GameObject_AddComponentT1(JSVCall vc, int count)
    {
        help_getGoAndType(vc);

        if (_typeInfo.IsCSMonoBehaviour)
        {
            var com = _curGo.AddComponent(_type);
            JSMgr.datax.setObject((int) JSApi.SetType.Rval, com);
        }
        else
        {
            string jsComponentName = JSCache.GetMono2JsComName(_typeString);
            var jsComponentType = typeof (JSComponent);
            if (string.IsNullOrEmpty(jsComponentName))
            {
                Debug.LogWarning(string.Format("\"{0}\" has no JSComponent_XX. Use JSComponent instead.", _typeString));
            }
            else
            {
                jsComponentType = JSDataExchangeMgr.GetTypeByName(jsComponentName, jsComponentType);
            }

            var jsComp = (JSComponent) _curGo.AddComponent(jsComponentType);
            jsComp.jsClassName = _typeString;
            jsComp.Setup(); // 要调用 js 的 Awake

            //JSApi.JSh_SetRvalObject(vc.cx, vc.vp, jsComp.jsObj);
            JSApi.setObject((int) JSApi.SetType.Rval, jsComp.GetJSObjID());
        }
        return true;
    }

    /*
     * GameObject.GetComponent<T>()
     */

    public static bool Component_GetComponentT1(JSVCall vc, int count)
    {
        help_getComponentGo(vc);
        GameObject_GetComponentT1(vc, count);
        _goFromComponent = null;
        return true;
    }

    public static bool GameObject_GetComponent__Type(JSVCall vc, int count)
    {
        return GameObject_GetComponentT1(vc, count);
    }

    public static bool GameObject_GetComponentT1(JSVCall vc, int count)
    {
        help_getGoAndType(vc);

        if (_typeInfo.IsCSMonoBehaviour)
        {
            var com = _curGo.GetComponent(_type);
            JSMgr.datax.setObject((int) JSApi.SetType.Rval, com);
        }
        else
        {
            var com = _curGo.GetComponents<JSComponent>();
            help_searchAndRetCom(vc, com, _typeString);
        }
        return true;
    }

    /*
     * GameObject.GetComponents<T>()
     */

    public static bool Component_GetComponentsT1(JSVCall vc, int count)
    {
        help_getComponentGo(vc);
        GameObject_GetComponentsT1(vc, count);
        _goFromComponent = null;
        return true;
    }

    public static bool GameObject_GetComponents__Type(JSVCall vc, int count)
    {
        return GameObject_GetComponentsT1(vc, count);
    }
    public static bool GameObject_GetComponentsT1(JSVCall vc, int count)
    {
        help_getGoAndType(vc);

        if (_typeInfo.IsCSMonoBehaviour)
        {
            var arrRet = _curGo.GetComponents(_type);
            help_retComArr(vc, arrRet);
        }
        else
        {
            var com = _curGo.GetComponents<JSComponent>();
            help_searchAndRetComs(vc, com, _typeString);
        }
        return true;
    }

    /*
     * GameObject.GetComponentInChildren<T>()
     */

    public static bool Component_GetComponentInChildrenT1(JSVCall vc, int count)
    {
        help_getComponentGo(vc);
        GameObject_GetComponentInChildrenT1(vc, count);
        _goFromComponent = null;
        return true;
    }

    public static bool GameObject_GetComponentInChildren__Type(JSVCall vc, int count)
    {
        return GameObject_GetComponentInChildrenT1(vc, count);
    }

    public static bool GameObject_GetComponentInChildrenT1(JSVCall vc, int count)
    {
        help_getGoAndType(vc);

        if (_typeInfo.IsCSMonoBehaviour)
        {
            var com = _curGo.GetComponentInChildren(_type);
            JSMgr.datax.setObject((int) JSApi.SetType.Rval, com);
        }
        else
        {
            var com = _curGo.GetComponentsInChildren<JSComponent>();
            help_searchAndRetCom(vc, com, _typeString);
        }
        return true;
    }

    /*
     * GetComponentsInChildren<T>()
     */

    public static bool Component_GetComponentsInChildrenT1(JSVCall vc, int count)
    {
        help_getComponentGo(vc);
        GameObject_GetComponentsInChildrenT1(vc, count);
        _goFromComponent = null;
        return true;
    }

    public static bool GameObject_GetComponentsInChildren__Type(JSVCall vc, int count)
    {
        return GameObject_GetComponentsInChildrenT1(vc, count);
    }

    public static bool GameObject_GetComponentsInChildrenT1(JSVCall vc, int count)
    {
        help_getGoAndType(vc);

        if (_typeInfo.IsCSMonoBehaviour)
        {
            var arrRet = _curGo.GetComponentsInChildren(_type);
            help_retComArr(vc, arrRet);
        }
        else
        {
            var com = _curGo.GetComponentsInChildren<JSComponent>();
            help_searchAndRetComs(vc, com, _typeString);
        }
        return true;
    }

    /*
     * GetComponentsInChildren<T>(bool includeInactive)
     */

    public static bool Component_GetComponentsInChildrenT1__Boolean(JSVCall vc, int count)
    {
        help_getComponentGo(vc);
        GameObject_GetComponentsInChildrenT1__Boolean(vc, count);
        _goFromComponent = null;
        return true;
    }

    public static bool GameObject_GetComponentsInChildren__Type__Boolean(JSVCall vc, int count)
    {
        return GameObject_GetComponentsInChildrenT1__Boolean(vc, count);
    }

    public static bool GameObject_GetComponentsInChildrenT1__Boolean(JSVCall vc, int count)
    {
        help_getGoAndType(vc);
        // TODO check
        //        bool includeInactive = JSMgr.datax.getBoolean(JSDataExchangeMgr.eGetType.GetARGV);
        bool includeInactive = JSApi.getBooleanS((int) JSApi.GetType.Arg);

        if (_typeInfo.IsCSMonoBehaviour)
        {
            var arrRet = _curGo.GetComponentsInChildren(_type, includeInactive);
            help_retComArr(vc, arrRet);
        }
        else
        {
            var com = _curGo.GetComponentsInChildren<JSComponent>(includeInactive);
            help_searchAndRetComs(vc, com, _typeString);
        }
        return true;
    }

    /*
     * GameObject.GetComponentInParent<T>()
     */

    public static bool Component_GetComponentInParentT1(JSVCall vc, int count)
    {
        help_getComponentGo(vc);
        GameObject_GetComponentInParentT1(vc, count);
        _goFromComponent = null;
        return true;
    }

    public static bool GameObject_GetComponentInParent__Type(JSVCall vc, int count)
    {
        return GameObject_GetComponentInParentT1(vc, count);
    }

    public static bool GameObject_GetComponentInParentT1(JSVCall vc, int count)
    {
        help_getGoAndType(vc);

        if (_typeInfo.IsCSMonoBehaviour)
        {
            var com = _curGo.GetComponentInParent(_type);
            JSMgr.datax.setObject((int) JSApi.SetType.Rval, com);
        }
        else
        {
            var com = _curGo.GetComponentsInParent<JSComponent>();
            help_searchAndRetCom(vc, com, _typeString);
        }
        return true;
    }

    /*
    * GetComponentsInParent<T>()
    */

    public static bool Component_GetComponentsInParentT1(JSVCall vc, int count)
    {
        help_getComponentGo(vc);
        GameObject_GetComponentsInParentT1(vc, count);
        _goFromComponent = null;
        return true;
    }

    public static bool GameObject_GetComponentsInParent__Type(JSVCall vc, int count)
    {
        return GameObject_GetComponentsInParentT1(vc, count);
    }

    public static bool GameObject_GetComponentsInParentT1(JSVCall vc, int count)
    {
        help_getGoAndType(vc);

        if (_typeInfo.IsCSMonoBehaviour)
        {
            var arrRet = _curGo.GetComponentsInParent(_type);
            help_retComArr(vc, arrRet);
        }
        else
        {
            var com = _curGo.GetComponentsInParent<JSComponent>();
            help_searchAndRetComs(vc, com, _typeString);
        }
        return true;
    }

    /*
     * GetComponentsInParent<T>(bool includeInactive)
     */

    public static bool Component_GetComponentsInParentT1__Boolean(JSVCall vc, int count)
    {
        help_getComponentGo(vc);
        GameObject_GetComponentsInParentT1__Boolean(vc, count);
        _goFromComponent = null;
        return true;
    }

    public static bool GameObject_GetComponentsInParent__Type__Boolean(JSVCall vc, int count)
    {
        return GameObject_GetComponentsInParentT1__Boolean(vc, count);
    }

    public static bool GameObject_GetComponentsInParentT1__Boolean(JSVCall vc, int count)
    {
        help_getGoAndType(vc);
        // TODO check
        //        bool includeInactive = JSMgr.datax.getBoolean(JSDataExchangeMgr.eGetType.GetARGV);
        bool includeInactive = JSApi.getBooleanS((int) JSApi.GetType.Arg);

        if (_typeInfo.IsCSMonoBehaviour)
        {
            var arrRet = _curGo.GetComponentsInParent(_type, includeInactive);
            help_retComArr(vc, arrRet);
        }
        else
        {
            var com = _curGo.GetComponentsInParent<JSComponent>(includeInactive);
            help_searchAndRetComs(vc, com, _typeString);
        }
        return true;
    }
}