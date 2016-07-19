using SharpKit.JavaScript;
using UnityEngine;
using System.Collections;


public class PerformanceTest1 : MonoBehaviour {

    Transform mTransform;
	void Start () {
        mTransform = transform;
        TestActioCallback();
    }
    void Test0()
    {
        var N = 10000000;

        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        var g = 0;
        var f = 0;
        for (var i = 0; i < N; i++)
        {
            g += 1;
            f += 1;
        }
        sw.Stop();
//         Debug.Log("loop count: " + N.ToString());
//         Debug.Log("calc result: " + (f + g).ToString());
        Debug.Log("test0 time: " + sw.ElapsedMilliseconds + " ms");
    }

    private void Test1()
    {
        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        Vector3 m;
        for (int i = 0; i < 2000; i++)
        {
            m = mTransform.position;

            mTransform.position = m;
        }
        Debug.Log("test1 time: " + sw.ElapsedMilliseconds + " ms");
    }

    private void Test2()
    {
        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        Vector3 m = mTransform.position;
        for (int i = 0; i < 2000; i++)
        {
            m = Vector3.Normalize(m);
        }
        Debug.Log("test2 time: " + sw.ElapsedMilliseconds + " ms");
    }

    private void Test3()
    {
        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        Vector3 m = mTransform.position;
        for (int i = 0; i < 2000; i++)
        {
            m.Normalize();
        }
        Debug.Log("test3 time: " + sw.ElapsedMilliseconds + " ms");
    }

    private void Test4()
    {
        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        Vector3 m = mTransform.position;
        for (int i = 0; i < 2000; i++)
        {
            mTransform.position = m;
        }
        Debug.Log("test4 time: " + sw.ElapsedMilliseconds + " ms");
    }

    private void Test5()
    {
        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        for (int i = 0; i < 2000; i++)
        {
            new Vector3(i, i, i);
        }
        Debug.Log("test5 time: " + sw.ElapsedMilliseconds + " ms");
    }

    void Test6()
    {
        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        for (var i = 0; i < 50000; i++)
        {
            var go = new GameObject("init");
            GameObject.DestroyImmediate(go);
        }
        
        Debug.Log("test6 time: " + sw.ElapsedMilliseconds + " ms");
    }

    
    public static APIExportTest.RefObject Run(APIExportTest.RefObject refObject)
    {
        APIExportTest.StaticObject.x += refObject.x;
        APIExportTest.StaticObject.y += refObject.y;
        return APIExportTest.StaticObject;
    }

    public void Test7()
    {
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        var obj = APIExportTest.StaticObject;
        for (int i = 0; i < 50000; i++)
        {
            obj = Run(obj);
        }
        sw.Stop();

        Debug.Log("test7 time: " + sw.ElapsedMilliseconds + " ms");
    }

    void TestActioCallback()
    {
        APIExportTest obj = new APIExportTest();

        //Delegate Test
        APIExportTest.OnStaticDelegateFinish = () =>
        {
            Debug.LogError("PerTest OnStaticUpdate");
        };
        APIExportTest.OnStaticDelegateFinish();

        obj.OnDelegateFinish += OnPerTestDelegateFinish;
        obj.OnDelegateFinish();
        obj.OnDelegateFinish -= OnPerTestDelegateFinish;
        if(obj.OnDelegateFinish == null)
            Debug.LogError("====Remove OnPerTestDelegateFinish====");

        System.Action delegateAction = () =>
        {
            Debug.LogError("PerTest AddDelegate");
        };
        obj.AddDelegate(delegateAction);
        obj.OnDelegateFinish();
        obj.RemoveDelegate(delegateAction);
        if (obj.OnDelegateFinish == null)
            Debug.LogError("====Remove AddDelegate====");

        //Static Event Test
        APIExportTest.OnStaticEventFinish += OnPerTestStaticEventFinish;
        APIExportTest.SendStaticEvent();
        Debug.LogError("====Remove OnPerTestStaticEventFinish====");
        APIExportTest.OnStaticEventFinish -= OnPerTestStaticEventFinish;
        APIExportTest.SendStaticEvent();

        //Instance Event Test
        Debug.LogError("====Test AddEvent====");
        System.Action<MonoBehaviour> eventAction = (mono) =>
        {
            Debug.LogError("PerTest AddEvent");
        };
        obj.AddEvent(eventAction);
        obj.SendEvent();
        Debug.LogError("====Remove AddEvent====");
        obj.RemoveEvent(eventAction);
        obj.SendEvent();

        Debug.LogError("====Test OnPerTestEventFinish====");
        obj.OnEventFinish += OnPerTestEventFinish;
        obj.SendEvent();
        Debug.LogError("====Remove OnPerTestEventFinish====");
        obj.OnEventFinish -= OnPerTestEventFinish;
        obj.SendEvent();
    }

    public void OnPerTestDelegateFinish()
    {
        Debug.LogError("PerTest OnPerTestDelegateFinish");
    }

    public void OnPerTestEventFinish(MonoBehaviour mono)
    {
        Debug.LogError("PerTest OnPerTestEventFinish");
    }

    public void OnPerTestStaticEventFinish()
    {
        Debug.LogError("PerTest OnPerTestStaticEventFinish");
    }

    public void OnChangeEvent()
    {

    }

    float elapsed = 0;
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > 3f)
        {
            elapsed = 0f;
            Test0();
            Test1();
            Test2();
            Test3();
            Test4();
            Test5();
            Test6();
        }
	}
}
