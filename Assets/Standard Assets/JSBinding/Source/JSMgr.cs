using UnityEngine;
using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

#pragma warning disable 414
public class MonoPInvokeCallbackAttribute : System.Attribute
{
    private Type type;
    public MonoPInvokeCallbackAttribute(Type t) { type = t; }
}
#pragma warning restore 414

public static class JSMgr
{
    [MonoPInvokeCallbackAttribute(typeof(JSApi.JSErrorReporter))]
    static int errorReporter(IntPtr cx, string message, IntPtr report)
    {
        string fileName = JSApi.getErroReportFileNameS(report);
        int lineno = JSApi.getErroReportLineNo(report);
        string str = fileName + "(" + lineno.ToString() + "): " + message;
        Debug.LogError(str);
        return 1;
    }

    // load generated js files
    public delegate void OnInitJSEngine(bool bSuccess);
    public static OnInitJSEngine onInitJSEngine;

    static bool RefCallStaticMethod(string className, string methodName)
    {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        bool find = false;
        MethodInfo method = null;
        for (int i = 0; i < assemblies.Length; i++)
        {
            Type[] types = assemblies[i].GetExportedTypes();

            for (int j = 0; j < types.Length; j++)
            {
                if (types[j].FullName == className)
                {
                    method = types[j].GetMethod(methodName);

                    if (method != null)
                    {
                        find = true;
                        break;
                    }
                }
            }
        }
        if (find)
        {
            method.Invoke(null, null);
            return true;
        }
        else
        {
            return false;
        }
    }
    static object RefGetStaticField(string className, string fieldName)
    {
        Type t = Type.GetType(className);
        if (t == null)
            return null;
        FieldInfo field = t.GetField(fieldName);
        if (field == null)
            return null;
        return field.GetValue(null);
    }

    /// <summary>
    /// The js engine round
    /// jSEngineRound++ whenever ShutDownJSEngine
    /// start from 1
    /// </summary>
    public static int jsEngineRound = 1;
    static int startValueMapID = 0;
    static bool IsJSIDOld(int id)
    {
        return id < startValueMapID;
    }
    static bool InitJSEngine_ing = false;
    public static bool InitJSEngine(OnInitJSEngine onInitJSEngine)
    {
        if (InitJSEngine_ing)
        {
            Debug.LogError("FATAL ERROR: Trying to InitJSEngine twice");
            return false;
        }

        InitJSEngine_ing = true;
        shutDown = false;

        int initResult = JSApi.InitJSEngine(
            new JSApi.JSErrorReporter(errorReporter),
            new JSApi.CSEntry(JSMgr.CSEntry),
            new JSApi.JSNative(require),
            new JSApi.OnObjCollected(onObjCollected),
            new JSApi.JSNative(print));

        startValueMapID = JSApi.getValueMapStartIndex();
        Debug.Log("startValueMapID " + startValueMapID);

        if (initResult != 0)
        {
            Debug.LogError("InitJSEngine fail. error = " + initResult);
            onInitJSEngine(false);
            InitJSEngine_ing = false;
            return false;
        }

        CSGenerateRegister.RegisterAll();
        onInitJSEngine(true);

        //if (!RefCallStaticMethod("CSGenerateRegister", "RegisterAll"))
        //{
        //    Debug.LogError("Call CSGenerateRegister.RegisterAll() failed. Did you forget to click menu [Assets | JSB | Generate JS and CS Bindings]?");
        //    onInitJSEngine(false);
        //    ret = false;
        //}
        //else
        //{
        //    onInitJSEngine(true);
        //    ret = true;
        //}

        JSCache.InitJsTypeConfig();
        InitJSEngine_ing = false;
        return true;
    }

    public static bool shutDown = false;
    public static bool IsShutDown { get { return shutDown; } }

