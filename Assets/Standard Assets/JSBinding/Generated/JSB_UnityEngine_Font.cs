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

public class JSB_UnityEngine_Font
{

////////////////////// Font ///////////////////////////////////////
// constructors

static bool Font_Font1(JSVCall vc, int argc)
{
    int _this = JSApi.getObject((int)JSApi.GetType.Arg);
    JSApi.attachFinalizerObject(_this);
    --argc;

    int len = argc;
    if (len == 0)
    {
        JSMgr.addJSCSRel(_this, new UnityEngine.Font());
    }

    return true;
}

static bool Font_Font2(JSVCall vc, int argc)
{
    int _this = JSApi.getObject((int)JSApi.GetType.Arg);
    JSApi.attachFinalizerObject(_this);
    --argc;

    int len = argc;
    if (len == 1)
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        JSMgr.addJSCSRel(_this, new UnityEngine.Font(arg0));
    }

    return true;
}

// fields

// properties
static void Font_material(JSVCall vc)
{
    if (vc.bGet)
    { 
        UnityEngine.Font _this = (UnityEngine.Font)vc.csObj;
        var result = _this.material;
                JSMgr.datax.setObject((int)JSApi.SetType.Rval, result);
    }
    else
    { 
        UnityEngine.Material arg0 = (UnityEngine.Material)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
        UnityEngine.Font _this = (UnityEngine.Font)vc.csObj;
        _this.material = arg0;
    }
}
static void Font_fontNames(JSVCall vc)
{
    if (vc.bGet)
    { 
        UnityEngine.Font _this = (UnityEngine.Font)vc.csObj;
        var result = _this.fontNames;
                var arrRet = result;
        for (int i = 0; arrRet != null && i < arrRet.Length; i++)
        {
            JSApi.setStringS((int)JSApi.SetType.SaveAndTempTrace, arrRet[i]);
            JSApi.moveSaveID2Arr(i);
        }
        JSApi.setArrayS((int)JSApi.SetType.Rval, (arrRet != null ? arrRet.Length : 0), true);
    }
    else
    { 
        System.String[] arg0 = JSDataExchangeMgr.GetJSArg<System.String[]>(() =>
        {
            int jsObjID = JSApi.getObject((int)JSApi.GetType.Arg);
            int length = JSApi.getArrayLength(jsObjID);
            var ret = new System.String[length];
            for (var i = 0; i < length; i++) {
                JSApi.getElement(jsObjID, i);
                ret[i] = (System.String)JSApi.getStringS((int)JSApi.GetType.SaveAndRemove);
            }
            return ret;
        });
        UnityEngine.Font _this = (UnityEngine.Font)vc.csObj;
        _this.fontNames = arg0;
    }
}
static void Font_characterInfo(JSVCall vc)
{
    if (vc.bGet)
    { 
        UnityEngine.Font _this = (UnityEngine.Font)vc.csObj;
        var result = _this.characterInfo;
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
        UnityEngine.CharacterInfo[] arg0 = JSDataExchangeMgr.GetJSArg<UnityEngine.CharacterInfo[]>(() =>
        {
            int jsObjID = JSApi.getObject((int)JSApi.GetType.Arg);
            int length = JSApi.getArrayLength(jsObjID);
            var ret = new UnityEngine.CharacterInfo[length];
            for (var i = 0; i < length; i++) {
                JSApi.getElement(jsObjID, i);
                ret[i] = (UnityEngine.CharacterInfo)JSMgr.datax.getObject((int)JSApi.GetType.SaveAndRemove);
            }
            return ret;
        });
        UnityEngine.Font _this = (UnityEngine.Font)vc.csObj;
        _this.characterInfo = arg0;
    }
}
static void Font_dynamic(JSVCall vc)
{
        UnityEngine.Font _this = (UnityEngine.Font)vc.csObj;
        var result = _this.dynamic;
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(result));
}
static void Font_fontSize(JSVCall vc)
{
        UnityEngine.Font _this = (UnityEngine.Font)vc.csObj;
        var result = _this.fontSize;
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(result));
}

// methods

