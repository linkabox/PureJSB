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

public class JSB_UnityEngine_SystemInfo
{

////////////////////// SystemInfo ///////////////////////////////////////
// constructors

static bool SystemInfo_SystemInfo1(JSVCall vc, int argc)
{
    int _this = JSApi.getObject((int)JSApi.GetType.Arg);
    JSApi.attachFinalizerObject(_this);
    --argc;

    int len = argc;
    if (len == 0)
    {
        JSMgr.addJSCSRel(_this, new UnityEngine.SystemInfo());
    }

    return true;
}

// fields

// properties
static void SystemInfo_operatingSystem(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.operatingSystem;
                JSApi.setStringS((int)JSApi.SetType.Rval, result);
}
static void SystemInfo_processorType(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.processorType;
                JSApi.setStringS((int)JSApi.SetType.Rval, result);
}
static void SystemInfo_processorCount(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.processorCount;
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(result));
}
static void SystemInfo_systemMemorySize(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.systemMemorySize;
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(result));
}
static void SystemInfo_graphicsMemorySize(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.graphicsMemorySize;
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(result));
}
static void SystemInfo_graphicsDeviceName(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.graphicsDeviceName;
                JSApi.setStringS((int)JSApi.SetType.Rval, result);
}
static void SystemInfo_graphicsDeviceVendor(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.graphicsDeviceVendor;
                JSApi.setStringS((int)JSApi.SetType.Rval, result);
}
static void SystemInfo_graphicsDeviceID(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.graphicsDeviceID;
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(result));
}
static void SystemInfo_graphicsDeviceVendorID(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.graphicsDeviceVendorID;
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(result));
}
static void SystemInfo_graphicsDeviceVersion(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.graphicsDeviceVersion;
                JSApi.setStringS((int)JSApi.SetType.Rval, result);
}
static void SystemInfo_graphicsShaderLevel(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.graphicsShaderLevel;
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(result));
}
static void SystemInfo_graphicsPixelFillrate(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.graphicsPixelFillrate;
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(result));
}
static void SystemInfo_supportsShadows(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.supportsShadows;
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(result));
}
static void SystemInfo_supportsRenderTextures(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.supportsRenderTextures;
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(result));
}
static void SystemInfo_supportsRenderToCubemap(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.supportsRenderToCubemap;
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(result));
}
static void SystemInfo_supportsImageEffects(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.supportsImageEffects;
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(result));
}
static void SystemInfo_supports3DTextures(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.supports3DTextures;
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(result));
}
static void SystemInfo_supportsComputeShaders(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.supportsComputeShaders;
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(result));
}
static void SystemInfo_supportsInstancing(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.supportsInstancing;
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(result));
}
static void SystemInfo_supportsSparseTextures(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.supportsSparseTextures;
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(result));
}
static void SystemInfo_supportedRenderTargetCount(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.supportedRenderTargetCount;
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(result));
}
static void SystemInfo_supportsStencil(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.supportsStencil;
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(result));
}
static void SystemInfo_supportsVertexPrograms(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.supportsVertexPrograms;
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(result));
}
static void SystemInfo_npotSupport(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.npotSupport;
                JSApi.setEnum((int)JSApi.SetType.Rval, (int)result);
}
static void SystemInfo_deviceUniqueIdentifier(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.deviceUniqueIdentifier;
                JSApi.setStringS((int)JSApi.SetType.Rval, result);
}
static void SystemInfo_deviceName(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.deviceName;
                JSApi.setStringS((int)JSApi.SetType.Rval, result);
}
static void SystemInfo_deviceModel(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.deviceModel;
                JSApi.setStringS((int)JSApi.SetType.Rval, result);
}
static void SystemInfo_supportsAccelerometer(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.supportsAccelerometer;
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(result));
}
static void SystemInfo_supportsGyroscope(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.supportsGyroscope;
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(result));
}
static void SystemInfo_supportsLocationService(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.supportsLocationService;
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(result));
}
static void SystemInfo_supportsVibration(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.supportsVibration;
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(result));
}
static void SystemInfo_deviceType(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.deviceType;
                JSApi.setEnum((int)JSApi.SetType.Rval, (int)result);
}
static void SystemInfo_maxTextureSize(JSVCall vc)
{
        var result = UnityEngine.SystemInfo.maxTextureSize;
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(result));
}

// methods

static bool SystemInfo_SupportsRenderTextureFormat__RenderTextureFormat(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        UnityEngine.RenderTextureFormat arg0 = (UnityEngine.RenderTextureFormat)JSApi.getEnum((int)JSApi.GetType.Arg);
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(UnityEngine.SystemInfo.SupportsRenderTextureFormat(arg0)));
    }

    return true;
}


//register

public static void __Register()
{
    JSMgr.CallbackInfo ci = new JSMgr.CallbackInfo();
    ci.type = typeof(UnityEngine.SystemInfo);
    ci.fields = new JSMgr.CSCallbackField[]
    {

    };
    ci.properties = new JSMgr.CSCallbackProperty[]
    {
        SystemInfo_operatingSystem,
        SystemInfo_processorType,
        SystemInfo_processorCount,
        SystemInfo_systemMemorySize,
        SystemInfo_graphicsMemorySize,
        SystemInfo_graphicsDeviceName,
        SystemInfo_graphicsDeviceVendor,
        SystemInfo_graphicsDeviceID,
        SystemInfo_graphicsDeviceVendorID,
        SystemInfo_graphicsDeviceVersion,
        SystemInfo_graphicsShaderLevel,
        SystemInfo_graphicsPixelFillrate,
        SystemInfo_supportsShadows,
        SystemInfo_supportsRenderTextures,
        SystemInfo_supportsRenderToCubemap,
        SystemInfo_supportsImageEffects,
        SystemInfo_supports3DTextures,
        SystemInfo_supportsComputeShaders,
        SystemInfo_supportsInstancing,
        SystemInfo_supportsSparseTextures,
        SystemInfo_supportedRenderTargetCount,
        SystemInfo_supportsStencil,
        SystemInfo_supportsVertexPrograms,
        SystemInfo_npotSupport,
        SystemInfo_deviceUniqueIdentifier,
        SystemInfo_deviceName,
        SystemInfo_deviceModel,
        SystemInfo_supportsAccelerometer,
        SystemInfo_supportsGyroscope,
        SystemInfo_supportsLocationService,
        SystemInfo_supportsVibration,
        SystemInfo_deviceType,
        SystemInfo_maxTextureSize,

    };
    ci.constructors = new JSMgr.MethodCallBackInfo[]
    {
        new JSMgr.MethodCallBackInfo(SystemInfo_SystemInfo1, ".ctor"),

    };
    ci.methods = new JSMgr.MethodCallBackInfo[]
    {
        new JSMgr.MethodCallBackInfo(SystemInfo_SupportsRenderTextureFormat__RenderTextureFormat, "SupportsRenderTextureFormat"),

    };
    JSMgr.allCallbackInfo.Add(ci);
}


}