    public static void ShutdownJSEngine()
    {
        shutDown = true;

        /* 
         * 在 Shutdown 时，先主动删除 CSRepresentedObject
         * 现在已经没有必要再维护 id -> CSR.. 的关系了
         * 移除后，CSR.. 有可能还被其他地方引用，所以在这个函数中调用C# GC，也不一定会调用到 CSR.. 的析构
         * 后来在某个时刻析构被调用，在析构函数里有  removeJSCSRel ，那里会判断 round 是上一轮的所以忽略
         */
        List<int> keysToRemove = new List<int>();
        List<object> csObjsToRemove = new List<object>();
        foreach (var KV in mDictionary1)
        {
            JS_CS_Rel rel = KV.Value;
            var wr = rel.csObj as WeakReference;
            if (wr != null)
            {
                if (wr.Target is CSRepresentedObject)
                {
                    keysToRemove.Add(KV.Key);
                    csObjsToRemove.Add(rel.csObj);
                }
            }
        }
        foreach (int jsObjId in keysToRemove)
        {
            mDictionary1.Remove(jsObjId);
        }
        foreach (var csObj in csObjsToRemove)
        {
            mDictionary2.Remove(csObj);
        }

        //
        // 并GC
        //
        System.GC.Collect();

        int Count = mDictionary1.Count;


        // There is a JS_GC called inside JSApi.ShutdownJSEngine
#if UNITY_EDITOR
        // DO NOT really cleanup everything, because we wanna start again
        JSApi.ShutdownJSEngine(0);
#else
        JSApi.ShutdownJSEngine(1);
#endif

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("After JSApi.ShutdownJSEngine: ");
        sb.Append("mDictionary1 count " + Count + " -> " + mDictionary1.Count + ", left elements(should only contain JSComponent):\n");
        /*
         * 到这里 mDictionary1 和 mDictionary2 应该只剩余 JSComponent 及其子类，原因是：
         * 除了 JSComponent 外，其他东西都应该在 JSApi.ShutdownJSEngine(0) 后被移除（他里面调用了 JS_GC)
         * 而 JSComponent 是没有垃圾回收回调的，他是在 OnDestroy 时从这2个字典里移除的
         * 这时候可能他的 OnDestroy 还没有执行，所以这2个字典里还会有他们
         */
        List<int> Keys = new List<int>(mDictionary1.Keys);
        foreach (int jsObjId in Keys)
        {
            if (!mDictionary1.ContainsKey(jsObjId))
                continue;

            JS_CS_Rel Rel = mDictionary1[jsObjId];
            sb.AppendLine(jsObjId + " " + Rel.csObj.GetType().Name);
        }
        Debug.Log(sb);

        allCallbackInfo.Clear();
        MoveJSCSRel2Old();
        mDictJSFun1.Clear();
        evaluatedScript.Clear();
        CSRepresentedObject.s_objCount = 0;
        CSRepresentedObject.s_funCount = 0;
        jsEngineRound++;
    }

    static readonly Dictionary<string, bool> evaluatedScript = new Dictionary<string, bool>();

    /// <summary>
    /// callback function list
    /// </summary>
    /// 
    public delegate void CSCallbackField(JSVCall vc);
    public delegate void CSCallbackProperty(JSVCall vc);
    public delegate bool CSCallbackMethod(JSVCall vc, int argc);

    public class MethodCallBackInfo
    {
        public MethodCallBackInfo(CSCallbackMethod f, string n) { fun = f; methodName = n; }
        public CSCallbackMethod fun;
        public string methodName; // this is originally used to distinguish overloaded methods
    }

    // usage
    // 1 use for calling cs from js, by directly-call
    public class CallbackInfo
    {
        public Type type;
        public CSCallbackField[] fields;
        public CSCallbackProperty[] properties;

        public MethodCallBackInfo[] constructors;
        public MethodCallBackInfo[] methods;
    }
    public static List<CallbackInfo> allCallbackInfo = new List<CallbackInfo>();

    // used to judge it's overloaded function or not
    // used in JSGenerator

