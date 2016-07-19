using UnityEngine;

public class JSPathSettings
{
    // extension (including ".")
    public const string jsExtension = ".bytes";
    // directory to save js files
    public static string jsDir = Application.dataPath + "/JavaScript";
    public static string jscDir = Application.dataPath + "/JavaScript_Jsc";
    public static string mergedJsDir = Application.dataPath + "/../Temp/JavaScript_js";

    public static string csGeneratedDir = Application.dataPath + "/Standard Assets/JSBinding/Generated";
    public static string Mono2JsComConfig = jsDir + "/Mono2JsComConfig.json";
    public static string JsTypeInfoConfig = jsDir + "/JsTypeInfoConfig.json";


    // directory to save generated js files (gen by JSGenerateor2)
    // a file to save generated js file names
    public static string csExportJsFile = jsDir + "/CSExportTypes" + jsExtension;
}