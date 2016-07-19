/*
 * JSVCall
 * 
 * It's the STACK used when calling cs from js
 * 
 */


using UnityEngine;
//using UnityEditor;
using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

using jsval = JSApi.jsval;

/// <summary>
/// JSVCall负责C#->Js的调用交互操作
/// </summary>
public class JSVCall
{
    public bool CallJSFunctionValue(int jsObjID, int funID)
    {
        if (funID <= 0) return false;
        if (JSMgr.IsShutDown) return false;

        JSApi.callFunctionValue(jsObjID, funID, 0);
        return true;
    }

    #region CallJSFunctionValue One Param

    public bool CallJSFunctionValue(int jsObjID, int funID, object arg1)
    {
        if (funID <= 0) return false;
        if (JSMgr.IsShutDown) return false;

        // TODO memory leak
        JSMgr.datax.setWhatever((int)JSApi.SetType.SaveAndTempTrace, arg1);
        JSApi.moveSaveID2Arr(0);

        JSApi.callFunctionValue(jsObjID, funID, 1);
        return true;
    }

    public bool CallJSFunctionValue(int jsObjID, int funID, char arg1)
    {
        if (funID <= 0) return false;
        if (JSMgr.IsShutDown) return false;

        // TODO memory leak
        JSApi.setChar((int)JSApi.SetType.SaveAndTempTrace, arg1);
        JSApi.moveSaveID2Arr(0);

        JSApi.callFunctionValue(jsObjID, funID, 1);
        return true;
    }

    public bool CallJSFunctionValue(int jsObjID, int funID, string arg1)
    {
        if (funID <= 0) return false;
        if (JSMgr.IsShutDown) return false;

        // TODO memory leak
        if (arg1 == null)
        {
            JSApi.setUndefined((int)JSApi.SetType.SaveAndTempTrace);
        }
        else
        {
            JSApi.setStringS((int)JSApi.SetType.SaveAndTempTrace,arg1);
        }
        JSApi.moveSaveID2Arr(0);

        JSApi.callFunctionValue(jsObjID, funID, 1);
        return true;
    }

    public bool CallJSFunctionValue(int jsObjID, int funID, byte arg1)
    {
        if (funID <= 0) return false;
        if (JSMgr.IsShutDown) return false;

        // TODO memory leak
        JSApi.setByte((int)JSApi.SetType.SaveAndTempTrace, arg1);
        JSApi.moveSaveID2Arr(0);

        JSApi.callFunctionValue(jsObjID, funID, 1);
        return true;
    }
    public bool CallJSFunctionValue(int jsObjID, int funID, sbyte arg1)
    {
        if (funID <= 0) return false;
        if (JSMgr.IsShutDown) return false;

        // TODO memory leak
        JSApi.setSByte((int)JSApi.SetType.SaveAndTempTrace, arg1);
        JSApi.moveSaveID2Arr(0);

        JSApi.callFunctionValue(jsObjID, funID, 1);
        return true;
    }
    public bool CallJSFunctionValue(int jsObjID, int funID, ushort arg1)
    {
        if (funID <= 0) return false;
        if (JSMgr.IsShutDown) return false;

        // TODO memory leak
        JSApi.setUInt16((int)JSApi.SetType.SaveAndTempTrace, arg1);
        JSApi.moveSaveID2Arr(0);

        JSApi.callFunctionValue(jsObjID, funID, 1);
        return true;
    }
    public bool CallJSFunctionValue(int jsObjID, int funID, short arg1)
    {
        if (funID <= 0) return false;
        if (JSMgr.IsShutDown) return false;

        // TODO memory leak
        JSApi.setInt16((int)JSApi.SetType.SaveAndTempTrace, arg1);
        JSApi.moveSaveID2Arr(0);

        JSApi.callFunctionValue(jsObjID, funID, 1);
        return true;
    }

    public bool CallJSFunctionValue(int jsObjID, int funID, uint arg1)
    {
        if (funID <= 0) return false;
        if (JSMgr.IsShutDown) return false;

        // TODO memory leak
        JSApi.setUInt32((int)JSApi.SetType.SaveAndTempTrace, arg1);
        JSApi.moveSaveID2Arr(0);

        JSApi.callFunctionValue(jsObjID, funID, 1);
        return true;
    }

