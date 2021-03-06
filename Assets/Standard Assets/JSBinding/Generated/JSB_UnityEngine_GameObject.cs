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

public class JSB_UnityEngine_GameObject
{

////////////////////// GameObject ///////////////////////////////////////
// constructors

static bool GameObject_GameObject1(JSVCall vc, int argc)
{
    int _this = JSApi.getObject((int)JSApi.GetType.Arg);
    JSApi.attachFinalizerObject(_this);
    --argc;

    int len = argc;
    if (len == 1)
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        JSMgr.addJSCSRel(_this, new UnityEngine.GameObject(arg0));
    }

    return true;
}

static bool GameObject_GameObject2(JSVCall vc, int argc)
{
    int _this = JSApi.getObject((int)JSApi.GetType.Arg);
    JSApi.attachFinalizerObject(_this);
    --argc;

    int len = argc;
    if (len == 0)
    {
        JSMgr.addJSCSRel(_this, new UnityEngine.GameObject());
    }

    return true;
}

static bool GameObject_GameObject3(JSVCall vc, int argc)
{
    int _this = JSApi.getObject((int)JSApi.GetType.Arg);
    JSApi.attachFinalizerObject(_this);
    --argc;

    int len = argc;
    if (len == 2)
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        System.Type[] arg1 = JSDataExchangeMgr.GetJSArg<System.Type[]>(() =>
        {
            int jsObjID = JSApi.getObject((int)JSApi.GetType.Arg);
            int length = JSApi.getArrayLength(jsObjID);
            var ret = new System.Type[length];
            for (var i = 0; i < length; i++) {
                JSApi.getElement(jsObjID, i);
                ret[i] = (System.Type)JSDataExchangeMgr.GetTypeByJsParam((int)JSApi.GetType.SaveAndRemove);
            }
            return ret;
        });
        JSMgr.addJSCSRel(_this, new UnityEngine.GameObject(arg0, arg1));
    }

    return true;
}

// fields

// properties
static void GameObject_isStatic(JSVCall vc)
{
    if (vc.bGet)
    { 
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.isStatic;
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(result));
    }
    else
    { 
        System.Boolean arg0 = (System.Boolean)JSApi.getBooleanS((int)JSApi.GetType.Arg);
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        _this.isStatic = arg0;
    }
}
static void GameObject_transform(JSVCall vc)
{
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.transform;
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, result);
}
static void GameObject_rigidbody(JSVCall vc)
{
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.rigidbody;
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, result);
}
static void GameObject_rigidbody2D(JSVCall vc)
{
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.rigidbody2D;
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, result);
}
static void GameObject_camera(JSVCall vc)
{
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.camera;
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, result);
}
static void GameObject_light(JSVCall vc)
{
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.light;
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, result);
}
static void GameObject_animation(JSVCall vc)
{
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.animation;
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, result);
}
static void GameObject_constantForce(JSVCall vc)
{
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.constantForce;
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, result);
}
static void GameObject_renderer(JSVCall vc)
{
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.renderer;
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, result);
}
static void GameObject_audio(JSVCall vc)
{
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.audio;
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, result);
}
static void GameObject_guiText(JSVCall vc)
{
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.guiText;
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, result);
}
static void GameObject_guiTexture(JSVCall vc)
{
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.guiTexture;
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, result);
}
static void GameObject_collider(JSVCall vc)
{
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.collider;
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, result);
}
static void GameObject_collider2D(JSVCall vc)
{
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.collider2D;
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, result);
}
static void GameObject_hingeJoint(JSVCall vc)
{
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.hingeJoint;
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, result);
}
static void GameObject_particleEmitter(JSVCall vc)
{
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.particleEmitter;
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, result);
}
static void GameObject_particleSystem(JSVCall vc)
{
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.particleSystem;
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, result);
}
static void GameObject_layer(JSVCall vc)
{
    if (vc.bGet)
    { 
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.layer;
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(result));
    }
    else
    { 
        System.Int32 arg0 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        _this.layer = arg0;
    }
}
static void GameObject_activeSelf(JSVCall vc)
{
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.activeSelf;
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(result));
}
static void GameObject_activeInHierarchy(JSVCall vc)
{
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.activeInHierarchy;
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(result));
}
static void GameObject_tag(JSVCall vc)
{
    if (vc.bGet)
    { 
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.tag;
                JSApi.setStringS((int)JSApi.SetType.Rval, result);
    }
    else
    { 
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        _this.tag = arg0;
    }
}
static void GameObject_gameObject(JSVCall vc)
{
        UnityEngine.GameObject _this = (UnityEngine.GameObject)vc.csObj;
        var result = _this.gameObject;
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, result);
}

