using SharpKit.JavaScript;
using UnityEngine;
using System.Collections;
using System.Text;

// JS VERSION OF THIS TEST DOES NOT WORK


public class EncodingTest : MonoBehaviour 
{
    public static string byteConverString(byte[] data, int index, int count)
    {
        Decoder d = Encoding.UTF8.GetDecoder(); // (1)
        int arrSize = d.GetCharCount(data, index, count);
        char[] chars = new char[arrSize];
        int charSize = d.GetChars(data, index, count, chars, 0);
        string str = new string(chars, 0, charSize);
        return str;
    }
    void Start()
    {
        byte[] bytes = new byte[] { 65, 66, 67, 0 };
        string str = byteConverString(bytes, 0, 3);
        Debug.Log(str);
    }
}
