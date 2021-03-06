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

public class JSB_UnityEngine_Shader
{

////////////////////// Shader ///////////////////////////////////////
// constructors

static bool Shader_Shader1(JSVCall vc, int argc)
{
    int _this = JSApi.getObject((int)JSApi.GetType.Arg);
    JSApi.attachFinalizerObject(_this);
    --argc;

    int len = argc;
    if (len == 0)
    {
        JSMgr.addJSCSRel(_this, new UnityEngine.Shader());
    }

    return true;
}

// fields

// properties
static void Shader_isSupported(JSVCall vc)
{
        UnityEngine.Shader _this = (UnityEngine.Shader)vc.csObj;
        var result = _this.isSupported;
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(result));
}
static void Shader_maximumLOD(JSVCall vc)
{
    if (vc.bGet)
    { 
        UnityEngine.Shader _this = (UnityEngine.Shader)vc.csObj;
        var result = _this.maximumLOD;
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(result));
    }
    else
    { 
        System.Int32 arg0 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        UnityEngine.Shader _this = (UnityEngine.Shader)vc.csObj;
        _this.maximumLOD = arg0;
    }
}
static void Shader_globalMaximumLOD(JSVCall vc)
{
    if (vc.bGet)
    { 
        var result = UnityEngine.Shader.globalMaximumLOD;
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(result));
    }
    else
    { 
        System.Int32 arg0 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        UnityEngine.Shader.globalMaximumLOD = arg0;
    }
}
static void Shader_renderQueue(JSVCall vc)
{
        UnityEngine.Shader _this = (UnityEngine.Shader)vc.csObj;
        var result = _this.renderQueue;
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(result));
}

// methods

static bool Shader_DisableKeyword__String(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        UnityEngine.Shader.DisableKeyword(arg0);
    }

    return true;
}

static bool Shader_EnableKeyword__String(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        UnityEngine.Shader.EnableKeyword(arg0);
    }

    return true;
}

static bool Shader_Find__String(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, UnityEngine.Shader.Find(arg0));
    }

    return true;
}

static bool Shader_PropertyToID__String(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(UnityEngine.Shader.PropertyToID(arg0)));
    }

    return true;
}

static bool Shader_SetGlobalBuffer__String__ComputeBuffer(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        UnityEngine.ComputeBuffer arg1 = (UnityEngine.ComputeBuffer)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
        UnityEngine.Shader.SetGlobalBuffer(arg0, arg1);
    }

    return true;
}

static bool Shader_SetGlobalColor__String__Color(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        UnityEngine.Color arg1 = (UnityEngine.Color)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
        UnityEngine.Shader.SetGlobalColor(arg0, arg1);
    }

    return true;
}

static bool Shader_SetGlobalColor__Int32__Color(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.Int32 arg0 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        UnityEngine.Color arg1 = (UnityEngine.Color)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
        UnityEngine.Shader.SetGlobalColor(arg0, arg1);
    }

    return true;
}

static bool Shader_SetGlobalFloat__Int32__Single(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.Int32 arg0 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        System.Single arg1 = (System.Single)JSApi.getSingle((int)JSApi.GetType.Arg);
        UnityEngine.Shader.SetGlobalFloat(arg0, arg1);
    }

    return true;
}

static bool Shader_SetGlobalFloat__String__Single(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        System.Single arg1 = (System.Single)JSApi.getSingle((int)JSApi.GetType.Arg);
        UnityEngine.Shader.SetGlobalFloat(arg0, arg1);
    }

    return true;
}

static bool Shader_SetGlobalInt__Int32__Int32(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.Int32 arg0 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        System.Int32 arg1 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        UnityEngine.Shader.SetGlobalInt(arg0, arg1);
    }

    return true;
}

static bool Shader_SetGlobalInt__String__Int32(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        System.Int32 arg1 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        UnityEngine.Shader.SetGlobalInt(arg0, arg1);
    }

    return true;
}

static bool Shader_SetGlobalMatrix__String__Matrix4x4(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        UnityEngine.Matrix4x4 arg1 = (UnityEngine.Matrix4x4)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
        UnityEngine.Shader.SetGlobalMatrix(arg0, arg1);
    }

    return true;
}

static bool Shader_SetGlobalMatrix__Int32__Matrix4x4(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.Int32 arg0 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        UnityEngine.Matrix4x4 arg1 = (UnityEngine.Matrix4x4)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
        UnityEngine.Shader.SetGlobalMatrix(arg0, arg1);
    }

    return true;
}

static bool Shader_SetGlobalTexGenMode__String__TexGenMode(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        UnityEngine.TexGenMode arg1 = (UnityEngine.TexGenMode)JSApi.getEnum((int)JSApi.GetType.Arg);
        UnityEngine.Shader.SetGlobalTexGenMode(arg0, arg1);
    }

    return true;
}

