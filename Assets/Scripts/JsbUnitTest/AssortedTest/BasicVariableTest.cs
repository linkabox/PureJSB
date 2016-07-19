using System;
using UnityEngine;
using System.Collections;

public class BasicVariableTest : MonoBehaviour
{
    private void Start()
    {
        BS_ToString();
        BS_Parse();
        BS_TryParse();
        BS_ComapreTo();
    }

    private void BS_ToString()
    {
        Debug.Log("==============BS_ToString==============");

        int i = 1;
        long l = 2;
        float f = 3.33f;
        double d = 4.44;

        Debug.Log(i.ToString("D5"));
        Debug.Log(i.ToString("D5"));
        Debug.Log(f.ToString("F1"));
        Debug.Log(d.ToString("F1"));
        Debug.Log("==============BS_ToString==============");
    }

    private void BS_Parse()
    {
        Debug.Log("==============BS_Parse==============");
        string s1 = "-14124";
        string s2 = "123.456";
        Debug.Log("int.Parse:" + int.Parse(s1));
        Debug.Log("long.Parse:" + long.Parse(s1));
        Debug.Log("float.Parse:" + float.Parse(s2));
        Debug.Log("double.Parse:" + double.Parse(s2));
        Debug.Log("==============BS_Parse==============");
    }

    private void BS_TryParse()
    {
        Debug.Log("==============BS_TryParse==============");
        int i = 1;
        long l = 2;
        float f = 3.33f;
        double d = 4.44;
        var s = "123.456";
        int.TryParse(s, out i);
        Debug.Log("int.TryParse:" + i);
        long.TryParse(s, out l);
        Debug.Log("long.TryParse:" + l);
        float.TryParse(s, out f);
        Debug.Log("float.TryParse:" + f);
        double.TryParse(s, out d);
        Debug.Log("double.TryParse:" + d);
        Debug.Log("==============BS_TryParse==============");
    }


    private void BS_ComapreTo()
    {
        Debug.Log("==============BS_ComapreTo==============");

        int i = 1;
        long l = 2;
        float f = 3.33f;
        double d = 4.44;
        bool b = false;
        Debug.Log("Int.CompareTo:" + i.CompareTo(i));
        Debug.Log("Int.CompareTo:" + i.CompareTo(i + 1));
        Debug.Log("Long.CompareTo:" + l.CompareTo(l));
        Debug.Log("Long.CompareTo:" + l.CompareTo(l + 1));
        Debug.Log("Float.CompareTo:" + f.CompareTo(f));
        Debug.Log("Float.CompareTo:" + f.CompareTo(f + 1));
        Debug.Log("Double.CompareTo:" + d.CompareTo(d));
        Debug.Log("Double.CompareTo:" + d.CompareTo(d + 1));
        Debug.Log("Boolean.CompareTo:" + b.CompareTo(b));
        Debug.Log("Boolean.CompareTo:" + b.CompareTo(!b));
        Debug.Log("==============BS_ComapreTo==============");
    }
}
