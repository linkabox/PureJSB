using SharpKit.JavaScript;
using UnityEngine;
using System.Collections;


public class AwakeA : MonoBehaviour
{
    public int valueOfA = 0;
	void Awake () {
        var b = GetComponent<AwakeB>();
        print(string.Format("A.GetComponent<B>: {0}, value: {1}", b.name, b.valueOfB));
	}
	
}