static bool Shader_SetGlobalTexture__String__Texture(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        UnityEngine.Texture arg1 = (UnityEngine.Texture)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
        UnityEngine.Shader.SetGlobalTexture(arg0, arg1);
    }

    return true;
}

static bool Shader_SetGlobalTexture__Int32__Texture(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.Int32 arg0 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        UnityEngine.Texture arg1 = (UnityEngine.Texture)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
        UnityEngine.Shader.SetGlobalTexture(arg0, arg1);
    }

    return true;
}

static bool Shader_SetGlobalTextureMatrixName__String__String(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        System.String arg1 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        UnityEngine.Shader.SetGlobalTextureMatrixName(arg0, arg1);
    }

    return true;
}

static bool Shader_SetGlobalVector__String__Vector4(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        UnityEngine.Vector4 arg1 = (UnityEngine.Vector4)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
        UnityEngine.Shader.SetGlobalVector(arg0, arg1);
    }

    return true;
}

static bool Shader_SetGlobalVector__Int32__Vector4(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.Int32 arg0 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        UnityEngine.Vector4 arg1 = (UnityEngine.Vector4)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
        UnityEngine.Shader.SetGlobalVector(arg0, arg1);
    }

    return true;
}

static bool Shader_WarmupAllShaders(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 0) 
    {
        UnityEngine.Shader.WarmupAllShaders();
    }

    return true;
}


//register

public static void __Register()
{
    JSMgr.CallbackInfo ci = new JSMgr.CallbackInfo();
    ci.type = typeof(UnityEngine.Shader);
    ci.fields = new JSMgr.CSCallbackField[]
    {

    };
    ci.properties = new JSMgr.CSCallbackProperty[]
    {
        Shader_isSupported,
        Shader_maximumLOD,
        Shader_globalMaximumLOD,
        Shader_renderQueue,

    };
    ci.constructors = new JSMgr.MethodCallBackInfo[]
    {
        new JSMgr.MethodCallBackInfo(Shader_Shader1, ".ctor"),

    };
    ci.methods = new JSMgr.MethodCallBackInfo[]
    {
        new JSMgr.MethodCallBackInfo(Shader_DisableKeyword__String, "DisableKeyword"),
        new JSMgr.MethodCallBackInfo(Shader_EnableKeyword__String, "EnableKeyword"),
        new JSMgr.MethodCallBackInfo(Shader_Find__String, "Find"),
        new JSMgr.MethodCallBackInfo(Shader_PropertyToID__String, "PropertyToID"),
        new JSMgr.MethodCallBackInfo(Shader_SetGlobalBuffer__String__ComputeBuffer, "SetGlobalBuffer"),
        new JSMgr.MethodCallBackInfo(Shader_SetGlobalColor__String__Color, "SetGlobalColor"),
        new JSMgr.MethodCallBackInfo(Shader_SetGlobalColor__Int32__Color, "SetGlobalColor"),
        new JSMgr.MethodCallBackInfo(Shader_SetGlobalFloat__Int32__Single, "SetGlobalFloat"),
        new JSMgr.MethodCallBackInfo(Shader_SetGlobalFloat__String__Single, "SetGlobalFloat"),
        new JSMgr.MethodCallBackInfo(Shader_SetGlobalInt__Int32__Int32, "SetGlobalInt"),
        new JSMgr.MethodCallBackInfo(Shader_SetGlobalInt__String__Int32, "SetGlobalInt"),
        new JSMgr.MethodCallBackInfo(Shader_SetGlobalMatrix__String__Matrix4x4, "SetGlobalMatrix"),
        new JSMgr.MethodCallBackInfo(Shader_SetGlobalMatrix__Int32__Matrix4x4, "SetGlobalMatrix"),
        new JSMgr.MethodCallBackInfo(Shader_SetGlobalTexGenMode__String__TexGenMode, "SetGlobalTexGenMode"),
        new JSMgr.MethodCallBackInfo(Shader_SetGlobalTexture__String__Texture, "SetGlobalTexture"),
        new JSMgr.MethodCallBackInfo(Shader_SetGlobalTexture__Int32__Texture, "SetGlobalTexture"),
        new JSMgr.MethodCallBackInfo(Shader_SetGlobalTextureMatrixName__String__String, "SetGlobalTextureMatrixName"),
        new JSMgr.MethodCallBackInfo(Shader_SetGlobalVector__String__Vector4, "SetGlobalVector"),
        new JSMgr.MethodCallBackInfo(Shader_SetGlobalVector__Int32__Vector4, "SetGlobalVector"),
        new JSMgr.MethodCallBackInfo(Shader_WarmupAllShaders, "WarmupAllShaders"),

    };
    JSMgr.allCallbackInfo.Add(ci);
}


}
