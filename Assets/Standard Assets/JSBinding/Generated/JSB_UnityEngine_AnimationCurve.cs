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

public class JSB_UnityEngine_AnimationCurve
{

////////////////////// AnimationCurve ///////////////////////////////////////
// constructors

static bool AnimationCurve_AnimationCurve1(JSVCall vc, int argc)
{
    int _this = JSApi.getObject((int)JSApi.GetType.Arg);
    JSApi.attachFinalizerObject(_this);
    --argc;

    int len = argc;
    if (len == 1)
    {
        UnityEngine.Keyframe[] arg0 = JSDataExchangeMgr.GetJSArg<UnityEngine.Keyframe[]>(() =>
        {
            int jsObjID = JSApi.getObject((int)JSApi.GetType.Arg);
            int length = JSApi.getArrayLength(jsObjID);
            var ret = new UnityEngine.Keyframe[length];
            for (var i = 0; i < length; i++) {
                JSApi.getElement(jsObjID, i);
                ret[i] = (UnityEngine.Keyframe)JSMgr.datax.getObject((int)JSApi.GetType.SaveAndRemove);
            }
            return ret;
        });
        JSMgr.addJSCSRel(_this, new UnityEngine.AnimationCurve(arg0));
    }

    return true;
}

static bool AnimationCurve_AnimationCurve2(JSVCall vc, int argc)
{
    int _this = JSApi.getObject((int)JSApi.GetType.Arg);
    JSApi.attachFinalizerObject(_this);
    --argc;

    int len = argc;
    if (len == 0)
    {
        JSMgr.addJSCSRel(_this, new UnityEngine.AnimationCurve());
    }

    return true;
}

// fields

// properties
static void AnimationCurve_keys(JSVCall vc)
{
    if (vc.bGet)
    { 
        UnityEngine.AnimationCurve _this = (UnityEngine.AnimationCurve)vc.csObj;
        var result = _this.keys;
                var arrRet = result;
        for (int i = 0; arrRet != null && i < arrRet.Length; i++)
        {
            JSMgr.datax.setObject((int)JSApi.SetType.SaveAndTempTrace, arrRet[i]);
            JSApi.moveSaveID2Arr(i);
        }
        JSApi.setArrayS((int)JSApi.SetType.Rval, (arrRet != null ? arrRet.Length : 0), true);
    }
    else
    { 
        UnityEngine.Keyframe[] arg0 = JSDataExchangeMgr.GetJSArg<UnityEngine.Keyframe[]>(() =>
        {
            int jsObjID = JSApi.getObject((int)JSApi.GetType.Arg);
            int length = JSApi.getArrayLength(jsObjID);
            var ret = new UnityEngine.Keyframe[length];
            for (var i = 0; i < length; i++) {
                JSApi.getElement(jsObjID, i);
                ret[i] = (UnityEngine.Keyframe)JSMgr.datax.getObject((int)JSApi.GetType.SaveAndRemove);
            }
            return ret;
        });
        UnityEngine.AnimationCurve _this = (UnityEngine.AnimationCurve)vc.csObj;
        _this.keys = arg0;
    }
}
static void AnimationCurve_Item_Int32(JSVCall vc)
{
        System.Int32 arg0 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        UnityEngine.AnimationCurve _this = (UnityEngine.AnimationCurve)vc.csObj;
        var result = _this[arg0];
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, result);
}
static void AnimationCurve_length(JSVCall vc)
{
        UnityEngine.AnimationCurve _this = (UnityEngine.AnimationCurve)vc.csObj;
        var result = _this.length;
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(result));
}
static void AnimationCurve_preWrapMode(JSVCall vc)
{
    if (vc.bGet)
    { 
        UnityEngine.AnimationCurve _this = (UnityEngine.AnimationCurve)vc.csObj;
        var result = _this.preWrapMode;
                JSApi.setEnum((int)JSApi.SetType.Rval, (int)result);
    }
    else
    { 
        UnityEngine.WrapMode arg0 = (UnityEngine.WrapMode)JSApi.getEnum((int)JSApi.GetType.Arg);
        UnityEngine.AnimationCurve _this = (UnityEngine.AnimationCurve)vc.csObj;
        _this.preWrapMode = arg0;
    }
}
static void AnimationCurve_postWrapMode(JSVCall vc)
{
    if (vc.bGet)
    { 
        UnityEngine.AnimationCurve _this = (UnityEngine.AnimationCurve)vc.csObj;
        var result = _this.postWrapMode;
                JSApi.setEnum((int)JSApi.SetType.Rval, (int)result);
    }
    else
    { 
        UnityEngine.WrapMode arg0 = (UnityEngine.WrapMode)JSApi.getEnum((int)JSApi.GetType.Arg);
        UnityEngine.AnimationCurve _this = (UnityEngine.AnimationCurve)vc.csObj;
        _this.postWrapMode = arg0;
    }
}

// methods

