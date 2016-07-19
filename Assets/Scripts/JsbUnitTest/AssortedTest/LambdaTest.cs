using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LambdaTest : MonoBehaviour
{
    private void Start()
    {
        L_Closure();
    }

    private void L_Closure()
    {
        Debug.Log("");
        Debug.Log("L_Closure");

        var aList = new List<Action>();
        var bList = new List<Action>();
        var cList = new List<Action<int>>();
        for (int i = 0; i < 3; i++)
        {
            aList.Add(() => Debug.Log("aList:"+i));
            var t = i;
            bList.Add(() => Debug.Log("bList:" + t));
            cList.Add(i1 => Debug.Log("cList:" + i1));
        }
        for (int i = 0; i < 3; i++)
        {
            aList[i]();
        }
        for (int i = 0; i < 3; i++)
        {
            bList[i]();
        }
        for (int i = 0; i < 3; i++)
        {
            cList[i](i);
        }
    }
}
