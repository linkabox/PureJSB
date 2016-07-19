using SharpKit.JavaScript;
using UnityEngine;
using System.Collections;


public class AwakeB : MonoBehaviour {

    public int valueOfB = 0;
    void Awake()
    {
        var a = GetComponent<AwakeA>();
        print(string.Format("B.GetComponent<A>: {0}, value: {1}", a.name, a.valueOfA));

        var c = gameObject.AddComponent<AwakeC>();
        print("c.valueOfC = " + c.valueOfC);
        gameObject.AddComponent<AwakeC>();
        gameObject.AddComponent<AwakeC>();
        gameObject.AddComponent<AwakeC>();
        gameObject.AddComponent<AwakeC>();
    }
	
}
