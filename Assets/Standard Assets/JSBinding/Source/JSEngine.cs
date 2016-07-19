using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Debug = UnityEngine.Debug;
using jsval = JSApi.jsval;
/// <summary>
/// JSEngine
/// Represents a JavaScript Engine object
/// In order to run JavaScript, there must be one and only one JSEngine object in the scene
/// 
/// JSEngine must have a lower execution order than JSComponent.
/// You can set script execution order by click menu Edit | Project Settings | Script Execution Order
/// for example, set JSEngine to 400, set JSComponent to 500
/// </summary>
public class JSEngine : MonoBehaviour
{
    public static JSEngine inst;
    public static int initState = 0;
    public static bool initSuccess { get { return initState == 1; } set { if (value) initState = 1; } }
    public static bool initFail { get { return initState == 2; } set { if (value) initState = 2; else initState = 0; } }

    /*
     * Debug settings, if port is not available, try another one
     */
	public bool UseJSC = false;
    public bool debug = true;
    public int port = 5086;
    bool mDebug = true;

    /*
     * Garbage Collection setting
     * if GCInterval < 0, will not call GC (default value, SpiderMonkey will automatically call GC)
     * if GCInterval >= 0, will call GC every GCInterval seconds
     */
    public float GCInterval = -1f;

    /*
     * 
     */
    public string[] InitLoadScripts = new string[0];

    public void OnInitJSEngine(bool bSuccess)
    {
        /* 
         * Debugging is only available in desktop platform
         * */
        mDebug = debug && UseJSC;
        if (bSuccess)
        {
            if (InitLoadScripts != null)
            {
                for (var i = 0; i < InitLoadScripts.Length; i++)
                {
                    JSMgr.evaluate(InitLoadScripts[i]);
                }
            }

            if (JSApi.initErrorHandler() == 1)
                Debug.Log("JS: print error stack: YES");
            else
                Debug.Log("JS: print error stack: NO");

            initSuccess = true;
            Debug.Log("JS: Init JSEngine OK");
            if (mDebug)
            {
                Debug.Log("JS: Enable Debugger");
                JSApi.enableDebugger(new string[] { JSPathSettings.jsDir }, 1, port);
            }
            //获取Manual/UnityEngine_MonoBehaviour下定义的jsb_UpdateJsCom全局方法
            idUpdateEvent = JSApi.getObjFunction(0, "jsb_UpdateJsCom");
            if (idUpdateEvent <= 0)
            {
                Debug.LogError("Can not find JsFunction[jsb_UpdateJsCom]");
            }
            idLateUpdateEvent = JSApi.getObjFunction(0, "jsb_LateUpdateJsCom");
            if (idLateUpdateEvent <= 0)
            {
                Debug.LogError("Can not find JsFunction[jsb_LateUpdateJsCom]");
            }
            idFixedUpdateEvent = JSApi.getObjFunction(0, "jsb_FixedUpdateJsCom");
            if (idFixedUpdateEvent <= 0)
            {
                Debug.LogError("Can not find JsFunction[jsb_FixedUpdateJsCom]");
            }
        }
        else
        {
            initFail = true;
            Debug.Log("JS: Init JSEngine FAIL");
        }
    }

    // FirstInit may be called from JSComponent!
    public static void FirstInit(JSEngine jse = null)
    {
        if (!initSuccess && !initFail)
        {
            if (jse == null)
            {
                GameObject jseGO = GameObject.Find("JSEngine");
                if (jseGO == null)
                {
                    initFail = true;
                    Debug.LogError("JSEngine gameObject not found.");
                }
                else
                {
                    jse = jseGO.GetComponent<JSEngine>();
                }
            }

            if (jse != null)
            {
                /*
                * Don't destroy this GameObject on load
                */
                DontDestroyOnLoad(jse.gameObject);
                inst = jse;
#if UNITY_EDITOR
                //编辑器模式下默认输出JSEngine统计数据
                inst.showStatistics = true;
#endif

                Stopwatch stopwatch = Stopwatch.StartNew();
                JSMgr.InitJSEngine(jse.OnInitJSEngine);
                stopwatch.Stop();
                Debug.Log("==============InitJSEngine: " + stopwatch.ElapsedMilliseconds + " ms");
            }
        }
    }

    void Awake()
    {
        if (JSEngine.inst != null && JSEngine.inst != this)
        {
            // destroy self if there is already a JSEngine gameObject
            Destroy(gameObject);
            return;
        }

		Debug.Log (UseJSC ? "JS: Use JSC" : "JS: Not Use JSC");
        JSEngine.FirstInit(this);
    }

    private int jsCallCSPerFrame = 0;
#if UNITY_EDITOR
    private string jsCallLogInfo = "";
#endif
    void Update()
    {
        if (this != JSEngine.inst)
            return;

        jsCallCSPerFrame = JSMgr.vCall.jsCallCount;
        JSMgr.vCall.jsCallCount = 0;
#if UNITY_EDITOR
        if (JSMgr.vCall.jsCallInfoSb.Length > 0)
        {
            jsCallLogInfo = JSMgr.vCall.jsCallInfoSb.ToString();
            JSMgr.vCall.jsCallInfoSb.Length = 0;
        }
#endif

        UpdateThreadSafeActions();

        if (initSuccess)
        {
            _UpdateEvent();
            if (mDebug)
                JSApi.updateDebugger();
        }
    }