static bool Font_GetCharacterInfo__Char__CharacterInfo__Int32__FontStyle(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 4) 
    {
        System.Char arg0 = (System.Char)JSApi.getChar((int)JSApi.GetType.Arg);
        int r_arg1 = JSApi.incArgIndex();
        UnityEngine.CharacterInfo arg1;
        System.Int32 arg2 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        UnityEngine.FontStyle arg3 = (UnityEngine.FontStyle)JSApi.getEnum((int)JSApi.GetType.Arg);
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(((UnityEngine.Font)vc.csObj).GetCharacterInfo(arg0, out arg1, arg2, arg3)));
        JSApi.setArgIndex(r_arg1);
        JSMgr.datax.setObject((int)JSApi.SetType.ArgRef, arg1);
    }

    return true;
}

static bool Font_GetCharacterInfo__Char__CharacterInfo__Int32(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 3) 
    {
        System.Char arg0 = (System.Char)JSApi.getChar((int)JSApi.GetType.Arg);
        int r_arg1 = JSApi.incArgIndex();
        UnityEngine.CharacterInfo arg1;
        System.Int32 arg2 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(((UnityEngine.Font)vc.csObj).GetCharacterInfo(arg0, out arg1, arg2)));
        JSApi.setArgIndex(r_arg1);
        JSMgr.datax.setObject((int)JSApi.SetType.ArgRef, arg1);
    }

    return true;
}

static bool Font_GetCharacterInfo__Char__CharacterInfo(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.Char arg0 = (System.Char)JSApi.getChar((int)JSApi.GetType.Arg);
        int r_arg1 = JSApi.incArgIndex();
        UnityEngine.CharacterInfo arg1;
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(((UnityEngine.Font)vc.csObj).GetCharacterInfo(arg0, out arg1)));
        JSApi.setArgIndex(r_arg1);
        JSMgr.datax.setObject((int)JSApi.SetType.ArgRef, arg1);
    }

    return true;
}

static bool Font_HasCharacter__Char(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.Char arg0 = (System.Char)JSApi.getChar((int)JSApi.GetType.Arg);
                JSApi.setBooleanS((int)JSApi.SetType.Rval, (System.Boolean)(((UnityEngine.Font)vc.csObj).HasCharacter(arg0)));
    }

    return true;
}

static bool Font_RequestCharactersInTexture__String__Int32__FontStyle(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 3) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        System.Int32 arg1 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        UnityEngine.FontStyle arg2 = (UnityEngine.FontStyle)JSApi.getEnum((int)JSApi.GetType.Arg);
        ((UnityEngine.Font)vc.csObj).RequestCharactersInTexture(arg0, arg1, arg2);
    }

    return true;
}

static bool Font_RequestCharactersInTexture__String__Int32(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 2) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        System.Int32 arg1 = (System.Int32)JSApi.getInt32((int)JSApi.GetType.Arg);
        ((UnityEngine.Font)vc.csObj).RequestCharactersInTexture(arg0, arg1);
    }

    return true;
}

static bool Font_RequestCharactersInTexture__String(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
        ((UnityEngine.Font)vc.csObj).RequestCharactersInTexture(arg0);
    }

    return true;
}
public static Action<UnityEngine.Font> Font_add_textureRebuilt_GetDelegate_member7_arg0(CSRepresentedObject objFunction)
{
    if (objFunction == null || objFunction.jsObjID == 0)
    {
        return null;
    }
    var action = JSMgr.getJSFunCSDelegateRel<Action<UnityEngine.Font>>(objFunction.jsObjID);
    if (action == null)
    {
        action = (obj) => 
        {
            JSMgr.vCall.CallJSFunctionValue(0, objFunction.jsObjID, obj);
        };
        JSMgr.addJSFunCSDelegateRel(objFunction.jsObjID, action);
    }
    return action;
}

static bool Font_add_textureRebuilt__ActionT1_Font(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        Action<UnityEngine.Font> action = JSDataExchangeMgr.GetJSArg<Action<UnityEngine.Font>>(()=>
        {
            if (JSApi.isFunctionS((int)JSApi.GetType.Arg))
                return Font_add_textureRebuilt_GetDelegate_member7_arg0(JSApi.getFunctionS((int)JSApi.GetType.Arg));
            else
                return (Action<UnityEngine.Font>)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
        });
        UnityEngine.Font.textureRebuilt += action;
    }

    return true;
}

