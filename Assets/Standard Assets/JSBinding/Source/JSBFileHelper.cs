using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using LITJson;
using UnityEngine;


public static class JSBFileHelper
{
    /// <summary>
    ///     Determines if is exist the specified Path.
    /// </summary>
    /// <returns><c>true</c> if is exist the specified Path; otherwise, <c>false</c>.</returns>
    /// <param name="path"></param>
    public static bool IsExist(string path)
    {
        return File.Exists(path);
    }

    /// <summary>
    ///     Creates the directory.
    /// </summary>
    /// <param name="path">Path.</param>
    public static void CreateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    public static void DeleteDirectory(string dir, bool recursive)
    {
        if (Directory.Exists(dir))
        {
            Directory.Delete(dir, recursive);
        }
    }

    /// <summary>
    /// 根据不同平台生成对应本地文件的Url,Window下需要使用file:///
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string GetLocalFileUrl(string filePath)
    {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            return "file:///" + filePath;
#else
        return "file://" + filePath;
#endif
    }

    public static void DeleteFile(string file)
    {
        if (File.Exists(file))
        {
            File.Delete(file);
        }
    }

    #region clipBoard

    private static PropertyInfo _copyBufferInfo;

    private static PropertyInfo GetSystemCopyBufferProperty()
    {
        if (_copyBufferInfo == null)
        {
            _copyBufferInfo = typeof(GUIUtility).GetProperty("systemCopyBuffer", BindingFlags.Static | BindingFlags.NonPublic);
            if (_copyBufferInfo == null)
                throw new Exception(
                    "Can't access internal member 'GUIUtility.systemCopyBuffer' it may have been removed / renamed");
        }
        return _copyBufferInfo;
    }

    public static string ClipBoard
    {
        get
        {
            var p = GetSystemCopyBufferProperty();
            if (p != null)
            {
                return (string)p.GetValue(null, null);
            }
            return "";
        }
        set
        {
            var p = GetSystemCopyBufferProperty();
            if (p != null)
                p.SetValue(null, value, null);
        }
    }

    #endregion

    #region Sync Read File

    /// <summary>
    ///     Reads all bytes.
    /// </summary>
    /// <returns>The all bytes.</returns>
    /// <param name="path">Path.</param>
    public static byte[] ReadAllBytes(string path)
    {
        try
        {
            byte[] data = File.ReadAllBytes(path);
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
        return null;
    }

    /// <summary>
    ///     Reads all text.
    /// </summary>
    /// <returns>The all text.</returns>
    /// <param name="path">Path.</param>
    public static string ReadAllText(string path)
    {
        try
        {
            string data = File.ReadAllText(path);
            return data;
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
        return null;
    }
    #endregion

    #region Sync Write File
    internal static void WriteAllBytes(string path, byte[] bytes)
    {
        try
        {
            string dir = Path.GetDirectoryName(path);
            CreateDirectory(dir);

            File.WriteAllBytes(path, bytes);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    /// <summary>
    ///     Writes all text.
    /// </summary>
    /// <param name="path">Path.</param>
    /// <param name="text"></param>
    public static void WriteAllText(string path, string text)
    {
        try
        {
            string dir = Path.GetDirectoryName(path);
            CreateDirectory(dir);

            File.WriteAllText(path, text);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    #endregion

}