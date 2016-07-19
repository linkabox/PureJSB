using UnityEngine;
using System.Collections;
using System.Text;

public class MathTest : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        TestMath();
    }

    void TestMath()
    {
        var sb = new StringBuilder();
        Debug.Log("=======Property=======");
        sb.AppendLine("Deg2Rad:" + Mathf.Deg2Rad);
        sb.AppendLine("Rad2Deg:" + Mathf.Rad2Deg);
        sb.AppendLine("Epsilon:" + Mathf.Epsilon);
        sb.AppendLine("Infinity:" + Mathf.Infinity);
        sb.AppendLine("NegativeInfinity:" + Mathf.NegativeInfinity);
        sb.AppendLine("PI:" + Mathf.PI);
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Base Function=======");
        sb.AppendLine("Abs Int:" + Mathf.Abs(-10));
        sb.AppendLine("Abs Float:" + Mathf.Abs(-10.5f));
        sb.AppendLine("Approximately:" + Mathf.Approximately(1.0f, 10.0f / 10.0f));
        sb.AppendLine("Repeat:" + Mathf.Repeat(3f, 2.5f));
        sb.AppendLine("DeltaAngle:" + Mathf.DeltaAngle(1080, 90));
        sb.AppendLine("Sqrt:" + Mathf.Sqrt(12));
        sb.AppendLine("Pow:" + Mathf.Pow(6, 1.8f));
        sb.AppendLine("Exp:" + Mathf.Exp(6));
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Triangle Function=======");
        sb.AppendLine("Sin:" + Mathf.Sin(3));
        sb.AppendLine("Asin:" + Mathf.Asin(0.5f));
        sb.AppendLine("Cos:" + Mathf.Cos(3));
        sb.AppendLine("Acos:" + Mathf.Acos(0.5f));
        sb.AppendLine("Tan:" + Mathf.Tan(0.5f));
        sb.AppendLine("Atan:" + Mathf.Atan(0.5f));
        sb.AppendLine("Atan2:" + Mathf.Atan2(0.5f, 0.5f));
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Ceil Function=======");
        sb.AppendLine("Ceil 10.0:" + Mathf.Ceil(10.0f));
        sb.AppendLine("Ceil 10.2:" + Mathf.Ceil(10.2f));
        sb.AppendLine("Ceil 10.7:" + Mathf.Ceil(10.7f));
        sb.AppendLine("Ceil -10.0:" + Mathf.Ceil(-10.0f));
        sb.AppendLine("Ceil -10.2:" + Mathf.Ceil(-10.2f));
        sb.AppendLine("Ceil -10.7:" + Mathf.Ceil(-10.7f));

        sb.AppendLine("CeilToInt 10.0:" + Mathf.CeilToInt(10.0f));
        sb.AppendLine("CeilToInt 10.2:" + Mathf.CeilToInt(10.2f));
        sb.AppendLine("CeilToInt 10.7:" + Mathf.CeilToInt(10.7f));
        sb.AppendLine("CeilToInt -10.0:" + Mathf.CeilToInt(-10.0f));
        sb.AppendLine("CeilToInt -10.2:" + Mathf.CeilToInt(-10.2f));
        sb.AppendLine("CeilToInt -10.7:" + Mathf.CeilToInt(-10.7f));
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Floor Function=======");
        sb.AppendLine("Floor 10.0:" + Mathf.Floor(10.0f));
        sb.AppendLine("Floor 10.2:" + Mathf.Floor(10.2f));
        sb.AppendLine("Floor 10.7:" + Mathf.Floor(10.7f));
        sb.AppendLine("Floor -10.0:" + Mathf.Floor(-10.0f));
        sb.AppendLine("Floor -10.2:" + Mathf.Floor(-10.2f));
        sb.AppendLine("Floor -10.7:" + Mathf.Floor(-10.7f));

        sb.AppendLine("FloorToInt 10.0:" + Mathf.FloorToInt(10.0f));
        sb.AppendLine("FloorToInt 10.2:" + Mathf.FloorToInt(10.2f));
        sb.AppendLine("FloorToInt 10.7:" + Mathf.FloorToInt(10.7f));
        sb.AppendLine("FloorToInt -10.0:" + Mathf.FloorToInt(-10.0f));
        sb.AppendLine("FloorToInt -10.2:" + Mathf.FloorToInt(-10.2f));
        sb.AppendLine("FloorToInt -10.7:" + Mathf.FloorToInt(-10.7f));
        Debug.Log(sb.ToString());
        sb.Length = 0;

        //Round方法C#与Js有一定偏差,使用时要注意
        //C#遵循的是IEEE的标准,规则如下
        //舍位小于等于4,不进位
        //舍位等于5时,看进位,奇进偶舍
        //舍位大于等于6,进位
        Debug.Log("=======Round Function=======");
        sb.AppendLine("Round 10.0:" + Mathf.Round(10.0f));
        sb.AppendLine("Round 10.2:" + Mathf.Round(10.2f));
        sb.AppendLine("Round 10.7:" + Mathf.Round(10.7f));
        sb.AppendLine("Round 10.5:" + Mathf.Round(10.5f));
        sb.AppendLine("Round 11.5:" + Mathf.Round(11.5f));
        sb.AppendLine("Round -10.0:" + Mathf.Round(-10.0f));
        sb.AppendLine("Round -10.2:" + Mathf.Round(-10.2f));
        sb.AppendLine("Round -10.7:" + Mathf.Round(-10.7f));
        sb.AppendLine("Round -10.5:" + Mathf.Round(-10.5f));
        sb.AppendLine("Round -11.5:" + Mathf.Round(-11.5f));

        sb.AppendLine("RoundToInt 10.0:" + Mathf.RoundToInt(10.0f));
        sb.AppendLine("RoundToInt 10.2:" + Mathf.RoundToInt(10.2f));
        sb.AppendLine("RoundToInt 10.7:" + Mathf.RoundToInt(10.7f));
        sb.AppendLine("RoundToInt 10.5:" + Mathf.RoundToInt(10.5f));
        sb.AppendLine("RoundToInt 11.5:" + Mathf.RoundToInt(11.5f));
        sb.AppendLine("RoundToInt -10.0:" + Mathf.RoundToInt(-10.0f));
        sb.AppendLine("RoundToInt -10.2:" + Mathf.RoundToInt(-10.2f));
        sb.AppendLine("RoundToInt -10.7:" + Mathf.RoundToInt(-10.7f));
        sb.AppendLine("RoundToInt -10.5:" + Mathf.RoundToInt(-10.5f));
        sb.AppendLine("RoundToInt -11.5:" + Mathf.RoundToInt(-11.5f));
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Sign Function=======");
        sb.AppendLine("Sign -10:" + Mathf.Sign(-10));
        sb.AppendLine("Sign 10:" + Mathf.Sign(10));
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Clamp Function=======");
        sb.AppendLine("Clamp Int:" + Mathf.Clamp(10, 1, 3));
        sb.AppendLine("Clamp Float:" + Mathf.Clamp(10.0f, 1.0f, 3.0f));
        sb.AppendLine("Clamp01:" + Mathf.Clamp01(3.14f));
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Lerp Function=======");
        sb.AppendLine("Lerp:" + Mathf.Lerp(1, 100, 0.3f));
        sb.AppendLine("InverseLerp:" + Mathf.InverseLerp(0, 100, 30));
        sb.AppendLine("LerpAngle:" + Mathf.LerpAngle(0.0f, 90f, 0.3f));
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======Log Function=======");
        sb.AppendLine("Log:" + Mathf.Log(6, 2));
        sb.AppendLine("Log E:" + Mathf.Log(100));
        sb.AppendLine("Log10:" + Mathf.Log10(100));
        Debug.Log(sb.ToString());
        sb.Length = 0;

        Debug.Log("=======MaxMin Function=======");
        sb.AppendLine("Max Int:" + Mathf.Max(1, 2));
        sb.AppendLine("Max IntArray:" + Mathf.Max(1, 2, 3, 4, 5));
        sb.AppendLine("Max Float:" + Mathf.Max(1.2f, 2.4f));
        sb.AppendLine("Max FloatArray:" + Mathf.Max(1.2f, 2.4f, 3.5f, 4.6f, 5.7f));

        sb.AppendLine("Min Int:" + Mathf.Min(1, 2));
        sb.AppendLine("Min IntArray:" + Mathf.Min(1, 2, 3, 4, 5));
        sb.AppendLine("Min Float:" + Mathf.Min(1.2f, 2.4f));
        sb.AppendLine("Min FloatArray:" + Mathf.Min(1.2f, 2.4f, 3.5f, 4.6f, 5.7f));
        Debug.Log(sb.ToString());
        sb.Length = 0;
    }
}
