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

public class JSB_UnityEngine_HumanBone
{

////////////////////// HumanBone ///////////////////////////////////////
// constructors
public static ConstructorID constructorID0 = new ConstructorID(null, null);

static bool HumanBone_HumanBone1(JSVCall vc, int argc)
{
    int _this = JSApi.getObject((int)JSApi.GetType.Arg);
    JSApi.attachFinalizerObject(_this);
    --argc;

    int len = argc;
    if (len == 0)
    {
        JSMgr.addJSCSRel(_this, new UnityEngine.HumanBone());
    }

    return true;
}

// fields
static void HumanBone_limit(JSVCall vc)
{
    if (vc.bGet) {
        UnityEngine.HumanBone _this = (UnityEngine.HumanBone)vc.csObj;
        var result = _this.limit;
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, result);
    }
    else {
        UnityEngine.HumanLimit arg0 = (UnityEngine.HumanLimit)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
        UnityEngine.HumanBone _this = (UnityEngine.HumanBone)vc.csObj;
        _this.limit = arg0;
        JSMgr.changeJSObj(vc.jsObjID, _this);
    }
}

// properties
static void HumanBone_boneName(JSVCall vc)
{
    if (vc.bGet)
    { 
        UnityEngine.HumanBone _this = (UnityEngine.HumanBone)vc.csObj;
        var result = _this.boneName;
                JSApi.setStringS((int)JSApi.SetType.Rval, result);
    }
    else
    { 
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        UnityEngine.HumanBone _this = (UnityEngine.HumanBone)vc.csObj;
        _this.boneName = arg0;
        JSMgr.changeJSObj(vc.jsObjID, _this);
    }
}
static void HumanBone_humanName(JSVCall vc)
{
    if (vc.bGet)
    { 
        UnityEngine.HumanBone _this = (UnityEngine.HumanBone)vc.csObj;
        var result = _this.humanName;
                JSApi.setStringS((int)JSApi.SetType.Rval, result);
    }
    else
    { 
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        UnityEngine.HumanBone _this = (UnityEngine.HumanBone)vc.csObj;
        _this.humanName = arg0;
        JSMgr.changeJSObj(vc.jsObjID, _this);
    }
}

// methods


//register

public static void __Register()
{
    JSMgr.CallbackInfo ci = new JSMgr.CallbackInfo();
    ci.type = typeof(UnityEngine.HumanBone);
    ci.fields = new JSMgr.CSCallbackField[]
    {
        HumanBone_limit,

    };
    ci.properties = new JSMgr.CSCallbackProperty[]
    {
        HumanBone_boneName,
        HumanBone_humanName,

    };
    ci.constructors = new JSMgr.MethodCallBackInfo[]
    {
        new JSMgr.MethodCallBackInfo(HumanBone_HumanBone1, ".ctor"),

    };
    ci.methods = new JSMgr.MethodCallBackInfo[]
    {

    };
    JSMgr.allCallbackInfo.Add(ci);
}


}