    #region JsCom UpdateEvent
    private int idUpdateEvent;
    private int idLateUpdateEvent;
    private int idFixedUpdateEvent;
    private void _UpdateEvent()
    {
        if (idUpdateEvent > 0)
        {
            JSMgr.vCall.CallJSFunctionValue(0, idUpdateEvent);
        }
    }

    private void _LateUpdateEvent()
    {
        if (idLateUpdateEvent > 0)
        {
            JSMgr.vCall.CallJSFunctionValue(0, idLateUpdateEvent);
        }
    }

    private void _FixedUpdateEvent()
    {
        if (idFixedUpdateEvent > 0)
        {
            JSMgr.vCall.CallJSFunctionValue(0, idFixedUpdateEvent);
        }
    }

    public void DumpJsComMapInfo()
    {
        if (!Application.isPlaying)
            return;

        if (initSuccess)
        {
            JSMgr.vCall.CallJSFunctionName(0, "jsb_DumpJsComMapInfo");
        }
    }
    #endregion

    List<Action> lstThreadSafeActions = new List<Action>();
    bool hasThreadSafeActions = false;
    object @lock = new object();

    public void DoThreadSafeAction(Action action)
    {
        lock (@lock)
        {
            hasThreadSafeActions = true;
            lstThreadSafeActions.Add(action);
        }
    }
    private void UpdateThreadSafeActions()
    {
        if (hasThreadSafeActions)
        {
            lock (@lock)
            {
                for (int i = 0; i < lstThreadSafeActions.Count; i++)
                {
                    Action action = lstThreadSafeActions[i];
                    if(action != null)
                        action();
                }
                lstThreadSafeActions.Clear();
                hasThreadSafeActions = false;
            }
        }
    }

    float accum = 0f;
    void LateUpdate()
    {
        if (this != JSEngine.inst)
            return;

        if (initSuccess)
        {
            _LateUpdateEvent();
            if (GCInterval >= 0f)
            {
                accum += Time.deltaTime;
                if (accum > GCInterval)
                {
                    accum = 0f;
                    //Debug.Log("_GC_Begin");
                    JSApi.gc();
                    //Debug.Log("_GC_End");
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (this != JSEngine.inst)
            return;

        if (initSuccess)
        {
            _FixedUpdateEvent();
        }
    }

    void OnDestroy()
    {
        if (this == JSEngine.inst)
        {
            if (mDebug && initSuccess)
            {
                JSApi.cleanupDebugger();
            }
            JSMgr.ShutdownJSEngine();
            JSEngine.inst = null;
            JSEngine.initState = 0;
            Debug.Log("JS: JSEngine Destroy");
        }
    }

	public bool showStatistics = true;
    public Vector2 scrollPos = Vector2.zero;
#if !UNITY_EDITOR
    /// <summary>
    /// OnGUI: Output some statistics
    /// </summary>
    void OnGUI()
    {
        if (this != inst)
            return;
        if (!showStatistics)
            return;

        scrollPos = DrawStatistics(scrollPos, 280f);
    }
#endif

    public Vector2 DrawStatistics(Vector2 sliderPos,float scrollViewHeight)
    {
        int countDict1, countDict2;

        JSMgr.GetDictCount(out countDict1, out countDict2);
        GUILayout.TextArea(
            "JS->CS Count: " + jsCallCSPerFrame + " Round: " + JSMgr.jsEngineRound + " Objs(Total " + countDict1 +
            ", Class " + countDict2 + ") CSR(Obj " + CSRepresentedObject.s_objCount + " Fun " +
            CSRepresentedObject.s_funCount + ") Del " + JSMgr.getJSFunCSDelegateCount());
#if UNITY_EDITOR
        GUILayout.TextArea(jsCallLogInfo);
#endif

        int clsCount = 0;
        var dict1 = JSMgr.GetDict1();
        var Keys = new List<int>(dict1.Keys);

        var class2Count = new Dictionary<string, int>();
        foreach (int K in Keys)
        {
            if (!dict1.ContainsKey(K))
                continue;

            var Rel = dict1[K];
            var typeName = Rel.csObj.GetType().Name;
            if (class2Count.ContainsKey(typeName))
            {
                class2Count[typeName]++;
            }
            else
            {
                class2Count[typeName] = 1;
            }
            if (Rel.csObj != null && Rel.csObj.GetType().IsClass)
            {
                clsCount++;
            }
        }
        float y = 40;
        sliderPos = GUILayout.BeginScrollView(sliderPos, GUILayout.Height(scrollViewHeight));
        GUILayout.TextArea("class count: " + clsCount);
        y += 20;
        GUILayout.TextArea("valueMapSize: " + JSApi.getValueMapSize());
        y += 20;
        GUILayout.TextArea("valueMapIndex: " + JSApi.getValueMapIndex());
        y += 20;
        foreach (var v in class2Count)
        {
            GUILayout.TextArea(v.Key + ": " + v.Value);
            y += 20;
        }
        GUILayout.EndScrollView();

        return sliderPos;
    }
}