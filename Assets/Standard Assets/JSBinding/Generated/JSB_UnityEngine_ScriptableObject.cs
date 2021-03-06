﻿
//------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by CSGenerator.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using jsval = JSApi.jsval;

public class JSB_UnityEngine_ScriptableObject
{

////////////////////// ScriptableObject ///////////////////////////////////////
// constructors

static bool ScriptableObject_ScriptableObject1(JSVCall vc, int argc)
{
    int _this = JSApi.getObject((int)JSApi.GetType.Arg);
    JSApi.attachFinalizerObject(_this);
    --argc;

    int len = argc;
    if (len == 0)
    {
        JSMgr.addJSCSRel(_this, new UnityEngine.ScriptableObject());
    }

    return true;
}

// fields

// properties

// methods

static bool ScriptableObject_CreateInstance__Type(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.Type arg0 = (System.Type)JSDataExchangeMgr.GetTypeByJsParam((int)JSApi.GetType.Arg);
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, UnityEngine.ScriptableObject.CreateInstance(arg0));
    }

    return true;
}

static bool ScriptableObject_CreateInstance__String(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, UnityEngine.ScriptableObject.CreateInstance(arg0));
    }

    return true;
}
public static MethodID methodID2 = new MethodID("CreateInstance", "T", TypeFlag.IsT, null, null);

static bool ScriptableObject_CreateInstanceT1(JSVCall vc, int argc)
{
    // Get generic method by name and param count.
    MethodInfo method = JSDataExchangeMgr.makeGenericMethod(typeof(UnityEngine.ScriptableObject), methodID2, 1); 
    if (method == null) return true;

    int len = argc - 1;
    if (len == 0) 
    {
        object[] arr_t = null;
                JSMgr.datax.setWhatever((int)JSApi.SetType.Rval, method.Invoke(null, arr_t));
    }

    return true;
}


//register

public static void __Register()
{
    JSMgr.CallbackInfo ci = new JSMgr.CallbackInfo();
    ci.type = typeof(UnityEngine.ScriptableObject);
    ci.fields = new JSMgr.CSCallbackField[]
    {

    };
    ci.properties = new JSMgr.CSCallbackProperty[]
    {

    };
    ci.constructors = new JSMgr.MethodCallBackInfo[]
    {
        new JSMgr.MethodCallBackInfo(ScriptableObject_ScriptableObject1, ".ctor"),

    };
    ci.methods = new JSMgr.MethodCallBackInfo[]
    {
        new JSMgr.MethodCallBackInfo(ScriptableObject_CreateInstance__Type, "CreateInstance"),
        new JSMgr.MethodCallBackInfo(ScriptableObject_CreateInstance__String, "CreateInstance"),
        new JSMgr.MethodCallBackInfo(ScriptableObject_CreateInstanceT1, "CreateInstance"),

    };
    JSMgr.allCallbackInfo.Add(ci);
}


}