static bool Font_GetMaxVertsForString__String(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        System.String arg0 = (System.String)JSApi.getStringS((int)JSApi.GetType.Arg);
                JSApi.setInt32((int)JSApi.SetType.Rval, (System.Int32)(UnityEngine.Font.GetMaxVertsForString(arg0)));
    }

    return true;
}
public static Action<UnityEngine.Font> Font_remove_textureRebuilt_GetDelegate_member9_arg0(CSRepresentedObject objFunction)
{
    if (objFunction == null || objFunction.jsObjID == 0)
    {
        return null;
    }
    var action = JSMgr.getJSFunCSDelegateRel<Action<UnityEngine.Font>>(objFunction.jsObjID);
    if (action == null)
    {
        action = (obj) => 
        {
            JSMgr.vCall.CallJSFunctionValue(0, objFunction.jsObjID, obj);
        };
        JSMgr.addJSFunCSDelegateRel(objFunction.jsObjID, action);
    }
    return action;
}

static bool Font_remove_textureRebuilt__ActionT1_Font(JSVCall vc, int argc)
{
    int len = argc;
    if (len == 1) 
    {
        Action<UnityEngine.Font> action = JSDataExchangeMgr.GetJSArg<Action<UnityEngine.Font>>(()=>
        {
            if (JSApi.isFunctionS((int)JSApi.GetType.Arg))
                return Font_remove_textureRebuilt_GetDelegate_member9_arg0(JSApi.getFunctionS((int)JSApi.GetType.Arg));
            else
                return (Action<UnityEngine.Font>)JSMgr.datax.getObject((int)JSApi.GetType.Arg);
        });
        UnityEngine.Font.textureRebuilt -= action;
    }

    return true;
}


//register

public static void __Register()
{
    JSMgr.CallbackInfo ci = new JSMgr.CallbackInfo();
    ci.type = typeof(UnityEngine.Font);
    ci.fields = new JSMgr.CSCallbackField[]
    {

    };
    ci.properties = new JSMgr.CSCallbackProperty[]
    {
        Font_material,
        Font_fontNames,
        Font_characterInfo,
        Font_dynamic,
        Font_fontSize,

    };
    ci.constructors = new JSMgr.MethodCallBackInfo[]
    {
        new JSMgr.MethodCallBackInfo(Font_Font1, ".ctor"),
        new JSMgr.MethodCallBackInfo(Font_Font2, ".ctor"),

    };
    ci.methods = new JSMgr.MethodCallBackInfo[]
    {
        new JSMgr.MethodCallBackInfo(Font_GetCharacterInfo__Char__CharacterInfo__Int32__FontStyle, "GetCharacterInfo"),
        new JSMgr.MethodCallBackInfo(Font_GetCharacterInfo__Char__CharacterInfo__Int32, "GetCharacterInfo"),
        new JSMgr.MethodCallBackInfo(Font_GetCharacterInfo__Char__CharacterInfo, "GetCharacterInfo"),
        new JSMgr.MethodCallBackInfo(Font_HasCharacter__Char, "HasCharacter"),
        new JSMgr.MethodCallBackInfo(Font_RequestCharactersInTexture__String__Int32__FontStyle, "RequestCharactersInTexture"),
        new JSMgr.MethodCallBackInfo(Font_RequestCharactersInTexture__String__Int32, "RequestCharactersInTexture"),
        new JSMgr.MethodCallBackInfo(Font_RequestCharactersInTexture__String, "RequestCharactersInTexture"),
        new JSMgr.MethodCallBackInfo(Font_add_textureRebuilt__ActionT1_Font, "add_textureRebuilt"),
        new JSMgr.MethodCallBackInfo(Font_GetMaxVertsForString__String, "GetMaxVertsForString"),
        new JSMgr.MethodCallBackInfo(Font_remove_textureRebuilt__ActionT1_Font, "remove_textureRebuilt"),

    };
    JSMgr.allCallbackInfo.Add(ci);
}


}