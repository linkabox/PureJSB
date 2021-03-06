﻿//
// Automatically generated by JSComponentGenerator.
//
using UnityEngine;

public class JSComponent_GUI_Physics_Render_Visible : JSComponent
{
    int idOnGUI;
    int idOnCollisionEnter;
    int idOnCollisionExit;
    int idOnCollisionStay;
    int idOnTriggerEnter;
    int idOnTriggerExit;
    int idOnTriggerStay;
    int idOnControllerColliderHit;
    int idOnPostRender;
    int idOnPreCull;
    int idOnPreRender;
    int idOnRenderObject;
    int idOnWillRenderObject;
    int idOnBecameInvisible;
    int idOnBecameVisible;

    protected override void initMemberFunction()
    {
        base.initMemberFunction();
        idOnGUI = JSApi.getObjFunction(jsObjID, "OnGUI");
        idOnCollisionEnter = JSApi.getObjFunction(jsObjID, "OnCollisionEnter");
        idOnCollisionExit = JSApi.getObjFunction(jsObjID, "OnCollisionExit");
        idOnCollisionStay = JSApi.getObjFunction(jsObjID, "OnCollisionStay");
        idOnTriggerEnter = JSApi.getObjFunction(jsObjID, "OnTriggerEnter");
        idOnTriggerExit = JSApi.getObjFunction(jsObjID, "OnTriggerExit");
        idOnTriggerStay = JSApi.getObjFunction(jsObjID, "OnTriggerStay");
        idOnControllerColliderHit = JSApi.getObjFunction(jsObjID, "OnControllerColliderHit");
        idOnPostRender = JSApi.getObjFunction(jsObjID, "OnPostRender");
        idOnPreCull = JSApi.getObjFunction(jsObjID, "OnPreCull");
        idOnPreRender = JSApi.getObjFunction(jsObjID, "OnPreRender");
        idOnRenderObject = JSApi.getObjFunction(jsObjID, "OnRenderObject");
        idOnWillRenderObject = JSApi.getObjFunction(jsObjID, "OnWillRenderObject");
        idOnBecameInvisible = JSApi.getObjFunction(jsObjID, "OnBecameInvisible");
        idOnBecameVisible = JSApi.getObjFunction(jsObjID, "OnBecameVisible");
    }

    void OnGUI()
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnGUI);
    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnCollisionEnter, collisionInfo);
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnCollisionExit, collisionInfo);
    }
    void OnCollisionStay(Collision collisionInfo)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnCollisionStay, collisionInfo);
    }
    void OnTriggerEnter(Collider other)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnTriggerEnter, other);
    }
    void OnTriggerExit(Collider other)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnTriggerExit, other);
    }
    void OnTriggerStay(Collider other)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnTriggerStay, other);
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnControllerColliderHit, hit);
    }
    void OnPostRender()
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnPostRender);
    }
    void OnPreCull()
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnPreCull);
    }
    void OnPreRender()
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnPreRender);
    }
    void OnRenderObject()
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnRenderObject);
    }
    void OnWillRenderObject()
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnWillRenderObject);
    }
    void OnBecameInvisible()
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnBecameInvisible);
    }
    void OnBecameVisible()
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnBecameVisible);
    }

}