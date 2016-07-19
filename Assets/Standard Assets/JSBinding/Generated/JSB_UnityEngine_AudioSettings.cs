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

public class JSB_UnityEngine_AudioSettings
{

////////////////////// AudioSettings ///////////////////////////////////////
// constructors

static bool AudioSettings_AudioSettings1(JSVCall vc, int argc)
{
    int _this = JSApi.getObject((int)JSApi.GetType.Arg);
    JSApi.attachFinalizerObject(_this);
    --argc;

    int len = argc;
    if (len == 0)
    {
        JSMgr.addJSCSRel(_this, new UnityEngine.AudioSettings());
    }

    return true;
}

// fields

// properties
static void AudioSettings_driverCaps(JSVCall vc)
{
        var result = UnityEngine.AudioSettings.driverCaps;
                JSApi.setEnum((int)JSApi.SetType.Rval, (int)result);
}
static void AudioSettings_speakerMode(JSVCall vc)
{
    if (vc.bGet)
    { 
        var result = UnityEngine.AudioSettings.speakerMode;
                JSApi.setEnum((int)JSApi.SetType.Rval, (int)result);
    }
    else
    { 
        UnityEngine.AudioSpeakerMode arg0 = (UnityEngine.AudioSpeakerMode)JSApi.getEnum((int)JSApi.GetType.Arg);
        UnityEngine.AudioSettings.speakerMode = arg0;
    }
}
static void AudioSettings_dspTime(JSVCall vc)
{
        var result = UnityEngine.AudioSettings.dspTime;
                JSApi.setDouble((int)JSApi.SetType.Rval, (System.Double)(result));
}
static void AudioSettings_outputSampleRate(JSVCall vc)
{
    if (vc.bGet)
    { 
        var result = UnityEngine.AudioSettings.outputSampleRate;
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(result));
    }
    else
    { 
        System.Int32 arg0 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        UnityEngine.AudioSettings.outputSampleRate = arg0;
    }
}

// methods

static bool AudioSettings_GetDSPBufferSize__Int32__Int32(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        int r_arg0 = JSApi.incArgIndex();
        System.Int32 arg0;
        int r_arg1 = JSApi.incArgIndex();
        System.Int32 arg1;
        UnityEngine.AudioSettings.GetDSPBufferSize(out arg0, out arg1);
        JSApi.setArgIndex(r_arg0);
        JSApi.setInt32((int)JSApi.SetType.ArgRef, arg0);
        JSApi.setArgIndex(r_arg1);
        JSApi.setInt32((int)JSApi.SetType.ArgRef, arg1);
    }

    return true;
}

static bool AudioSettings_SetDSPBufferSize__Int32__Int32(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.Int32 arg0 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        System.Int32 arg1 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        UnityEngine.AudioSettings.SetDSPBufferSize(arg0, arg1);
    }

    return true;
}


//register

public static void __Register()
{
    JSMgr.CallbackInfo ci = new JSMgr.CallbackInfo();
    ci.type = typeof(UnityEngine.AudioSettings);
    ci.fields = new JSMgr.CSCallbackField[]
    {

    };
    ci.properties = new JSMgr.CSCallbackProperty[]
    {
        AudioSettings_driverCaps,
        AudioSettings_speakerMode,
        AudioSettings_dspTime,
        AudioSettings_outputSampleRate,

    };
    ci.constructors = new JSMgr.MethodCallBackInfo[]
    {
        new JSMgr.MethodCallBackInfo(AudioSettings_AudioSettings1, ".ctor"),

    };
    ci.methods = new JSMgr.MethodCallBackInfo[]
    {
        new JSMgr.MethodCallBackInfo(AudioSettings_GetDSPBufferSize__Int32__Int32, "GetDSPBufferSize"),
        new JSMgr.MethodCallBackInfo(AudioSettings_SetDSPBufferSize__Int32__Int32, "SetDSPBufferSize"),

    };
    JSMgr.allCallbackInfo.Add(ci);
}


}