using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class JSCGenerator
{
#if UNITY_EDITOR_WIN
    //[MenuItem("JSB/Compile all JavaScript to Bytecode", false, 151)]
    public static void ConvertJavaScriptToBytecode()
    {
        // 1)
        CopyAndChangeExt();

        // 2)
        string bat = (Application.dataPath + "/../../tools/cocos2d-console/bin/cocos").Replace('/', '\\');
        string arg = string.Format("jscompile -s \"{0}\" -d \"{1}\"", JSPathSettings.mergedJsDir.Replace('/', '\\'), JSPathSettings.jscDir.Replace('/', '\\'));
        var process = System.Diagnostics.Process.Start(bat, arg);
		process.WaitForExit(); // 等待结束
    }
#endif
	
	//[MenuItem("JSB/Change Extension .jsc to .bytes", false, 161)]
	public static void ChangeExtensionJsc2Bytes()
	{
		string[] arr = Directory.GetFiles (JSPathSettings.jscDir, "*.jsc", SearchOption.AllDirectories);
		foreach (var f in arr)
		{
			File.Copy (f, f.Replace(".jsc", JSPathSettings.jsExtension), true);
			File.Delete(f);
		}
		AssetDatabase.Refresh ();
		Debug.Log("生成 JSC 完成");
	}


    // 拷贝整个文件夹 proj/Assets/StreamingAssets/JavaScript/ -> proj/Temp/JavaScript_js/
    // 修改文件后缀 .javascript -> .js
    // 只是拷贝和改后缀而已
    static void CopyAndChangeExt()
    {
        string[] lst;

        // 1) 删除 mergedJsDir 下所有文件
        if (Directory.Exists(JSPathSettings.mergedJsDir))
        {
            lst = Directory.GetFiles(JSPathSettings.mergedJsDir, "*.*", SearchOption.AllDirectories);
            foreach (var l in lst)
            {
                File.Delete(l);
            }
        }

        // 2) 拷贝 jsDir -> mergedJsDir
        lst = Directory.GetFiles(JSPathSettings.jsDir, "*.javascript", SearchOption.AllDirectories);
        foreach (var l in lst)
        {
            string dst = l.Replace('\\', '/').Replace(JSPathSettings.jsDir, JSPathSettings.mergedJsDir).Replace(JSPathSettings.jsExtension, ".js");
            Directory.CreateDirectory(Path.GetDirectoryName(dst));
            File.Copy(l, dst, true);
        }

        Debug.Log("MergeAndCopyJavaScript finish");
    }
}