// methods

static bool GameObject_AddComponent__Type(JSVCall vc, int argc)
{
    UnityEngineManual.GameObject_AddComponent__Type(vc, argc);
    return true;
}

static bool GameObject_AddComponent__String(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, ((UnityEngine.GameObject)vc.csObj).AddComponent(arg0));
    }

    return true;
}
public static MethodID methodID2 = new MethodID("AddComponent", "T", TypeFlag.IsT, null, null);

static bool GameObject_AddComponentT1(JSVCall vc, int argc)
{
    UnityEngineManual.GameObject_AddComponentT1(vc, argc);
    return true;
}

static bool GameObject_BroadcastMessage__String__Object__SendMessageOptions(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 3) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        System.Object arg1 = (System.Object)JSMgr.datax.getWhatever((int)JSApi.GetType.Arg);
        UnityEngine.SendMessageOptions arg2 = (UnityEngine.SendMessageOptions)JSApi.getEnum((int)JSApi.GetType.Arg);
        ((UnityEngine.GameObject)vc.csObj).BroadcastMessage(arg0, arg1, arg2);
    }

    return true;
}

static bool GameObject_BroadcastMessage__String__Object(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        System.Object arg1 = (System.Object)JSMgr.datax.getWhatever((int)JSApi.GetType.Arg);
        ((UnityEngine.GameObject)vc.csObj).BroadcastMessage(arg0, arg1);
    }

    return true;
}

static bool GameObject_BroadcastMessage__String__SendMessageOptions(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        UnityEngine.SendMessageOptions arg1 = (UnityEngine.SendMessageOptions)JSApi.getEnum((int)JSApi.GetType.Arg);
        ((UnityEngine.GameObject)vc.csObj).BroadcastMessage(arg0, arg1);
    }

    return true;
}

static bool GameObject_BroadcastMessage__String(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        ((UnityEngine.GameObject)vc.csObj).BroadcastMessage(arg0);
    }

    return true;
}

static bool GameObject_CompareTag__String(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(((UnityEngine.GameObject)vc.csObj).CompareTag(arg0)));
    }

    return true;
}

static bool GameObject_GetComponent__Type(JSVCall vc, int argc)
{
    UnityEngineManual.GameObject_GetComponent__Type(vc, argc);
    return true;
}

static bool GameObject_GetComponent__String(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, ((UnityEngine.GameObject)vc.csObj).GetComponent(arg0));
    }

    return true;
}
public static MethodID methodID10 = new MethodID("GetComponent", "T", TypeFlag.IsT, null, null);

static bool GameObject_GetComponentT1(JSVCall vc, int argc)
{
    UnityEngineManual.GameObject_GetComponentT1(vc, argc);
    return true;
}

static bool GameObject_GetComponentInChildren__Type(JSVCall vc, int argc)
{
    UnityEngineManual.GameObject_GetComponentInChildren__Type(vc, argc);
    return true;
}
public static MethodID methodID12 = new MethodID("GetComponentInChildren", "T", TypeFlag.IsT, null, null);

static bool GameObject_GetComponentInChildrenT1(JSVCall vc, int argc)
{
    UnityEngineManual.GameObject_GetComponentInChildrenT1(vc, argc);
    return true;
}

static bool GameObject_GetComponentInParent__Type(JSVCall vc, int argc)
{
    UnityEngineManual.GameObject_GetComponentInParent__Type(vc, argc);
    return true;
}
public static MethodID methodID14 = new MethodID("GetComponentInParent", "T", TypeFlag.IsT, null, null);

static bool GameObject_GetComponentInParentT1(JSVCall vc, int argc)
{
    UnityEngineManual.GameObject_GetComponentInParentT1(vc, argc);
    return true;
}