    public bool CallJSFunctionValue(int jsObjID, int funID, int arg1)
    {
        if (funID <= 0) return false;
        if (JSMgr.IsShutDown) return false;

        // TODO memory leak
        JSApi.setInt32((int)JSApi.SetType.SaveAndTempTrace, arg1);
        JSApi.moveSaveID2Arr(0);

        JSApi.callFunctionValue(jsObjID, funID, 1);
        return true;
    }

    public bool CallJSFunctionValue(int jsObjID, int funID, ulong arg1)
    {
        if (funID <= 0) return false;
        if (JSMgr.IsShutDown) return false;

        // TODO memory leak
        JSApi.setUInt64((int)JSApi.SetType.SaveAndTempTrace, arg1);
        JSApi.moveSaveID2Arr(0);

        JSApi.callFunctionValue(jsObjID, funID, 1);
        return true;
    }

    public bool CallJSFunctionValue(int jsObjID, int funID, long arg1)
    {
        if (funID <= 0) return false;
        if (JSMgr.IsShutDown) return false;

        // TODO memory leak
        JSApi.setInt64((int)JSApi.SetType.SaveAndTempTrace, arg1);
        JSApi.moveSaveID2Arr(0);

        JSApi.callFunctionValue(jsObjID, funID, 1);
        return true;
    }

    public bool CallJSFunctionValue(int jsObjID, int funID, float arg1)
    {
        if (funID <= 0) return false;
        if (JSMgr.IsShutDown) return false;

        // TODO memory leak
        JSApi.setSingle((int)JSApi.SetType.SaveAndTempTrace, arg1);
        JSApi.moveSaveID2Arr(0);

        JSApi.callFunctionValue(jsObjID, funID, 1);
        return true;
    }

    public bool CallJSFunctionValue(int jsObjID, int funID, double arg1)
    {
        if (funID <= 0) return false;
        if (JSMgr.IsShutDown) return false;

        // TODO memory leak
        JSApi.setDouble((int)JSApi.SetType.SaveAndTempTrace, arg1);
        JSApi.moveSaveID2Arr(0);

        JSApi.callFunctionValue(jsObjID, funID, 1);
        return true;
    }
    #endregion

    #region CallJSFunctionValue More than One Param

    public bool CallJSFunctionValue(int jsObjID, int funID, object arg1, object arg2)
    {
        if (funID <= 0) return false;
        if (JSMgr.IsShutDown) return false;

        // TODO memory leak
        JSMgr.datax.setWhatever((int)JSApi.SetType.SaveAndTempTrace, arg1);
        JSApi.moveSaveID2Arr(0);

        // TODO memory leak
        JSMgr.datax.setWhatever((int)JSApi.SetType.SaveAndTempTrace, arg2);
        JSApi.moveSaveID2Arr(1);

        JSApi.callFunctionValue(jsObjID, funID, 2);
        return true;
    }
    public bool CallJSFunctionValue(int jsObjID, int funID, object arg1, object arg2, object arg3)
    {
        if (funID <= 0) return false;
        if (JSMgr.IsShutDown) return false;

        // TODO memory leak
        JSMgr.datax.setWhatever((int)JSApi.SetType.SaveAndTempTrace, arg1);
        JSApi.moveSaveID2Arr(0);

        // TODO memory leak
        JSMgr.datax.setWhatever((int)JSApi.SetType.SaveAndTempTrace, arg2);
        JSApi.moveSaveID2Arr(1);

        // TODO memory leak
        JSMgr.datax.setWhatever((int)JSApi.SetType.SaveAndTempTrace, arg3);
        JSApi.moveSaveID2Arr(2);

        JSApi.callFunctionValue(jsObjID, funID, 3);
        return true;
    }

    public bool CallJSFunctionValue(int jsObjID, int funID, params object[] args)
    {
        if (funID <= 0) return false;
        if (JSMgr.IsShutDown) return false;

        int argsLen = (args != null ? args.Length : 0);
        if (argsLen == 0)
        {
            JSApi.callFunctionValue(jsObjID, funID, 0);
            return true;
        }

        for (int i = 0; i < argsLen; i++)
        {
            // TODO memory leak
            JSMgr.datax.setWhatever((int)JSApi.SetType.SaveAndTempTrace, args[i]);
            JSApi.moveSaveID2Arr(i);
        }

        JSApi.callFunctionValue(jsObjID, funID, argsLen);
        return true;
    }

    #endregion

    #region CallJSFunctionName

