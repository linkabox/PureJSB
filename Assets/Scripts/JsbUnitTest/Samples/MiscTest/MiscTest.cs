using SharpKit.JavaScript;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MiscTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
//        PerTest.test123(1.5, 1.3);
        //PerTest.testString(null, "abc");
	    var logo = Resources.Load("Textures/logo");
        Debug.LogError("Resources.Load: "+logo);

	    logo = Resources.Load<Texture2D>("Textures/logo");
        Debug.LogError("Resources.Load<T>: " + logo);

        List<string> contents = new List<string>();
        contents.Add("hello");
        contents.Add("fxck");
        contents.Add("foo");
	    Debug.LogError(contents.Random());

        Debug.LogError("ForEach contents");
	    foreach (string content in contents)
	    {
	        Debug.LogError(content);
	    }
        Debug.LogError("For contents");
	    for (int i = 0; i < contents.Count; i++)
	    {
	        Debug.LogError(contents[i]);
	    }

        var obj = new APIExportTest();
        obj.IDs.Add(123);
        obj.IDs.Add(456);
        Debug.LogError("For IDs");
        for (int i = 0; i < obj.IDs.Count; i++)
	    {
	        Debug.LogError(obj.IDs[i]);
	    }
        Debug.LogError("ForEach IDs");
        foreach (int i in obj.IDs)
	    {
	        Debug.LogError(i);
	    }
        //PrintStrings("a", "b", "c");
        //print(null);
    }
    void PrintStrings(string s, params string[] strs)
    {
        foreach (var v in strs)
            print(v);
    }
}