static bool AnimationCurve_AddKey__Single__Single(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.Single arg0 = (System.Single)JSApi.getSingle((int)JSApi.GetType.Arg);
        System.Single arg1 = (System.Single)JSApi.getSingle((int)JSApi.GetType.Arg);
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(((UnityEngine.AnimationCurve)vc.csObj).AddKey(arg0, arg1)));
    }

    return true;
}

static bool AnimationCurve_AddKey__Keyframe(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        UnityEngine.Keyframe arg0 = (UnityEngine.Keyframe)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(((UnityEngine.AnimationCurve)vc.csObj).AddKey(arg0)));
    }

    return true;
}

static bool AnimationCurve_Evaluate__Single(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.Single arg0 = (System.Single)JSApi.getSingle((int)JSApi.GetType.Arg);
                JSApi.setSingle((int)JSApi.SetType.Rval, (System.Single)(((UnityEngine.AnimationCurve)vc.csObj).Evaluate(arg0)));
    }

    return true;
}

static bool AnimationCurve_MoveKey__Int32__Keyframe(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.Int32 arg0 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        UnityEngine.Keyframe arg1 = (UnityEngine.Keyframe)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(((UnityEngine.AnimationCurve)vc.csObj).MoveKey(arg0, arg1)));
    }

    return true;
}

static bool AnimationCurve_RemoveKey__Int32(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.Int32 arg0 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        ((UnityEngine.AnimationCurve)vc.csObj).RemoveKey(arg0);
    }

    return true;
}

static bool AnimationCurve_SmoothTangents__Int32__Single(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.Int32 arg0 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        System.Single arg1 = (System.Single)JSApi.getSingle((int)JSApi.GetType.Arg);
        ((UnityEngine.AnimationCurve)vc.csObj).SmoothTangents(arg0, arg1);
    }

    return true;
}

static bool AnimationCurve_EaseInOut__Single__Single__Single__Single(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 4) 
    {
        System.Single arg0 = (System.Single)JSApi.getSingle((int)JSApi.GetType.Arg);
        System.Single arg1 = (System.Single)JSApi.getSingle((int)JSApi.GetType.Arg);
        System.Single arg2 = (System.Single)JSApi.getSingle((int)JSApi.GetType.Arg);
        System.Single arg3 = (System.Single)JSApi.getSingle((int)JSApi.GetType.Arg);
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, UnityEngine.AnimationCurve.EaseInOut(arg0, arg1, arg2, arg3));
    }

    return true;
}

static bool AnimationCurve_Linear__Single__Single__Single__Single(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 4) 
    {
        System.Single arg0 = (System.Single)JSApi.getSingle((int)JSApi.GetType.Arg);
        System.Single arg1 = (System.Single)JSApi.getSingle((int)JSApi.GetType.Arg);
        System.Single arg2 = (System.Single)JSApi.getSingle((int)JSApi.GetType.Arg);
        System.Single arg3 = (System.Single)JSApi.getSingle((int)JSApi.GetType.Arg);
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, UnityEngine.AnimationCurve.Linear(arg0, arg1, arg2, arg3));
    }

    return true;
}


//register

public static void __Register()
{
    JSMgr.CallbackInfo ci = new JSMgr.CallbackInfo();
    ci.type = typeof(UnityEngine.AnimationCurve);
    ci.fields = new JSMgr.CSCallbackField[]
    {

    };
    ci.properties = new JSMgr.CSCallbackProperty[]
    {
        AnimationCurve_keys,
        AnimationCurve_Item_Int32,
        AnimationCurve_length,
        AnimationCurve_preWrapMode,
        AnimationCurve_postWrapMode,

    };
    ci.constructors = new JSMgr.MethodCallBackInfo[]
    {
        new JSMgr.MethodCallBackInfo(AnimationCurve_AnimationCurve1, ".ctor"),
        new JSMgr.MethodCallBackInfo(AnimationCurve_AnimationCurve2, ".ctor"),

    };
    ci.methods = new JSMgr.MethodCallBackInfo[]
    {
        new JSMgr.MethodCallBackInfo(AnimationCurve_AddKey__Single__Single, "AddKey"),
        new JSMgr.MethodCallBackInfo(AnimationCurve_AddKey__Keyframe, "AddKey"),
        new JSMgr.MethodCallBackInfo(AnimationCurve_Evaluate__Single, "Evaluate"),
        new JSMgr.MethodCallBackInfo(AnimationCurve_MoveKey__Int32__Keyframe, "MoveKey"),
        new JSMgr.MethodCallBackInfo(AnimationCurve_RemoveKey__Int32, "RemoveKey"),
        new JSMgr.MethodCallBackInfo(AnimationCurve_SmoothTangents__Int32__Single, "SmoothTangents"),
        new JSMgr.MethodCallBackInfo(AnimationCurve_EaseInOut__Single__Single__Single__Single, "EaseInOut"),
        new JSMgr.MethodCallBackInfo(AnimationCurve_Linear__Single__Single__Single__Single, "Linear"),

    };
    JSMgr.allCallbackInfo.Add(ci);
}


}
