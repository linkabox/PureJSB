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

public class JSB_UnityEngine_AudioEchoFilter
{

////////////////////// AudioEchoFilter ///////////////////////////////////////
// constructors

static bool AudioEchoFilter_AudioEchoFilter1(JSVCall vc, int argc)
{
    int _this = JSApi.getObject((int)JSApi.GetType.Arg);
    JSApi.attachFinalizerObject(_this);
    --argc;

    int len = argc;
    if (len == 0)
    {
        JSMgr.addJSCSRel(_this, new UnityEngine.AudioEchoFilter());
    }

    return true;
}

// fields

// properties
static void AudioEchoFilter_delay(JSVCall vc)
{
    if (vc.bGet)
    { 
        UnityEngine.AudioEchoFilter _this = (UnityEngine.AudioEchoFilter)vc.csObj;
        var result = _this.delay;
                JSApi.setSingle((int)JSApi.SetType.Rval, (System.Single)(result));
    }
    else
    { 
        System.Single arg0 = (System.Single)JSApi.getSingle((int)JSApi.GetType.Arg);
        UnityEngine.AudioEchoFilter _this = (UnityEngine.AudioEchoFilter)vc.csObj;
        _this.delay = arg0;
    }
}
static void AudioEchoFilter_decayRatio(JSVCall vc)
{
    if (vc.bGet)
    { 
        UnityEngine.AudioEchoFilter _this = (UnityEngine.AudioEchoFilter)vc.csObj;
        var result = _this.decayRatio;
                JSApi.setSingle((int)JSApi.SetType.Rval, (System.Single)(result));
    }
    else
    { 
        System.Single arg0 = (System.Single)JSApi.getSingle((int)JSApi.GetType.Arg);
        UnityEngine.AudioEchoFilter _this = (UnityEngine.AudioEchoFilter)vc.csObj;
        _this.decayRatio = arg0;
    }
}
static void AudioEchoFilter_dryMix(JSVCall vc)
{
    if (vc.bGet)
    { 
        UnityEngine.AudioEchoFilter _this = (UnityEngine.AudioEchoFilter)vc.csObj;
        var result = _this.dryMix;
                JSApi.setSingle((int)JSApi.SetType.Rval, (System.Single)(result));
    }
    else
    { 
        System.Single arg0 = (System.Single)JSApi.getSingle((int)JSApi.GetType.Arg);
        UnityEngine.AudioEchoFilter _this = (UnityEngine.AudioEchoFilter)vc.csObj;
        _this.dryMix = arg0;
    }
}
static void AudioEchoFilter_wetMix(JSVCall vc)
{
    if (vc.bGet)
    { 
        UnityEngine.AudioEchoFilter _this = (UnityEngine.AudioEchoFilter)vc.csObj;
        var result = _this.wetMix;
                JSApi.setSingle((int)JSApi.SetType.Rval, (System.Single)(result));
    }
    else
    { 
        System.Single arg0 = (System.Single)JSApi.getSingle((int)JSApi.GetType.Arg);
        UnityEngine.AudioEchoFilter _this = (UnityEngine.AudioEchoFilter)vc.csObj;
        _this.wetMix = arg0;
    }
}

// methods


//register

public static void __Register()
{
    JSMgr.CallbackInfo ci = new JSMgr.CallbackInfo();
    ci.type = typeof(UnityEngine.AudioEchoFilter);
    ci.fields = new JSMgr.CSCallbackField[]
    {

    };
    ci.properties = new JSMgr.CSCallbackProperty[]
    {
        AudioEchoFilter_delay,
        AudioEchoFilter_decayRatio,
        AudioEchoFilter_dryMix,
        AudioEchoFilter_wetMix,

    };
    ci.constructors = new JSMgr.MethodCallBackInfo[]
    {
        new JSMgr.MethodCallBackInfo(AudioEchoFilter_AudioEchoFilter1, ".ctor"),

    };
    ci.methods = new JSMgr.MethodCallBackInfo[]
    {

    };
    JSMgr.allCallbackInfo.Add(ci);
}


}
