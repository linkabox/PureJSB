using SharpKit.JavaScript;
using UnityEngine;
using System.Collections;


public class AwakeC : MonoBehaviour
{
    public int valueOfC = 0;
    void Awake()
    { 
        valueOfC = 8; 
    }

    void Update()
    {
        Debug.LogError("AwakeC:"+this.GetInstanceID());
    }
}
