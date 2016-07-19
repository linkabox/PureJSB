using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using SharpKit.JavaScript;

public class DateTimeTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        TestDateTime();
    }

    private void TestDateTime()
    {
        var sb = new StringBuilder();
        var dateMin = DateTime.MinValue;
        var dateCustom = new DateTime(2008, 3, 8, 15, 8, 7, 400);

        Debug.Log("=======Ctor=======");
        sb.AppendLine("MinValue:" + DateTime.MinValue);
        sb.AppendLine("MaxValue:" + DateTime.MaxValue);

        var dateTime1 = new DateTime();
        sb.AppendLine("dataTime1:" + dateTime1);

        var dateTime2 = new DateTime(2011, 1, 3);
        sb.AppendLine("dateTime2:" + dateTime2);

        var dateTime3 = new DateTime(2011, 1, 3, 15, 33, 8);
        sb.AppendLine("dateTime3:" + dateTime3);

        var dateTime4 = new DateTime(2011, 1, 3, 15, 33, 8, 30);
        sb.AppendLine("dateTime4:" + dateTime4);

        //Js Number精度问题不支持这种构造方式
        //var dateTime5 = new DateTime(1460106152000);
        //sb.AppendLine("dateTime5:" + dateTime5);
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Property=======");
        sb.AppendLine("Today:" + DateTime.Today);
        sb.AppendLine("Now:" + dateCustom);
        sb.AppendLine("Date:" + dateCustom.Date);
        sb.AppendLine("Year:" + dateCustom.Year);
        sb.AppendLine("Month:" + dateCustom.Month);
        sb.AppendLine("Day:" + dateCustom.Day);

        sb.AppendLine("DayOfWeek:" + dateCustom.DayOfWeek);
        sb.AppendLine("DayOfYear:" + dateCustom.DayOfYear);

        sb.AppendLine("Hour:" + dateCustom.Hour);
        sb.AppendLine("Minute:" + dateCustom.Minute);
        sb.AppendLine("Second:" + dateCustom.Second);
        sb.AppendLine("Millisecond:" + dateCustom.Millisecond);
        sb.AppendLine("Ticks:" + dateCustom.Ticks);
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Add=======");
        sb.AppendLine("AddYears:" + dateMin.AddYears(2));
        sb.AppendLine("AddMonths:" + dateMin.AddMonths(2));
        sb.AppendLine("AddDays:" + dateMin.AddDays(2));

        sb.AppendLine("AddHours:" + dateMin.AddHours(2).Hour);
        sb.AppendLine("AddMinutes:" + dateMin.AddMinutes(2).Minute);
        sb.AppendLine("AddSeconds:" + dateMin.AddSeconds(2).Second);
        sb.AppendLine("AddMilliseconds:" + dateMin.AddMilliseconds(2).Millisecond);
        sb.AppendLine("AddTicks:" + dateMin.AddTicks(2000).Ticks);
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Operator=======");
        sb.AppendLine("Now > Min:" + (dateCustom > dateMin));
        sb.AppendLine("Now >= Min:" + (dateCustom >= dateMin));
        sb.AppendLine("Now == Now:" + (dateCustom == dateMin));
        sb.AppendLine("Now < Min:" + (dateCustom < dateMin));
        sb.AppendLine("Now <= Min:" + (dateCustom <= dateMin));
        sb.AppendLine("Now CompareTo Min:" + dateCustom.CompareTo(dateMin));
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Parse=======");
        sb.AppendLine("Parse1:" + System.DateTime.Parse("2016-4-8"));
        sb.AppendLine("Parse1-1:" + System.DateTime.Parse("2016-4-8 18:40"));
        sb.AppendLine("Parse1-2:" + System.DateTime.Parse("2016-4-8 18:40:28"));
        sb.AppendLine("Parse2:" + System.DateTime.Parse("2016-04-08"));
        sb.AppendLine("Parse2-1:" + System.DateTime.Parse("2016-04-08 18:40"));
        sb.AppendLine("Parse2-2:" + System.DateTime.Parse("2016-04-08 18:40:28"));
        sb.AppendLine("Parse3:" + System.DateTime.Parse("2016/04/08"));
        sb.AppendLine("Parse3-1:" + System.DateTime.Parse("2016/04/08 18:40"));
        sb.AppendLine("Parse3-2:" + System.DateTime.Parse("2016/04/08 18:40:28"));
        sb.AppendLine("Parse4:" + System.DateTime.Parse("2016/4/8"));
        sb.AppendLine("Parse4-1:" + System.DateTime.Parse("2016/4/8 18:40"));
        sb.AppendLine("Parse4-2:" + System.DateTime.Parse("2016/4/8 18:40:28"));
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Format=======");
        sb.AppendLine("ToString:" + dateCustom.ToString());

        sb.AppendLine("ToLongTimeString:" + dateCustom.ToLongTimeString());
        sb.AppendLine("ToLongDateString:" + dateCustom.ToLongDateString());

        sb.AppendLine("ToShortTimeString:" + dateCustom.ToShortTimeString());
        sb.AppendLine("ToShortDateString:" + dateCustom.ToShortDateString());

        sb.AppendLine("ToString(G):" + dateCustom.ToString("G"));
        sb.AppendLine("ToString(yy-MM-dd):" + dateCustom.ToString("yy-MM-dd"));
        sb.AppendLine("ToString(yyyy-MM-dd):" + dateCustom.ToString("yyyy-MM-dd"));
        sb.AppendLine("ToString(hh:mm:ss):" + dateCustom.ToString("hh:mm:ss"));
        sb.AppendLine("ToString(yyyy年MM月dd日):" + dateCustom.ToString("yyyy年MM月dd日"));
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======UnixTimeStamp <=> DateTime=======");
        var unixTimestamp = DateTimeToUnixTimestamp(dateCustom);
        sb.AppendLine("Now -> UnixTimeStamp:" + unixTimestamp);
        sb.AppendLine("UnixTimeStamp -> Now:" + UnixTimeStampToDateTime(unixTimestamp));
        Debug.Log(sb.ToString());
        sb.Length = 0;
    }

    [JsMethod(Code = "return new Date(unixTimestamp);")]
    public static DateTime UnixTimeStampToDateTime(long unixTimestamp)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return dateTime.AddTicks(unixTimestamp * 10000).ToLocalTime();
    }

    [JsMethod(Code = "return dateTime.getTime();")]
    public static long DateTimeToUnixTimestamp(DateTime dateTime)
    {
        return (dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime()).Ticks / 10000;
    }
}
