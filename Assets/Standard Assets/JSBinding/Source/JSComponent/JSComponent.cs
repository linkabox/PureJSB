using System;
using UnityEngine;

/// <summary>
///     JSComponent
///     A class redirect event functions (Awake, Start, Update, etc.) to JavaScript
///     Support serializations
/// </summary>
public class JSComponent : MonoBehaviour
{
    public string jsClassName = string.Empty;
    private bool _isInit;           //标记是否初始化完成,所有Mono事件对应的jsFuncId都设置完毕
    private bool _enableBeforeInit; //标记是否在初始化之前就触发过OnEnable事件
    private GameObject mGo;
    private int idAwake;
    private int idOnEnable;
    private int idStart;
    private int idOnDisable;
    private int idOnDestroy;
    private int idUpdateJsComState;
    [HideInInspector]
    [NonSerialized]
    protected int jsObjID;

    private int jsState;

    private bool jsSuccess
    {
        get { return jsState == 1; }
        set { if (value) jsState = 1; }
    }

    public bool jsFail
    {
        get { return jsState == 2; }
        set
        {
            if (value) jsState = 2;
            else jsState = 0;
        }
    }

    /// <summary>
    ///     Initializes the member function.
    /// </summary>
    protected virtual void initMemberFunction()
    {
        idAwake = JSApi.getObjFunction(jsObjID, "Awake");
        idStart = JSApi.getObjFunction(jsObjID, "Start");
        idOnEnable = JSApi.getObjFunction(jsObjID, "OnEnable");
        idOnDisable = JSApi.getObjFunction(jsObjID, "OnDisable");
        idOnDestroy = JSApi.getObjFunction(jsObjID, "OnDestroy");
        idUpdateJsComState = JSApi.getObjFunction(jsObjID, "$UpdateJsComState");
    }

    #region Monobehaviour Message

    void Awake()
    {
        Setup();
    }

    public void Setup()
    {
        if (mGo == null)
            mGo = gameObject;

        //通过AddComponent添加的JsCom需要设置好jsClassName,再调用Awake方法来进行初始化操作
        if (!string.IsNullOrEmpty(jsClassName))
        {
            init();
            callAwake();
        }
    }