static bool GameObject_GetComponents__Type__ListT1_Component(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.Type arg0 = (System.Type)JSDataExchangeMgr.GetTypeByJsParam((int)JSApi.GetType.Arg);
        List<UnityEngine.Component> arg1 = (List<UnityEngine.Component>)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
        ((UnityEngine.GameObject)vc.csObj).GetComponents(arg0, arg1);
    }

    return true;
}

static bool GameObject_GetComponents__Type(JSVCall vc, int argc)
{
    UnityEngineManual.GameObject_GetComponents__Type(vc, argc);
    return true;
}
public static MethodID methodID17 = new MethodID("GetComponents", "Void", TypeFlag.None, new string[]{"List`1"}, new TypeFlag[]{TypeFlag.IsGenericType});

static bool GameObject_GetComponentsT1__ListT1_T(JSVCall vc, int argc)
{
    // Get generic method by name and param count.
    MethodInfo method = JSDataExchangeMgr.makeGenericMethod(vc.csObj.GetType(), methodID17, 1); 
    if (method == null) return true;

    int len = argc - 1;
    if (len == 1) 
    {
        object arg0 = (object)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
        object[] arr_t = new object[]{ arg0 };
        method.Invoke(vc.csObj, arr_t);
    }

    return true;
}
public static MethodID methodID18 = new MethodID("GetComponents", "T[]", TypeFlag.None, null, null);

static bool GameObject_GetComponentsT1(JSVCall vc, int argc)
{
    UnityEngineManual.GameObject_GetComponentsT1(vc, argc);
    return true;
}

static bool GameObject_GetComponentsInChildren__Type__Boolean(JSVCall vc, int argc)
{
    UnityEngineManual.GameObject_GetComponentsInChildren__Type__Boolean(vc, argc);
    return true;
}
public static MethodID methodID20 = new MethodID("GetComponentsInChildren", "Void", TypeFlag.None, new string[]{"Boolean", "List`1"}, new TypeFlag[]{TypeFlag.None, TypeFlag.IsGenericType});

static bool GameObject_GetComponentsInChildrenT1__Boolean__ListT1_T(JSVCall vc, int argc)
{
    // Get generic method by name and param count.
    MethodInfo method = JSDataExchangeMgr.makeGenericMethod(vc.csObj.GetType(), methodID20, 1); 
    if (method == null) return true;

    int len = argc - 1;
    if (len == 2) 
    {
        System.Boolean arg0 = (System.Boolean)JSApi.getBooleanS((int)JSApi.GetType.Arg);
        object arg1 = (object)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
        object[] arr_t = new object[]{ arg0, arg1 };
        method.Invoke(vc.csObj, arr_t);
    }

    return true;
}
public static MethodID methodID21 = new MethodID("GetComponentsInChildren", "Void", TypeFlag.None, new string[]{"List`1"}, new TypeFlag[]{TypeFlag.IsGenericType});

static bool GameObject_GetComponentsInChildrenT1__ListT1_T(JSVCall vc, int argc)
{
    // Get generic method by name and param count.
    MethodInfo method = JSDataExchangeMgr.makeGenericMethod(vc.csObj.GetType(), methodID21, 1); 
    if (method == null) return true;

    int len = argc - 1;
    if (len == 1) 
    {
        object arg0 = (object)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
        object[] arr_t = new object[]{ arg0 };
        method.Invoke(vc.csObj, arr_t);
    }

    return true;
}

static bool GameObject_GetComponentsInChildren__Type(JSVCall vc, int argc)
{
    UnityEngineManual.GameObject_GetComponentsInChildren__Type(vc, argc);
    return true;
}
public static MethodID methodID23 = new MethodID("GetComponentsInChildren", "T[]", TypeFlag.None, new string[]{"Boolean"}, new TypeFlag[]{TypeFlag.None});

static bool GameObject_GetComponentsInChildrenT1__Boolean(JSVCall vc, int argc)
{
    UnityEngineManual.GameObject_GetComponentsInChildrenT1__Boolean(vc, argc);
    return true;
}
public static MethodID methodID24 = new MethodID("GetComponentsInChildren", "T[]", TypeFlag.None, null, null);

