using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/**
 * 如果脚本继承了以下几种接口，则统一用这个替换
 * 这个只支持 Awake Start Update LateUpdate
 * 
 */

public class JSComponent_EventTrigger : JSComponent,
    IEventSystemHandler,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerClickHandler,
    IBeginDragHandler,
    IInitializePotentialDragHandler,
    IDragHandler,
    IEndDragHandler,
    IDropHandler,
    IScrollHandler,
    IUpdateSelectedHandler,
    ISelectHandler,
    IDeselectHandler,
    IMoveHandler,
    ISubmitHandler,
    ICancelHandler
{
    int idUpdate;
    int idLateUpdate;

    int idOnBeginDrag;
    int idOnCancel;
    int idOnDeselect;
    int idOnDrag;
    int idOnDrop;
    int idOnEndDrag;
    int idOnInitializePotentialDrag;
    int idOnMove;
    int idOnPointerClick;
    int idOnPointerDown;
    int idOnPointerEnter;
    int idOnPointerExit;
    int idOnPointerUp;
    int idOnScroll;
    int idOnSelect;
    int idOnSubmit;
    int idOnUpdateSelected;

    protected override void initMemberFunction()
    {
        base.initMemberFunction();
        idUpdate = JSApi.getObjFunction(jsObjID, "Update");
        idLateUpdate = JSApi.getObjFunction(jsObjID, "LateUpdate");

        idOnBeginDrag = JSApi.getObjFunction(jsObjID, "OnBeginDrag");
        idOnCancel = JSApi.getObjFunction(jsObjID, "OnCancel");
        idOnDeselect = JSApi.getObjFunction(jsObjID, "OnDeselect");
        idOnDrag = JSApi.getObjFunction(jsObjID, "OnDrag");
        idOnDrop = JSApi.getObjFunction(jsObjID, "OnDrop");
        idOnEndDrag = JSApi.getObjFunction(jsObjID, "OnEndDrag");
        idOnInitializePotentialDrag = JSApi.getObjFunction(jsObjID, "OnInitializePotentialDrag");
        idOnMove = JSApi.getObjFunction(jsObjID, "OnMove");
        idOnPointerClick = JSApi.getObjFunction(jsObjID, "OnPointerClick");
        idOnPointerDown = JSApi.getObjFunction(jsObjID, "OnPointerDown");
        idOnPointerEnter = JSApi.getObjFunction(jsObjID, "OnPointerEnter");
        idOnPointerExit = JSApi.getObjFunction(jsObjID, "OnPointerExit");
        idOnPointerUp = JSApi.getObjFunction(jsObjID, "OnPointerUp");
        idOnScroll = JSApi.getObjFunction(jsObjID, "OnScroll");
        idOnSelect = JSApi.getObjFunction(jsObjID, "OnSelect");
        idOnSubmit = JSApi.getObjFunction(jsObjID, "OnSubmit");
        idOnUpdateSelected = JSApi.getObjFunction(jsObjID, "OnUpdateSelected");
    }
    void Update()
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idUpdate);
    }
    void LateUpdate()
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idLateUpdate);
    }
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnBeginDrag, eventData);
    }
    public virtual void OnCancel(BaseEventData eventData)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnCancel, eventData);
    }
    public virtual void OnDeselect(BaseEventData eventData)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnDeselect, eventData);
    }
    public virtual void OnDrag(PointerEventData eventData)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnDrag, eventData);
    }
    public virtual void OnDrop(PointerEventData eventData)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnDrop, eventData);
    }
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnEndDrag, eventData);
    }
    public virtual void OnInitializePotentialDrag(PointerEventData eventData)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnInitializePotentialDrag, eventData);
    }
    public virtual void OnMove(AxisEventData eventData)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnMove, eventData);
    }
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnPointerClick, eventData);
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnPointerDown, eventData);
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnPointerEnter, eventData);
    }
    public virtual void OnPointerExit(PointerEventData eventData)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnPointerExit, eventData);
    }
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnPointerUp, eventData);
    }
    public virtual void OnScroll(PointerEventData eventData)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnScroll, eventData);
    }
    public virtual void OnSelect(BaseEventData eventData)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnSelect, eventData);
    }
    public virtual void OnSubmit(BaseEventData eventData)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnSubmit, eventData);
    }
    public virtual void OnUpdateSelected(BaseEventData eventData)
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnUpdateSelected, eventData);
    }
}
