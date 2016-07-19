using System;
using SharpKit.JavaScript;
using System.Collections.Generic;
using LITJson;
using System.Linq;
using System.Reflection;
using UnityEngine;

[assembly: JsMethod(TargetType = typeof(string), TargetMethod = ".ctor", OmitNewOperator = true)]
[assembly: JsProperty(TargetType = typeof(Time), Global = true, NativeField = true,
    TargetProperty = "deltaTime", Name = "_jsComManager.dT")]
[assembly: JsProperty(TargetType = typeof(Time), Global = true, NativeField = true,
    TargetProperty = "fixedDeltaTime", Name = "_jsComManager.fDT")]
[assembly: JsProperty(TargetType = typeof(Time), Global = true, NativeField = true,
    TargetProperty = "unscaledDeltaTime", Name = "_jsComManager.uDT")]

[JsType(JsMode.Prototype)]
public static class JsHelper
{
    [JsMethod(Code = @"return Number.isInteger(obj);")]
    public static bool IsInt(this object obj)
    {
        return obj is int;
    }

    [JsMethod(Code = @"return typeof obj === 'number';")]
    public static bool IsFloat(this object obj)
    {
        return obj is float;
    }

    #region Collection

    [JsMethod(Code = @"var count = list.get_Count();
    if (count == 0)
        return null;
    var randomIndex = Math.floor(count*Math.random());
    return list.get_Item$$Int32(randomIndex);")]
    public static T Random<T>(this IList<T> list)
    {
        var count = list.Count;

        if (count == 0)
            return default(T);

        return list.ElementAt(UnityEngine.Random.Range(0, count));
    }

    #endregion

    #region Json

    [JsMethod(Code = @"return JsonUtils.stringify(obj, tFlag);")]
    public static string ToJson(object obj, bool tFlag = false)
    {
        if (tFlag)
        {
            JsonWriter writer = new JsonWriter();
            //writer.TypeWriter = null;
            writer.TypeHinting = true;
            JsonMapper.ToJson(obj, writer);
            return writer.ToString();
        }
        else
        {
            return JsonMapper.ToJson(obj);
        }
    }

    [JsMethod(Code = @"return JsonUtils.parse(json, T);", IgnoreGenericArguments = false)]
    public static T ToObject<T>(string json)
    {
        try
        {
            return JsonMapper.ToObject<T>(json);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return default(T);
        }
    }

    [JsMethod(Code = @"return JsonUtils.parse(json, T, TChild);", IgnoreGenericArguments = false)]
    public static T ToCollection<T, TChild>(string json)
    {
        return JsonMapper.ToObject<T>(json);
    }

    #endregion
}