static bool GameObject_GetComponentsInChildrenT1(JSVCall vc, int argc)
{
    UnityEngineManual.GameObject_GetComponentsInChildrenT1(vc, argc);
    return true;
}
public static MethodID methodID25 = new MethodID("GetComponentsInParent", "Void", TypeFlag.None, new string[]{"Boolean", "List`1"}, new TypeFlag[]{TypeFlag.None, TypeFlag.IsGenericType});

static bool GameObject_GetComponentsInParentT1__Boolean__ListT1_T(JSVCall vc, int argc)
{
    // Get generic method by name and param count.
    MethodInfo method = JSDataExchangeMgr.makeGenericMethod(vc.csObj.GetType(), methodID25, 1); 
    if (method == null) return true;

    int len = argc - 1;
    if (len == 2) 
    {
        System.Boolean arg0 = (System.Boolean)JSApi.getBooleanS((int)JSApi.GetType.Arg);
        object arg1 = (object)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
        object[] arr_t = new object[]{ arg0, arg1 };
        method.Invoke(vc.csObj, arr_t);
    }

    return true;
}

static bool GameObject_GetComponentsInParent__Type__Boolean(JSVCall vc, int argc)
{
    UnityEngineManual.GameObject_GetComponentsInParent__Type__Boolean(vc, argc);
    return true;
}
public static MethodID methodID27 = new MethodID("GetComponentsInParent", "T[]", TypeFlag.None, new string[]{"Boolean"}, new TypeFlag[]{TypeFlag.None});

static bool GameObject_GetComponentsInParentT1__Boolean(JSVCall vc, int argc)
{
    UnityEngineManual.GameObject_GetComponentsInParentT1__Boolean(vc, argc);
    return true;
}

static bool GameObject_GetComponentsInParent__Type(JSVCall vc, int argc)
{
    UnityEngineManual.GameObject_GetComponentsInParent__Type(vc, argc);
    return true;
}
public static MethodID methodID29 = new MethodID("GetComponentsInParent", "T[]", TypeFlag.None, null, null);

static bool GameObject_GetComponentsInParentT1(JSVCall vc, int argc)
{
    UnityEngineManual.GameObject_GetComponentsInParentT1(vc, argc);
    return true;
}

static bool GameObject_SampleAnimation__AnimationClip__Single(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        UnityEngine.AnimationClip arg0 = (UnityEngine.AnimationClip)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
        System.Single arg1 = (System.Single)JSApi.getSingle((int)JSApi.GetType.Arg);
        ((UnityEngine.GameObject)vc.csObj).SampleAnimation(arg0, arg1);
    }

    return true;
}

static bool GameObject_SendMessage__String__Object__SendMessageOptions(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 3) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        System.Object arg1 = (System.Object)JSMgr.datax.getWhatever((int)JSApi.GetType.Arg);
        UnityEngine.SendMessageOptions arg2 = (UnityEngine.SendMessageOptions)JSApi.getEnum((int)JSApi.GetType.Arg);
        ((UnityEngine.GameObject)vc.csObj).SendMessage(arg0, arg1, arg2);
    }

    return true;
}

static bool GameObject_SendMessage__String__SendMessageOptions(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        UnityEngine.SendMessageOptions arg1 = (UnityEngine.SendMessageOptions)JSApi.getEnum((int)JSApi.GetType.Arg);
        ((UnityEngine.GameObject)vc.csObj).SendMessage(arg0, arg1);
    }

    return true;
}

static bool GameObject_SendMessage__String__Object(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        System.Object arg1 = (System.Object)JSMgr.datax.getWhatever((int)JSApi.GetType.Arg);
        ((UnityEngine.GameObject)vc.csObj).SendMessage(arg0, arg1);
    }

    return true;
}

static bool GameObject_SendMessage__String(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        ((UnityEngine.GameObject)vc.csObj).SendMessage(arg0);
    }

    return true;
}

