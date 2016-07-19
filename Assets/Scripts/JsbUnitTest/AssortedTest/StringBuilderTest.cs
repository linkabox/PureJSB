using System;
using UnityEngine;
using System.Collections;
using System.Text;

public class StringBuilderTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        TestStringBuilder();
    }

    private void TestStringBuilder()
    {
        var log = new StringBuilder();
        Debug.Log("=======Ctor=======");
        var sb1 = new StringBuilder();
        log.AppendLine("sb1:" + sb1);
        var sb2 = new StringBuilder(10);
        log.AppendLine("sb2:" + sb2);
        var sb3 = new StringBuilder(10, 1000);
        log.AppendLine("sb3:" + sb3);
        var sb4 = new StringBuilder("HelloWorld");
        log.AppendLine("sb4:" + sb4);
        Debug.Log(log.ToString());
        log.Length = 0;

        Debug.Log("=======Property=======");
        sb1.Append("Hello,my name is xxxx.");
        sb1.Capacity = 1000;
        log.AppendLine("sb1.Capacity:" + sb1.Capacity);
        sb1.Length = 5;
        log.AppendLine("sb1.Length:" + sb1.Length);
        log.AppendLine("sb1.MaxCapacity:" + sb1.MaxCapacity);
        log.AppendLine("sb1[0]:" + sb1[0]);
        sb1[0] = 'G';
        log.AppendLine("sb1:" + sb1);
        Debug.Log(log.ToString());
        log.Length = 0;

        Debug.Log("=======Append=======");
        log.Append(false);
        log.Append('X');
        log.Append(new char[] { 'y', 'u', 'o' });
        log.Append(0.32f);
        log.Append(6.28);
        log.Append(123);
        log.Append(678L);
        Debug.Log("Length:" + log.Length + " " + log.ToString());
        log.Length = 0;

        Debug.Log("=======AppendLine=======");
        log.Append("hehe");
        log.AppendLine();
        log.AppendLine("you are foo");
        Debug.Log("Length:" + log.Length + " " + log.ToString());
        log.Length = 0;

        Debug.Log("=======AppendFormat=======");
        log.AppendFormat("int:{0},float:{1},double:{2},bool:{3}", 123, 3.14, 6.28, true);
        Debug.Log("Length:" + log.Length + " " + log.ToString());
        log.Length = 0;

        Debug.Log("=======Insert=======");
        log.Append("|Mid|");
        log.Insert(0, false);
        log.Insert(0, false);
        log.Insert(0, 'X');
        log.Insert(0, new char[] { 'y', 'u', 'o' });
        log.Insert(0, 0.32f);
        log.Insert(0, 6.28);
        log.Insert(0, 123);
        log.Insert(0, 678L);
        Debug.Log("Length:" + log.Length + " " + log.ToString());
        log.Length = 0;

        Debug.Log("=======Remove=======");
        log.Append("Something to remove");
        log.Remove(0, 3);
        Debug.Log("Length:" + log.Length + " " + log.ToString());
        log.Length = 0;

        Debug.Log("=======Replace=======");
        log.Append("Something to replace");
        log.Append("Something to change");
        log.Replace("Something", "Any");
        log.Replace('e', 'X');
        Debug.Log("Length:" + log.Length + " " + log.ToString());
        log.Length = 0;
    }
}
