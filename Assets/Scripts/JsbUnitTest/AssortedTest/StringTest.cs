using System;
using UnityEngine;
using System.Collections;

public class StringTest: MonoBehaviour
{
    private void Start()
    {
        S_Ctor();
        S_Join();
        S_ToCharArray();
        S_Equals();
        S_Compare();
    }

    private void S_Ctor()
    {
        Debug.Log("");
        Debug.Log("S_Ctor");

        var strs = new char[] {'a', 'b', 'c'};
//        var str = new string(strs[0], 3);
//        Debug.Log(str);
        var str = new string(strs);
        Debug.Log("ctor$$Char$Array: " + str);
        str = new string(strs, 1, 2);
        Debug.Log("ctor$$Char$Array$$Int32$$Int32: " + str);
    }

    private void S_Join()
    {
        Debug.Log("");
        Debug.Log("S_Join");

        var strs = new[] {"a", "ab", "abc", "abcd"};
        Debug.Log(string.Join("|", strs));
        Debug.Log(string.Join("|", strs, 1, 2));
    }


    private void S_ToCharArray()
    {
        Debug.Log("");
        Debug.Log("S_ToCharArray");

        var str = "abcdefg";
        foreach (var s in str.ToCharArray())
        {
            Debug.Log(s);
        }
        foreach (var s in str.ToCharArray(2, 2))
        {
            Debug.Log(s);
        }
    }

    private void S_Equals()
    {
        Debug.Log("");
        Debug.Log("S_Equals");

        var str1 = "abcd";
        var str2 = "abcd";
        var str3 = "ABCD";

        Debug.Log(str1.Equals(str2));
        Debug.Log(str1.Equals(str3));
        Debug.Log(str1.Equals(str3, StringComparison.CurrentCultureIgnoreCase));
        Debug.Log(string.Equals(str1, str2));
        Debug.Log(string.Equals(str1, str3));
        Debug.Log(string.Equals(str1, str3, StringComparison.CurrentCultureIgnoreCase));
    }

    private void S_Compare()
    {
        Debug.Log("");
        Debug.Log("S_Compare");

        var str1 = "abcd";
        var str2 = "abcd";
        var str3 = "ABCD";

        Debug.Log(string.Compare(str1, str2));
        Debug.Log(string.Compare(str1, str3));
        Debug.Log(string.Compare(str1, str3, true));
    }
}