static bool GameObject_SendMessageUpwards__String__Object__SendMessageOptions(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 3) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        System.Object arg1 = (System.Object)JSMgr.datax.getWhatever((int)JSApi.GetType.Arg);
        UnityEngine.SendMessageOptions arg2 = (UnityEngine.SendMessageOptions)JSApi.getEnum((int)JSApi.GetType.Arg);
        ((UnityEngine.GameObject)vc.csObj).SendMessageUpwards(arg0, arg1, arg2);
    }

    return true;
}

static bool GameObject_SendMessageUpwards__String__Object(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        System.Object arg1 = (System.Object)JSMgr.datax.getWhatever((int)JSApi.GetType.Arg);
        ((UnityEngine.GameObject)vc.csObj).SendMessageUpwards(arg0, arg1);
    }

    return true;
}

static bool GameObject_SendMessageUpwards__String__SendMessageOptions(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        UnityEngine.SendMessageOptions arg1 = (UnityEngine.SendMessageOptions)JSApi.getEnum((int)JSApi.GetType.Arg);
        ((UnityEngine.GameObject)vc.csObj).SendMessageUpwards(arg0, arg1);
    }

    return true;
}

static bool GameObject_SendMessageUpwards__String(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        ((UnityEngine.GameObject)vc.csObj).SendMessageUpwards(arg0);
    }

    return true;
}

static bool GameObject_SetActive__Boolean(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.Boolean arg0 = (System.Boolean)JSApi.getBooleanS((int)JSApi.GetType.Arg);
        ((UnityEngine.GameObject)vc.csObj).SetActive(arg0);
    }

    return true;
}

static bool GameObject_CreatePrimitive__PrimitiveType(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        UnityEngine.PrimitiveType arg0 = (UnityEngine.PrimitiveType)JSApi.getEnum((int)JSApi.GetType.Arg);
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, UnityEngine.GameObject.CreatePrimitive(arg0));
    }

    return true;
}

static bool GameObject_Find__String(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, UnityEngine.GameObject.Find(arg0));
    }

    return true;
}

static bool GameObject_FindGameObjectsWithTag__String(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
                var arrRet = UnityEngine.GameObject.FindGameObjectsWithTag(arg0);
        for (int i = 0; arrRet != null && i < arrRet.Length; i++)
        {
            JSMgr.datax.setObject((int)JSApi.SetType.SaveAndTempTrace, arrRet[i]);
            JSApi.moveSaveID2Arr(i);
        }
        JSApi.setArrayS((int)JSApi.SetType.Rval, (arrRet != null ? arrRet.Length : 0), true);
    }

    return true;
}

static bool GameObject_FindGameObjectWithTag__String(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, UnityEngine.GameObject.FindGameObjectWithTag(arg0));
    }

    return true;
}

static bool GameObject_FindWithTag__String(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, UnityEngine.GameObject.FindWithTag(arg0));
    }

    return true;
}


//register