    private void callAwake()
    {
        if (jsSuccess && !_isInit)
        {
            RegisterJsCom();
            UpdateJsComState();
            JSMgr.vCall.CallJSFunctionValue(jsObjID, idAwake);
            if (_enableBeforeInit)
            {
                JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnEnable);
                _enableBeforeInit = false;
            }
            _isInit = true;
        }
    }

    private void RegisterJsCom()
    {
        //Debug.LogError("RegisterJsCom: " + jsClassName);
        JSMgr.vCall.CallJSFunctionName(jsObjID, "$RegisterJsCom");
    }

    private void UnRegisterJcom()
    {
        //Debug.LogError("UnRegisterJscom: " + jsClassName);
        JSMgr.vCall.CallJSFunctionName(jsObjID, "$UnRegisterJscom");
    }

    private void UpdateJsComState()
    {
        if (jsSuccess)
        {
            JSMgr.vCall.CallJSFunctionValue(jsObjID, idUpdateJsComState, mGo.activeInHierarchy, isActiveAndEnabled);
        }
    }

    void OnEnable()
    {
        if (_isInit)
        {
            UpdateJsComState();
            JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnEnable);
        }
        else
        {
            _enableBeforeInit = true;
        }
    }

    private void Start()
    {
        JSMgr.vCall.CallJSFunctionValue(jsObjID, idStart);
    }

    void OnDisable()
    {
        if (_isInit)
        {
            UpdateJsComState();
            JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnDisable);
        }
    }

    private void OnDestroy()
    {
        if (!JSMgr.IsShutDown)
        {
            JSMgr.vCall.CallJSFunctionValue(jsObjID, idOnDestroy);
            UnRegisterJcom();
        }

        if (jsSuccess)
        {
            // remove this jsObjID even if JSMgr.isShutDown is true
            JSMgr.removeJSCSRel(jsObjID);
        }

        if (JSMgr.IsShutDown)
        {
            return;
        }

        if (jsSuccess)
        {
            // JSMgr.RemoveRootedObject(jsObj);
            JSApi.setTraceS(jsObjID, false);
            // JSMgr.removeJSCSRel(jsObjID); // Move upwards

            //
            // jsObj doesn't have finalize
            // we must remove it here
            // having a finalize is another approach
            //
            //JSApi.removeByID(jsObjID);
            //removeMemberFunction();
        }
    }

    #endregion

    /// <summary>
    ///     Removes if exist.
    /// </summary>
    /// <param name="id">The identifier.</param>
    //private void removeIfExist(int id)
    //{
    //    if (id != 0)
    //    {
    //        JSApi.removeByID(id);
    //    }
    //}

    //private void removeMemberFunction()
    //{
    //    // ATTENSION
    //    // same script have same idAwake idStart ... values
    //    // if these lines are executed in OnDestroy (for example  for gameObject A)
    //    // other gameObjects (for example B) with the same script
    //    // will also miss these functions
    //    // 
    //    // and if another C (with the same script) is born later   
    //    // it will re-get these values  but they are new values 
    //    // 
    //    // 
    //    // but if they are not removed in OnDestroy 
    //    // C valueMap may grow to a very big size
    //    //
    //    //         removeIfExist(idAwake);
    //    //         removeIfExist(idStart);
    //    //         removeIfExist(idFixedUpdate);
    //    //         removeIfExist(idUpdate);
    //    //         removeIfExist(idOnDestroy);
    //    //         removeIfExist(idOnGUI);
    //    //         removeIfExist(idOnEnable);
    //    //         removeIfExist(idOnTriggerEnter2D);
    //    //         removeIfExist(idOnTriggerStay);
    //    //         removeIfExist(idOnTriggerExit);
    //    //         removeIfExist(idOnAnimatorMove);
    //    //         removeIfExist(idOnAnimatorIK);
    //    //         removeIfExist(idDestroyChildGameObject);
    //    //         removeIfExist(idDisableChildGameObject);
    //    //         removeIfExist(idDestroyGameObject);
    //}

    //protected void callIfExist(int funID)
    //{
    //    if (funID > 0)
    //    {
    //        JSMgr.vCall.CallJSFunctionValue(jsObjID, funID);
    //    }
    //}

    //protected void callIfExist(int funID, params object[] args)
    //{
    //    if (funID > 0)
    //    {
    //        JSMgr.vCall.CallJSFunctionValue(jsObjID, funID, args);
    //    }
    //}

    public void initJS()
    {
        if (jsFail || jsSuccess) return;

        if (string.IsNullOrEmpty(jsClassName))
        {
            jsFail = true;
            return;
        }

        // ATTENSION
        // cannot use createJSClassObject here
        // because we have to call ctor, to run initialization code
        // this object will not have finalizeOp
        jsObjID = JSApi.newJSClassObject(jsClassName);
        JSApi.setTraceS(jsObjID, true);
        if (jsObjID == 0)
        {
            Debug.LogError("New MonoBehaviour \"" + jsClassName + "\" failed. Did you forget to export that class?");
            jsFail = true;
            return;
        }
        JSMgr.addJSCSRel(jsObjID, this);
        initMemberFunction();
        jsSuccess = true;
    }

    //
    // 有几个事情要做
    // A) initJS()
    // B) initSerializedData(jsObjID)
    // C) callIfExist(idAwake);
    //
    // 不同时候要做的事情，假设有2个类 X 和 Y
    // 1) 假设 X 类不被其他类所引用，则 X 类 Awake 时：A + B + C
    // 2) 在 X 类 initSerializedData 时发现引用了 Y 类，而 Y 类的 Awake 还没有被调用，那么会马上调用 Y 类的 A（看 GetJSObjID 函数），之后 Y 类的 Awake 里：B + C
    //    看 JSSerializer.GetGameObjectMonoBehaviourJSObj 函数
    //    为什么第1步只调用Y的A，而不调B？因为那时候X类正在处理序列化，不想中间又穿插Y的序列化处理，也用不到
    // 3) 在 AddComponent<X>() 时，我们知道他会调用 Awake()，但是此时由于 jsClassName 未被设置，所以会 jsFail=true，但紧接着我们又设置 jsFail=false，然后调用 init(true) 和 callAwake()，做的事情也是 A + B + C
    //    看 Components.cs 里的 GameObject_AddComponentT1 函数
    // 4) 在 GetComponent<X>() 时，如果 X 的 Awake() 还未调用，我们会调用 X 的 init(true)，他做了 A + B，之后 X 的 Awake() 再做 C
    //    看 Components.cs 里的 help_searchAndRetCom 和 help_searchAndRetComs 函数
    //
    //
    // 总结：以上那么多分类，做的事情其实就是，当一个类X要在Awake时去获取Y类组件，甚至访问Y类成员，如果此时Y类的Awake还没有调用，此时会得到undefined，那么我们只好先初始化一下Y类的JS对象。
    // 
    private void init()
    {
        if (!JSEngine.initSuccess && !JSEngine.initFail)
        {
            JSEngine.FirstInit();
        }
        if (!JSEngine.initSuccess)
        {
            return;
        }

        initJS();
    }

    /// <summary>
    ///     get javascript object id of this JSComponent.
    ///     jsObjID may == 0 when this function is called, because other scripts refer to this JSComponent.
    ///     in this case, we call initJS() for this JSComponent immediately.
    /// </summary>
    /// <returns></returns>
    public int GetJSObjID()
    {
        if (jsObjID == 0)
        {
            init();
        }
        return jsObjID;
    }
}