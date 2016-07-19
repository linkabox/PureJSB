using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LingQTest : MonoBehaviour
{
    private void Start()
    {
        LQ_ToList();
        LQ_Any();
    }

    private void LQ_ToList()
    {
        Debug.Log("");
        Debug.Log("LQ_ToList");

        var array = new []{"a", "ab", "abc"};
        var list = array.ToList();
        foreach (var l in list)
        {
            Debug.Log(l);
        }
        list = list.ToList();
        foreach (var l in list)
        {
            Debug.Log(l);
        }
    }


    private void LQ_Any()
    {
        Debug.Log("");
        Debug.Log("LQ_Any");

        var list = new List<string>()
        {
            "a",
            "ab",
            "abc",
        };
        Debug.Log(list.Any(s => s == "a"));
        Debug.Log(list.Any(s => s == "cba"));
        Debug.Log(list.Any());
        list.Clear();
        Debug.Log(list.Any());
    }
}
