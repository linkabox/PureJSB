using UnityEngine;
using System.Collections;
using System;

/*
 * 用于指定脚本要使用的 JSComponent 名
 * 
 * 例如
 * JSComponent(Name = "JSComponent_EventTrigger")
 */
[AttributeUsage(AttributeTargets.Class)]
public class JSComponentAttribute : Attribute
{
    public string Name { get; set; }
}

/// <summary>
/// 对C#导出类接口生成的代码做特殊处理，如：NGUITools.AddMissingComponent方法
/// </summary>
[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Method, AllowMultiple = true)]
public class CsExportedMethodAttribute : Attribute
{
    public Type TargetType { get; set; }
    public string TargetMethodName { get; set; }
    public string JsCode { get; set; }
}