    public static JSVCall vCall = new JSVCall();
    public static JSDataExchangeMgr datax = new JSDataExchangeMgr();


    /// <summary>
    /// CSEntry: entry for javascript CS.Call
    /// </summary>
    /// <param name="iOP">The i op.</param>
    /// <param name="slot">The slot.</param>
    /// <param name="index">The index.</param>
    /// <param name="isStatic">The is static.</param>
    /// <param name="argc">The argc.</param>
    /// <returns></returns>
    [MonoPInvokeCallbackAttribute(typeof(JSApi.CSEntry))]
    static int CSEntry(int iOP, int slot, int index, int isStatic, int argc)
    {
        if (JSMgr.IsShutDown) return 0;
        try
        {
            vCall.CallCallback(iOP, slot, index, isStatic, argc);
        }
        catch (System.Exception ex)
        {
            /* 
             * if exception occurs, catch it, pass the error to js, and return false
             * js then print the error string and js call stack
             * note: the error contains cs call stack, so now we have both cs and js call stack
             */
            //JSApi.JSh_ReportError(cx, ex.ToString());
            JSApi.reportError(ex.ToString());
            return 0;
        }

        return 1;
    }

    public static bool evaluate(string jsScriptName)
    {
        if (evaluatedScript.ContainsKey(jsScriptName))
        {
            return true;
        }
        // add even failed
        evaluatedScript.Add(jsScriptName, true);

        var bytes = JSFileLoader.LoadJSSync(jsScriptName);

        if (bytes == null)
        {
            Debug.LogError(jsScriptName + "file bytes is null");
            return false;
        }
        else if (bytes.Length == 0)
        {
            Debug.LogError(jsScriptName + "file bytes length = 0");
            return false;
        }

        bool ret;
        if (JSEngine.inst.UseJSC)
            ret = 1 == JSApi.evaluate_jsc(bytes, (uint)bytes.Length, jsScriptName);
        else
            ret = 1 == JSApi.evaluate(bytes, (uint)bytes.Length, jsScriptName);

        return ret;
    }

    /// <summary>
    /// execute a JavaScript script
    /// can only require a script once.
    /// </summary>
    /// <param name="cx">The cx.</param>
    /// <param name="argc">The argc.</param>
    /// <param name="vp">The vp.</param>
    /// <returns></returns>
    [MonoPInvokeCallbackAttribute(typeof(JSApi.JSNative))]
    static bool require(IntPtr cx, uint argc, IntPtr vp)
    {
        string jsScriptName = JSApi.getArgStringS(vp, 0);
        bool ret = evaluate(jsScriptName);
        JSApi.setRvalBoolS(vp, ret);
        return true;
    }

    [MonoPInvokeCallbackAttribute(typeof(JSApi.JSNative))]
    static bool print(IntPtr cx, uint argc, IntPtr vp)
    {
        string str = JSApi.getArgStringS(vp, 0);
        UnityEngine.Debug.Log(str);
        return true;
    }

    #region JS_CS_REL

    public class JS_CS_Rel
    {
        public int jsObjID;
        public object csObj;
        public JS_CS_Rel(int jsObjID, object csObj)
        {
            this.jsObjID = jsObjID;
            this.csObj = csObj;
        }
    }
    public static void addJSCSRel(int jsObjID, object csObj, bool weakReference = false)
    {
        if (weakReference)
        {
            WeakReference wrObj = new WeakReference(csObj);
            var Rel = new JS_CS_Rel(jsObjID, wrObj);
            AddDictionary1(jsObjID,Rel);
            AddDictionary2(Rel);
        }
        else
        {
            JSCache.TypeInfo typeInfo = JSCache.GetTypeInfo(csObj.GetType());

            if (mDictionary1.ContainsKey(jsObjID))
            {
                if (typeInfo.IsValueType)
                {
                    mDictionary1.Remove(jsObjID);
                }
            }

            var Rel = new JS_CS_Rel(jsObjID, csObj);
            AddDictionary1(jsObjID, Rel);

            if (typeInfo.IsClass)
            {
                AddDictionary2(Rel);
            }
        }
    }

