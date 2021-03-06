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

public class JSB_UnityEngine_StackTraceUtility
{

////////////////////// StackTraceUtility ///////////////////////////////////////
// constructors

static bool StackTraceUtility_StackTraceUtility1(JSVCall vc, int argc)
{
    int _this = JSApi.getObject((int)JSApi.GetType.Arg);
    JSApi.attachFinalizerObject(_this);
    --argc;

    int len = argc;
    if (len == 0)
    {
        JSMgr.addJSCSRel(_this, new UnityEngine.StackTraceUtility());
    }

    return true;
}

// fields

// properties

// methods

static bool StackTraceUtility_ExtractStackTrace(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 0) 
    {
                JSApi.setStringS((int)JSApi.SetType.Rval, UnityEngine.StackTraceUtility.ExtractStackTrace());
    }

    return true;
}

static bool StackTraceUtility_ExtractStringFromException__Object(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.Object arg0 = (System.Object)JSMgr.datax.getWhatever((int)JSApi.GetType.Arg);
                JSApi.setStringS((int)JSApi.SetType.Rval, UnityEngine.StackTraceUtility.ExtractStringFromException(arg0));
    }

    return true;
}


//register

public static void __Register()
{
    JSMgr.CallbackInfo ci = new JSMgr.CallbackInfo();
    ci.type = typeof(UnityEngine.StackTraceUtility);
    ci.fields = new JSMgr.CSCallbackField[]
    {

    };
    ci.properties = new JSMgr.CSCallbackProperty[]
    {

    };
    ci.constructors = new JSMgr.MethodCallBackInfo[]
    {
        new JSMgr.MethodCallBackInfo(StackTraceUtility_StackTraceUtility1, ".ctor"),

    };
    ci.methods = new JSMgr.MethodCallBackInfo[]
    {
        new JSMgr.MethodCallBackInfo(StackTraceUtility_ExtractStackTrace, "ExtractStackTrace"),
        new JSMgr.MethodCallBackInfo(StackTraceUtility_ExtractStringFromException__Object, "ExtractStringFromException"),

    };
    JSMgr.allCallbackInfo.Add(ci);
}


}
