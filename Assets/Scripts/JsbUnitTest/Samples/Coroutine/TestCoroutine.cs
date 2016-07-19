using SharpKit.JavaScript;
using UnityEngine;
using System.Collections;


public class TestCoroutine : MonoBehaviour {
	void Start () 
    {
        StartCoroutine("DoTest");
        StartCoroutine(DoTest2(() => { 
            Debug.Log("Action called!"); 
        }));
        InvokeRepeating("PrintHelloInvoke", 4f, 1f);
        Invoke("DelayInvoke", 5f);
	}
	
	void Update () 
    {
        if (Input.GetMouseButtonUp(0))
        {
//             if (IsInvoking("PrintHelloInvoke"))
//             {
//                 CancelInvoke("PrintHelloInvoke");
//                 print("PrintHelloInvoke has been stopped!");
//             }
            StopCoroutine("DoTest");
        }
	}
    IEnumerator WaitForCangJingKong()
    {
        yield return new WaitForSeconds(2f);
    }
    IEnumerator DoTest()
    {
        // test null
        Debug.Log("DoTest 1");
        yield return null;

        // test WaitForSeconds
        Debug.Log("DoTest 2");
        yield return new WaitForSeconds(1f);

        // 
        // ^u BuildSetting 
//         Debug.Log("Do Test LoadLevelAdditive");
//         yield return Application.LoadLevelAdditiveAsync("AddScene");

        // test WWW
        WWW www = new WWW("file://" + Application.dataPath + "/JSBinding/Samples/Coroutine/CoroutineReadme.txt");
        yield return www;
        Debug.Log("DoTest 3 Text from WWW: " + www.text);

        // test another coroutine
        yield return StartCoroutine(WaitForCangJingKong());
        Debug.Log("DoTest 4 Wait for CangJingKong finished!");
    }
    IEnumerator DoTest2(System.Action a)
    {
        Debug.Log("will call action 2 seconds later");
        yield return new WaitForSeconds(2f);
        a();
    }
    void PrintHelloInvoke()
    {
        print("Hello, Invoke! (every 1 second)");
    }
    void DelayInvoke()
    {
        print("This is call 5 seconds later, only once!");
    }
}