    private static void AddDictionary1(int jsObjID, JS_CS_Rel Rel)
    {
        if (mDictionary1.ContainsKey(jsObjID))
        {
            mDictionary1[jsObjID] = Rel;
            Debug.LogError("mDictionary1 has same key to add:" + jsObjID + " - " + Rel.csObj);
        }
        else
        {
            mDictionary1.Add(jsObjID, Rel);
        }
    }

    private static void AddDictionary2(JS_CS_Rel Rel)
    {
        if (mDictionary2.ContainsKey(Rel.csObj))
        {
            mDictionary2[Rel.csObj] = Rel;
            Debug.LogError("mDictionary2 has same key to add:" + Rel.csObj + " - " + Rel.jsObjID);
        }
        else
        {
            mDictionary2.Add(Rel.csObj, Rel);
        }
    }

    // round 用于标记 CSRepresentedObj 是属于上一轮的还是这一轮的
    // id 可以用于判定JS对象是属于上一轮的还是这一轮的
    public static void removeJSCSRel(int id, int round = 0)
    {
        // don't remove an ID belonging to previous round
        if (round == 0 || round == JSMgr.jsEngineRound)
        {
            JS_CS_Rel Rel;

            if (IsJSIDOld(id))
            {   // 这个分支不分进入！
                // 因为上一轮的理应在 ShutdownJSEngine 之后全部被回收
                // 但这些代码留着也无防
                if (mDictionary1_Old.TryGetValue(id, out Rel))
                {
                    mDictionary1_Old.Remove(id);
                    mDictionary2_Old.Remove(Rel.csObj);

                    Debug.Log("Remove " + id + " from old, left " + mDictionary1_Old.Count + " and " + mDictionary2_Old.Count);
                }
                else
                {
                    Debug.LogError("JSMgr.removeJSCSRel (OLD): " + id + " not found.");
                }
            }
            else
            {
                if (mDictionary1.TryGetValue(id, out Rel))
                {
                    mDictionary1.Remove(id);
                    mDictionary2.Remove(Rel.csObj);
                }
                else if (!JSMgr.IsShutDown)
                {
                    Debug.LogError("JSMgr.removeJSCSRel: " + id + " not found.");
                }
            }
        }
        else if (round > 0)
        {
            //Debug.Log(new StringBuilder().AppendFormat("didn't remove id {0} because it belongs to old round {1}", id, round));
        }
    }

