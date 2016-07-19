using SharpKit.JavaScript;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DictionaryTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}


    float elapsed = 0;
	void Update ()
    {
        elapsed += Time.deltaTime;
        if (elapsed > 1f)
        {
            elapsed = 0f;
            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict.Add("qiucw", 28);
            dict.Add("helj", 27);

            int age;
            if (dict.TryGetValue("qiucw", out age))
            {
                Debug.Log("age: " + age.ToString());
            }
            else
            {
                Debug.Log("not found");
            }
            foreach (var v in dict)
            {
                Debug.Log(v.Key.ToString() + "->" + v.Value.ToString());
            }
        }
	}
}
