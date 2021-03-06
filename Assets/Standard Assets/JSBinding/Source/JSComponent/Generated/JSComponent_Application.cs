﻿//
// Automatically generated by JSComponentGenerator.
//
using UnityEngine;

public class JSComponent_Application : JSComponent
{
    int idOnApplicationFocus;
    int idOnApplicationPause;
    int idOnApplicationQuit;

    protected override void initMemberFunction()
    {
        base.initMemberFunction();
        idOnApplicationFocus = JSApi.getObjFunction(jsObjID, "OnApplicationFocus");
        idOnApplicationPause = JSApi.getObjFunction(jsObjID, "OnApplicationPause");
        idOnApplicationQuit = JSApi.getObjFunction(jsObjID, "OnApplicationQuit");
    }

    void OnApplicationFocus(bool focusStatus)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnApplicationFocus, focusStatus);
    }
    void OnApplicationPause(bool pauseStatus)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnApplicationPause, pauseStatus);
    }
    void OnApplicationQuit()
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnApplicationQuit);
    }

}