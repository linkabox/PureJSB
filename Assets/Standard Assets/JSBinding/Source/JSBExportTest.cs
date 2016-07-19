using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class TestExtensionMethod
{
    [CsExportedMethod(JsCode = @"
        GetOrAddComponent$1: function(T, go) { 
            var t = go.GetComponent$1(T);
            if (t == null){
                t = go.AddComponent$1(T);
            }
            return t; //Ret: T
        },")]
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        T t = go.GetComponent<T>();
        if (t == null)
        {
            t = go.AddComponent<T>();
        }
        return t;
    }
}

public class APIExportTest
{
    public List<int> IDs = new List<int>();
    public event System.Action<MonoBehaviour> OnEventFinish;
    public static event System.Action OnStaticEventFinish;
    public System.Action OnDelegateFinish;
    public static System.Action OnStaticDelegateFinish;

    public static int StaticID { get; set; }
    public int ID { get; set; }

    public void AddDelegate(System.Action action)
    {
        OnDelegateFinish += action;
    }

    public void RemoveDelegate(System.Action action)
    {
        OnDelegateFinish -= action;
    }
    public void AddEvent(System.Action<MonoBehaviour> action)
    {
        OnEventFinish += action;
    }

    public void RemoveEvent(System.Action<MonoBehaviour> action)
    {
        OnEventFinish -= action;
    }
    public void SendEvent()
    {
        if (OnEventFinish != null)
            OnEventFinish(null);
    }

    public static void SendStaticEvent()
    {
        if (OnStaticEventFinish != null)
            OnStaticEventFinish();
    }

    public bool toggle;
    public APIExportTest()
    {
        Debug.LogError("Default Ctor");
    }
    public APIExportTest(int capacity)
    {
        Debug.LogError("Ctor with Capacity: " + capacity);
    }

    public APIExportTest(IEnumerable enumerable)
    {
        Debug.LogError("Ctor with Enumerable: " + enumerable);
    }
    public class RefObject
    {
        public string Name =
            "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

        public int x = 1;
        public int y = 2;

        public void PrintLog()
        {
            Debug.LogError(string.Format("Name:{0}\nx:{1}\ny:{2}\n", Name, x, y));
        }
    }

    public static RefObject StaticObject = new RefObject();

    public static void test123(params object[] ts)
    {
        if (ts != null && ts.Length > 0)
        {
            for (int i = 0; i < ts.Length; i++)
            {
                Debug.LogError("[" + i + "] = " + ts[i]);
            }
        }

    }

    public static void testString(params string[] ts)
    {
        if (ts != null && ts.Length > 0)
        {
            for (int i = 0; i < ts.Length; i++)
            {
                Debug.LogError("[" + i + "] = " + ts[i]);
            }
        }

    }

    public static void testRefParams(ref string a, out string b)
    {
        a = "HelloWorld";
        b = "GameOver";
    }

    public static RefObject[] testReturnArray()
    {
        var result = new RefObject[3];
        for (int i = 0; i < 3; i++)
        {
            var obj = new RefObject();
            obj.x = i;
            obj.y = i;
            obj.Name = "ArrayList";
            result[i] = obj;
        }
        return result;
    }

    public static List<RefObject> testReturnList()
    {
        var result = new List<RefObject>(3);
        for (int i = 0; i < 3; i++)
        {
            var obj = new RefObject();
            obj.x = i;
            obj.y = i;
            obj.Name = "List";
            result.Add(obj);
        }
        return result;
    }

    public static HashSet<RefObject> testReturnHashSet()
    {
        var result = new HashSet<RefObject>();
        for (int i = 0; i < 3; i++)
        {
            var obj = new RefObject();
            obj.x = i;
            obj.y = i;
            obj.Name = "HashSet";
            result.Add(obj);
        }
        return result;
    }

    public static Dictionary<int, RefObject> testReturnDic()
    {
        var result = new Dictionary<int, RefObject>();
        for (int i = 0; i < 3; i++)
        {
            var obj = new RefObject();
            obj.x = i;
            obj.y = i;
            obj.Name = "Dictionary";
            result.Add(i, obj);
        }
        return result;
    }
}