    public static object getCSObj(int jsObjID)
    {
        JS_CS_Rel obj;
        if (mDictionary1.TryGetValue(jsObjID, out obj))
        {
            object ret = obj.csObj;
            if (ret is WeakReference)
            {
                object tar = ((WeakReference)ret).Target;
                if (tar == null)
                {
                    //                    JSEngine.inst.UpdateThreadSafeActions();
                    //                    if (mDictionary1.ContainsKey(jsObjID))
                    //                        Debug.LogError("ERROR: JSMgr.getCSObj WeakReference.Target == null");

                    // 这里为什么这么做
                    // 这里先移除，返回值为null，外面自然会再添加
                    // 更多细节请看 CSRepresentedObject 注释！
                    // 这里唯一需要纠结一下的就是 round 参数，总是传 0 吧，现在已经不需要检查 round 是否是上一轮的
                    // 而且在手机上跑的话也不可能出现遗留上一轮对象的问题，因为一共只有一轮
                    JSMgr.removeJSCSRel(jsObjID, 0 /* round TODO */);
                    JSMgr.removeJSFunCSDelegateRel(jsObjID);
                }
                return tar;
            }
            return ret;
        }
        return null;
    }
    public static int getJSObj(object csObj, JSCache.TypeInfo typeInfo)
    {
        if (typeInfo.IsValueType)
        {
            return 0;
        }

        JS_CS_Rel Rel;
        var wr = csObj as WeakReference;
        object newObj = (wr != null) ? wr.Target : csObj;
        if (mDictionary2.TryGetValue(csObj, out Rel))
        {
#if UNITY_EDITOR
            wr = Rel.csObj as WeakReference;
            object oldObj = (wr != null) ? wr.Target : Rel.csObj;
            if (!oldObj.Equals(newObj))
            {
                Debug.LogError("mDictionary2 and mDictionary1 saves different object");
            }
#endif
            return Rel.jsObjID;
        }
        return 0;
    }
    public static void changeJSObj(int jsObjID, object csObjNew)
    {
        if (!csObjNew.GetType().IsValueType)
        {
            Debug.LogError("class can not call changeJSObj2");
            return;
        }
        JS_CS_Rel Rel;
        if (mDictionary1.TryGetValue(jsObjID, out Rel))
        {
            mDictionary1[jsObjID] = new JS_CS_Rel(jsObjID, csObjNew);
        }
    }
    public static void MoveJSCSRel2Old()
    {
        mDictionary1_Old = mDictionary1;
        mDictionary2_Old = mDictionary2;

        mDictionary1 = new Dictionary<int, JS_CS_Rel>(); // key = jsObjID
        mDictionary2 = new Dictionary<object, JS_CS_Rel>(); // key = csObj
    }

    [MonoPInvokeCallbackAttribute(typeof(JSApi.OnObjCollected))]
    static void onObjCollected(int id)
    {
        removeJSCSRel(id);
    }

    static Dictionary<int, JS_CS_Rel> mDictionary1 = new Dictionary<int, JS_CS_Rel>(); // 以jsObjID为key,获取对应csObj
    static Dictionary<int, JS_CS_Rel> mDictionary1_Old;
    /// <summary>
    /// NOTICE
    /// two C# object may have same hash code?
    /// if Destroy(go) was called, obj becomes null, ... 
    /// </summary>
    static Dictionary<object, JS_CS_Rel> mDictionary2 = new Dictionary<object, JS_CS_Rel>(); // 以csObj为key,获取对应jsObjID
    static Dictionary<object, JS_CS_Rel> mDictionary2_Old;

    public static void GetDictCount(out int countDict1, out int countDict2)
    {
        countDict1 = mDictionary1.Count;
        countDict2 = mDictionary2.Count;
    }
    public static Dictionary<int, JS_CS_Rel> GetDict1() { return mDictionary1; }

    #endregion

    #region JS<->CS fun<->Delegate relationship

    // key = jsFuncId, Value = JS_CS_FunRel(Delegate, Delegate.GetHashCode())
    static Dictionary<int, WeakReference> mDictJSFun1 = new Dictionary<int, WeakReference>();

    public static T getJSFunCSDelegateRel<T>(int funID)
    {
        WeakReference wr = null;
        if (mDictJSFun1.TryGetValue(funID, out wr))
        {
            object obj = wr.Target;
            if(obj == null)
                Debug.LogError("ERROR getJSFunCSDelegateRel rel.wr.Target == null");

            return (T) obj;
        }
        return default(T);
    }
    public static void addJSFunCSDelegateRel(int funID, Delegate del)
    {
        if (!mDictJSFun1.ContainsKey(funID))
        {
            var wr = new WeakReference(del);
            mDictJSFun1.Add(funID, wr);
        }
    }
    public static void removeJSFunCSDelegateRel(int funID)
    {
        mDictJSFun1.Remove(funID);
    }
    public static int getFunIDByDelegate(Delegate del)
    {
        foreach (var pair in mDictJSFun1)
        {
            Delegate target = (Delegate)pair.Value.Target;
            if (target != null && target == del)
                return pair.Key;
        }
        return 0;
    }
    public static string getJSFunCSDelegateCount()
    {
        return mDictJSFun1.Count.ToString();
    }
    #endregion
}