    public bool CallJSFunctionName(int jsObjID, string funName)
    {
        if (JSMgr.IsShutDown) return false;
        int funID = JSApi.getObjFunction(jsObjID, funName);
        if (funID <= 0)
            return false;

        return CallJSFunctionValue(jsObjID, funID);
    }
    public bool CallJSFunctionName(int jsObjID, string funName, object arg1)
    {
        if (JSMgr.IsShutDown) return false;
        int funID = JSApi.getObjFunction(jsObjID, funName);
        if (funID <= 0)
            return false;

        return CallJSFunctionValue(jsObjID, funID, arg1);
    }
    public bool CallJSFunctionName(int jsObjID, string funName, object arg1, object arg2)
    {
        if (JSMgr.IsShutDown) return false;
        int funID = JSApi.getObjFunction(jsObjID, funName);
        if (funID <= 0)
            return false;

        return CallJSFunctionValue(jsObjID, funID, arg1, arg2);
    }
    public bool CallJSFunctionName(int jsObjID, string funName, object arg1, object arg2, object arg3)
    {
        if (JSMgr.IsShutDown) return false;
        int funID = JSApi.getObjFunction(jsObjID, funName);
        if (funID <= 0)
            return false;

        return CallJSFunctionValue(jsObjID, funID, arg1, arg2, arg3);
    }
    public bool CallJSFunctionName(int jsObjID, string funName, params object[] args)
    {
        if (JSMgr.IsShutDown) return false;
        int funID = JSApi.getObjFunction(jsObjID, funName);
        if (funID <= 0)
            return false;

        return CallJSFunctionValue(jsObjID, funID, args);
    }

    #endregion

    public enum Oper
    {
        GET_FIELD = 0,
        SET_FIELD = 1,
        GET_PROPERTY = 2,
        SET_PROPERTY = 3,
        METHOD = 4,
        CONSTRUCTOR = 5,
    }

    public bool bGet = false; // for property
    public int jsObjID = 0;
    public object csObj;

    public int jsCallCount = 0;
#if UNITY_EDITOR
    public StringBuilder jsCallInfoSb = new StringBuilder();
#endif
    public bool CallCallback(int iOP, int slot, int index, int isStatic, int argc)
    {
        jsCallCount++;
        this.jsObjID = 0;
        this.csObj = null;

        Oper op = (Oper)iOP;

        if (slot < 0 || slot >= JSMgr.allCallbackInfo.Count)
        {
            throw (new Exception("Bad slot: " + slot));
            //return false;
        }
        JSMgr.CallbackInfo aInfo = JSMgr.allCallbackInfo[slot];
#if UNITY_EDITOR
        if(JSEngine.inst.showStatistics)
            jsCallInfoSb.AppendFormat("Type:{0} Op:{1} index:{2}\n", aInfo.type, op, index);
#endif
        if (isStatic == 0)
        {
            this.jsObjID = JSApi.getObject((int)JSApi.GetType.Arg);
            if (this.jsObjID == 0)
            {
                throw (new Exception("Invalid this jsObjID"));
                //return false;
            }

            // for manual javascript code, this.csObj will be null
            this.csObj = JSMgr.getCSObj(jsObjID);
            //if (this.csObj == null) {
            //	throw(new Exception("Invalid this csObj"));
            //    return JSApi.JS_FALSE;
            //}

            --argc;
        }

        switch (op)
        {
            case Oper.GET_FIELD:
            case Oper.SET_FIELD:
                {
                    this.bGet = (op == Oper.GET_FIELD);
                    JSMgr.CSCallbackField fun = aInfo.fields[index];
                    if (fun == null)
                    {
                        throw (new Exception("Field not found"));
                        //return false;
                    }
                    fun(this);
                }
                break;
            case Oper.GET_PROPERTY:
            case Oper.SET_PROPERTY:
                {
                    this.bGet = (op == Oper.GET_PROPERTY);
                    JSMgr.CSCallbackProperty fun = aInfo.properties[index];
                    if (fun == null)
                    {
                        throw (new Exception("Property not found"));
                        //return false;
                    }
                    fun(this);
                }
                break;
            case Oper.METHOD:
            case Oper.CONSTRUCTOR:
                {
                    JSMgr.MethodCallBackInfo[] arrMethod;
                    if (op == Oper.METHOD)
                        arrMethod = aInfo.methods;
                    else
                        arrMethod = aInfo.constructors;

                    arrMethod[index].fun(this, argc);
                }
                break;
        }
        return true;
    }
}