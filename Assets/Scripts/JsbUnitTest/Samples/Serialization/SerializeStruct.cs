using SharpKit.JavaScript;
using System;
using UnityEngine;
using System.Collections;


public class SerializeStruct : MonoBehaviour
{
    [System.Serializable]

//[JsType(JsMode.Clr,"../../StreamingAssets/JavaScript/SharpKitGenerated/Serialization/AppleInfo.javascript")]


    public struct AppleInfo
    { 
        public int age;
        public GameObject go;
        public string firstName;
        public bool doYouLoveMe;
    }
    public AppleInfo appleInfo;

    void Start()
    {
        Debug.Log("age: " + appleInfo.age);
        if (appleInfo.go != null)
            Debug.Log("go: " + appleInfo.go.name);
        else
            Debug.Log("go: null");
        Debug.Log("firstName: " + appleInfo.firstName);
        Debug.Log("doYouLoveMe: " + (appleInfo.doYouLoveMe ? "true" : "false"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
