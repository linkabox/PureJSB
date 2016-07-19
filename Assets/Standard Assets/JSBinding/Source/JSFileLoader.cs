using System;
using UnityEngine;
using System.Collections;
using System.IO;


/*
 * JSFileLoader
 * All js files are loaded by this class.
 */
public static class JSFileLoader
{
    public static byte[] LoadJSSync(string scriptName)
    {
        string filePath = GetJsScriptPath(scriptName);
        try
        {
            return File.ReadAllBytes(filePath);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return null;
        }
    }

    /// <summary>
    /// Gets the full name of the javascript file.
    /// </summary>
    /// <param name="jsScriptName">The short name.</param>
    /// <returns></returns>
    public static string GetJsScriptPath(string jsScriptName)
    {
        if (jsScriptName.EndsWith(".json"))
            return jsScriptName;

        string minPath = JSPathSettings.jsDir + "/" + jsScriptName + ".min" + JSPathSettings.jsExtension;
        if (File.Exists(minPath))
        {
            return minPath;
        }
        return JSPathSettings.jsDir + "/" + jsScriptName + JSPathSettings.jsExtension;
    }
}