public static void __Register()
{
    JSMgr.CallbackInfo ci = new JSMgr.CallbackInfo();
    ci.type = typeof(UnityEngine.GameObject);
    ci.fields = new JSMgr.CSCallbackField[]
    {

    };
    ci.properties = new JSMgr.CSCallbackProperty[]
    {
        GameObject_isStatic,
        GameObject_transform,
        GameObject_rigidbody,
        GameObject_rigidbody2D,
        GameObject_camera,
        GameObject_light,
        GameObject_animation,
        GameObject_constantForce,
        GameObject_renderer,
        GameObject_audio,
        GameObject_guiText,
        GameObject_guiTexture,
        GameObject_collider,
        GameObject_collider2D,
        GameObject_hingeJoint,
        GameObject_particleEmitter,
        GameObject_particleSystem,
        GameObject_layer,
        GameObject_activeSelf,
        GameObject_activeInHierarchy,
        GameObject_tag,
        GameObject_gameObject,

    };
    ci.constructors = new JSMgr.MethodCallBackInfo[]
    {
        new JSMgr.MethodCallBackInfo(GameObject_GameObject1, ".ctor"),
        new JSMgr.MethodCallBackInfo(GameObject_GameObject2, ".ctor"),
        new JSMgr.MethodCallBackInfo(GameObject_GameObject3, ".ctor"),

    };
    ci.methods = new JSMgr.MethodCallBackInfo[]
    {
        new JSMgr.MethodCallBackInfo(GameObject_AddComponent__Type, "AddComponent"),
        new JSMgr.MethodCallBackInfo(GameObject_AddComponent__String, "AddComponent"),
        new JSMgr.MethodCallBackInfo(GameObject_AddComponentT1, "AddComponent"),
        new JSMgr.MethodCallBackInfo(GameObject_BroadcastMessage__String__Object__SendMessageOptions, "BroadcastMessage"),
        new JSMgr.MethodCallBackInfo(GameObject_BroadcastMessage__String__Object, "BroadcastMessage"),
        new JSMgr.MethodCallBackInfo(GameObject_BroadcastMessage__String__SendMessageOptions, "BroadcastMessage"),
        new JSMgr.MethodCallBackInfo(GameObject_BroadcastMessage__String, "BroadcastMessage"),
        new JSMgr.MethodCallBackInfo(GameObject_CompareTag__String, "CompareTag"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponent__Type, "GetComponent"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponent__String, "GetComponent"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponentT1, "GetComponent"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponentInChildren__Type, "GetComponentInChildren"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponentInChildrenT1, "GetComponentInChildren"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponentInParent__Type, "GetComponentInParent"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponentInParentT1, "GetComponentInParent"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponents__Type__ListT1_Component, "GetComponents"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponents__Type, "GetComponents"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponentsT1__ListT1_T, "GetComponents"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponentsT1, "GetComponents"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponentsInChildren__Type__Boolean, "GetComponentsInChildren"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponentsInChildrenT1__Boolean__ListT1_T, "GetComponentsInChildren"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponentsInChildrenT1__ListT1_T, "GetComponentsInChildren"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponentsInChildren__Type, "GetComponentsInChildren"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponentsInChildrenT1__Boolean, "GetComponentsInChildren"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponentsInChildrenT1, "GetComponentsInChildren"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponentsInParentT1__Boolean__ListT1_T, "GetComponentsInParent"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponentsInParent__Type__Boolean, "GetComponentsInParent"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponentsInParentT1__Boolean, "GetComponentsInParent"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponentsInParent__Type, "GetComponentsInParent"),
        new JSMgr.MethodCallBackInfo(GameObject_GetComponentsInParentT1, "GetComponentsInParent"),
        new JSMgr.MethodCallBackInfo(GameObject_SampleAnimation__AnimationClip__Single, "SampleAnimation"),
        new JSMgr.MethodCallBackInfo(GameObject_SendMessage__String__Object__SendMessageOptions, "SendMessage"),
        new JSMgr.MethodCallBackInfo(GameObject_SendMessage__String__SendMessageOptions, "SendMessage"),
        new JSMgr.MethodCallBackInfo(GameObject_SendMessage__String__Object, "SendMessage"),
        new JSMgr.MethodCallBackInfo(GameObject_SendMessage__String, "SendMessage"),
        new JSMgr.MethodCallBackInfo(GameObject_SendMessageUpwards__String__Object__SendMessageOptions, "SendMessageUpwards"),
        new JSMgr.MethodCallBackInfo(GameObject_SendMessageUpwards__String__Object, "SendMessageUpwards"),
        new JSMgr.MethodCallBackInfo(GameObject_SendMessageUpwards__String__SendMessageOptions, "SendMessageUpwards"),
        new JSMgr.MethodCallBackInfo(GameObject_SendMessageUpwards__String, "SendMessageUpwards"),
        new JSMgr.MethodCallBackInfo(GameObject_SetActive__Boolean, "SetActive"),
        new JSMgr.MethodCallBackInfo(GameObject_CreatePrimitive__PrimitiveType, "CreatePrimitive"),
        new JSMgr.MethodCallBackInfo(GameObject_Find__String, "Find"),
        new JSMgr.MethodCallBackInfo(GameObject_FindGameObjectsWithTag__String, "FindGameObjectsWithTag"),
        new JSMgr.MethodCallBackInfo(GameObject_FindGameObjectWithTag__String, "FindGameObjectWithTag"),
        new JSMgr.MethodCallBackInfo(GameObject_FindWithTag__String, "FindWithTag"),

    };
    JSMgr.allCallbackInfo.Add(ci);
}


}
