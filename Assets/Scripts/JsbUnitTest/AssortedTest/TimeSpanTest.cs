using System;
using UnityEngine;
using System.Collections;
using System.Text;

public class TimeSpanTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        TestDateTime();
    }

    private void TestDateTime()
    {
        var sb = new StringBuilder();
        var tsMin = TimeSpan.MinValue;
        var tsCustom = new TimeSpan(6, 3, 1, 4, 3448);

        Debug.Log("=======Static Field=======");
        sb.AppendLine("MinValue:" + TimeSpan.MinValue);
        sb.AppendLine("MaxValue:" + TimeSpan.MaxValue);

        sb.AppendLine("TicksPerDay:" + TimeSpan.TicksPerDay);
        sb.AppendLine("TicksPerHour:" + TimeSpan.TicksPerHour);
        sb.AppendLine("TicksPerMinute:" + TimeSpan.TicksPerMinute);
        sb.AppendLine("TicksPerSecond:" + TimeSpan.TicksPerSecond);
        sb.AppendLine("TicksPerMillisecond:" + TimeSpan.TicksPerMillisecond);

        sb.AppendLine("Zero:" + TimeSpan.Zero);
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Ctor=======");
        var timeSpan1 = new TimeSpan();
        sb.AppendLine("timeSpan1:" + timeSpan1);

        var timeSpan2 = new TimeSpan(3, 1, 4);
        sb.AppendLine("timeSpan2:" + timeSpan2);

        var timeSpan3 = new TimeSpan(3, 1, 4, 8);
        sb.AppendLine("timeSpan3:" + timeSpan3);

        var timeSpan4 = new TimeSpan(3, 1, 4, 8, 700);
        sb.AppendLine("timeSpan4:" + timeSpan4);

        var timeSpan5 = new TimeSpan(604800000L * 10000); //一星期的Ticks
        sb.AppendLine("timeSpan5:" + timeSpan5);

        var timeSpan6 = TimeSpan.FromTicks(604800000L * 10000);
        sb.AppendLine("FromTicks:" + timeSpan6);

        var timeSpan7 = TimeSpan.FromDays(7);
        sb.AppendLine("FromDays:" + timeSpan7);

        var timeSpan8 = TimeSpan.FromHours(7 * 24);
        sb.AppendLine("FromHours:" + timeSpan8);

        var timeSpan9 = TimeSpan.FromMinutes(7 * 24 * 60);
        sb.AppendLine("FromMinutes:" + timeSpan9);

        var timeSpan10 = TimeSpan.FromSeconds(7 * 24 * 60 * 60);
        sb.AppendLine("FromSeconds:" + timeSpan10);

        var timeSpan11 = TimeSpan.FromMilliseconds(7 * 24 * 60 * 60 * 1000);
        sb.AppendLine("FromMilliseconds:" + timeSpan11);
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Property=======");
        sb.AppendLine("Days:" + tsCustom.Days);
        sb.AppendLine("Hours:" + tsCustom.Hours);
        sb.AppendLine("Minutes:" + tsCustom.Minutes);
        sb.AppendLine("Seconds:" + tsCustom.Seconds);
        sb.AppendLine("Milliseconds:" + tsCustom.Milliseconds);

        sb.AppendLine("Ticks:" + tsCustom.Ticks);
        sb.AppendLine("TotalDays:" + tsCustom.TotalDays);
        sb.AppendLine("TotalHours:" + tsCustom.TotalHours);
        sb.AppendLine("TotalMinutes:" + tsCustom.TotalMinutes);
        sb.AppendLine("TotalSeconds:" + tsCustom.TotalSeconds);
        sb.AppendLine("TotalMilliseconds:" + tsCustom.TotalMilliseconds);
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Add=======");
        sb.AppendLine("Add:" + TimeSpan.Zero.Add(new TimeSpan(2, 30, 50)));
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Operator=======");
        sb.AppendLine("Now > Min:" + (tsCustom > tsMin));
        sb.AppendLine("Now >= Min:" + (tsCustom >= tsMin));
        sb.AppendLine("Now == Now:" + (tsCustom == tsMin));
        sb.AppendLine("Now < Min:" + (tsCustom < tsMin));
        sb.AppendLine("Now <= Min:" + (tsCustom <= tsMin));
        sb.AppendLine("Now CompareTo Min:" + tsCustom.CompareTo(tsMin));
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Format=======");
        sb.AppendLine("TimeSpan(6, 3, 1, 4, 3448):" + tsCustom.ToString());
        sb.AppendLine("TimeSpan(16, 0, 0, 0, 0):" + new TimeSpan(16, 0, 0, 0, 0));
        sb.AppendLine("TimeSpan(0, 0, 0, 0, 448):" + new TimeSpan(0, 0, 0, 0, 448));
        sb.AppendLine("TimeSpan(0, 3, 1, 4, 0):" + new TimeSpan(0, 3, 1, 4, 0));
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Parse=======");
        //dd.HH:mm:ss.fff
        //不支持 "6.12:14:45,3448","6.34:14:45"
        string[] strValues = { "6", "6:12", "6:12:14", "6.12:14:45", "6.12:14:45", "6.12:14:45.3448" };
        foreach (string s in strValues)
        {
            var ts = TimeSpan.Parse(s);
            sb.AppendLine(s + " --> " + ts);
        }
        Debug.Log(sb.ToString());
        sb.Length = 0;
    }
}
