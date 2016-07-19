using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using cg;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
using SharpKit.JavaScript;

public static class JSAnalyzer
{
    // delegate return true: not to replace
    public delegate bool DelFilterReplaceFile(string fullpath);

    public enum TraverseOp
    {
        CopyMonoBehaviour,
        RemoveOldBehaviour,
        Analyze
    }

    private static bool CheckHasError;

    /// <summary>
    ///     Exams the component to see if there is something not supported.
    ///     Currently check List only.
    /// </summary>
    /// <param name="com">The component.</param>
    /// <returns>An error list.</returns>
    private static List<string> ExamMonoBehaviour(MonoBehaviour com)
    {
        var lstProblem = new List<string>();
        //StringBuilder sbProblem = new StringBuilder();
        var behaviour = com;
        //Type type = behaviour.GetType();
        var fields = JSSerializerEditor.GetMonoBehaviourSerializedFields(behaviour);
        for (int i = 0; i < fields.Length; i++)
        {
            var field = fields[i];
            var fieldType = field.FieldType;
            if (fieldType.IsArray)
            {
                fieldType = fieldType.GetElementType();
            }
            // ! List is not supported
            // 
            if (fieldType.IsGenericType)
            {
                lstProblem.Add(
                    new StringBuilder().AppendFormat("{0} {1}.{2} serialization not supported.", fieldType.Name,
                        com.GetType().Name, field.Name).ToString());
                //continue;
            }

            // if this MonoBehaviour refer to another MonoBehaviour (A)
            // A must be export or compiled to JavaScript as well
            if (typeof(MonoBehaviour).IsAssignableFrom(fieldType))
            {
                if (!JSSerializerEditor.WillTypeBeAvailableInJavaScript(fieldType))
                {
                    lstProblem.Add(
                        new StringBuilder().AppendFormat("{0} {1}.{2} not available in JavaScript.", fieldType.Name,
                            com.GetType().Name, field.Name).ToString());
                }
            }

            //more to exam
        }
        return lstProblem;
    }

    public static string GetTempFileNameFullPath(string shortPath)
    {
        Directory.CreateDirectory(Application.dataPath + "/Temp/");
        return Application.dataPath + "/Temp/" + shortPath;
    }

    public static string GetAllExportedMembersFile()
    {
        return GetTempFileNameFullPath("AllExportedMembers.txt");
    }

    /// <summary>
    ///     Do some actions to GameObject hierachy.
    /// </summary>
    /// <param name="sbLog">The log.</param>
    /// <param name="go">The gameobject.</param>
    /// <param name="tab">The tab.</param>
    /// <param name="op">The operation.</param>
    public static void TraverseGameObject(StringBuilder sbLog, GameObject go, int tab, TraverseOp op)
    {
        for (int t = 0; t < tab; t++)
        {
            sbLog.Append("    ");
        }
        sbLog.AppendFormat("{0}", go.name);

        bool hasChecked = false;
        bool hasReplaced = false;
        bool hasError = false;

        // disconnect prefab instance !!
        if ((op == TraverseOp.CopyMonoBehaviour || op == TraverseOp.RemoveOldBehaviour)
            && PrefabUtility.GetPrefabType(go) == PrefabType.PrefabInstance)
        {
            PrefabUtility.DisconnectPrefabInstance(go);
        }

        // action!
        switch (op)
        {
            case TraverseOp.CopyMonoBehaviour:
                {
                    bool bReplaced = JSSerializerEditor.CopyGameObject(go);
                    if (bReplaced && !hasReplaced)
                    {
                        hasReplaced = true;
                        sbLog.Append(" (REPLACED)");
                    }
                }
                break;
            case TraverseOp.RemoveOldBehaviour:
                {
                    JSSerializerEditor.RemoveOtherMonoBehaviours(go);
                }
                break;
            case TraverseOp.Analyze:
                {
                    var coms = go.GetComponents(typeof(MonoBehaviour));

                    // Calculate MonoBehaviour's Count
                    // Only check scripts that has JsType attribute
                    var dictMono = new Dictionary<Type, int>();
                    for (int c = 0; c < coms.Length; c++)
                    {
                        var mb = (MonoBehaviour)coms[c];
                        if (mb == null)
                        {
                            CheckHasError = true;
                            Debug.LogError("Null MonoBehaviour found, gameObject name: " + go.name);
                            continue;
                        }

                        if (JSSerializerEditor.WillTypeBeTranslatedToJavaScript(mb.GetType()))
                        {
                            if (!dictMono.ContainsKey(mb.GetType()))
                                dictMono.Add(mb.GetType(), 1);
                            else
                                dictMono[mb.GetType()]++;
                        }
                    }
                    foreach (var t in dictMono)
                    {
                        if (!hasChecked)
                        {
                            hasChecked = true;
                            sbLog.Append(" (CHECKED)");
                        }

                        if (t.Value > 1)
                        {
                            if (!hasError)
                            {
                                hasError = true;
                                sbLog.Append(" ERROR: ");
                            }
                            CheckHasError = true;
                            sbLog.AppendFormat("Same MonoBehaviour more than once. Name: {0}, Count: {1} ", t.Key.Name,
                                t.Value);
                        }
                    }

                    for (int c = 0; c < coms.Length; c++)
                    {
                        var mb = (MonoBehaviour)coms[c];
                        if (mb == null)
                        {
                            continue;
                        }
                        if (JSSerializerEditor.WillTypeBeTranslatedToJavaScript(mb.GetType()))
                        {
                            var lstError = ExamMonoBehaviour(mb);
                            if (lstError.Count > 0)
                                CheckHasError = true;
                            for (int x = 0; x < lstError.Count; x++)
                            {
                                if (!hasError)
                                {
                                    hasError = true;
                                    sbLog.Append(" ERROR: ");
                                }
                                sbLog.Append(lstError[x] + " ");
                            }
                        }
                    }
                }
                break;
            default:
                break;
        }
        sbLog.Append("\n");

        // traverse children
        int childCount = go.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            var child = go.transform.GetChild(i);
            TraverseGameObject(sbLog, child.gameObject, tab + 1, op);
        }
    }

    /// <summary>
    ///     Does file path end with underscore?
    /// </summary>
    /// <param name="path">The file path.</param>
    /// <returns></returns>
    private static bool FileNameBeginsWithUnderscore(string path)
    {
        string shortName = path.Substring(Math.Max(path.LastIndexOf('/'), path.LastIndexOf('\\')) + 1);
        return shortName[0] == '_';
    }

    /// <summary>
    ///     Iterates all scenes and all prefabs in the project.
    ///     Checks all MonoBehaviours who has JsType attribute.
    ///     Save current scene before this action.
    ///     3 things will be checked:
    ///     1) Did you bind a MonoBehaviour(with JsType attribute) to a GameObject twice or more? (Support only one)
    ///     2) Did your MonoBehaviour(with JsType attribute) refer to other MonoBehaviour that is not available in JavaScript?
    ///     3) Did your MonoBehaviour(with JsType attribute) have not-supported public fields? (List, for example)
    /// </summary>
    //[MenuItem("JSB/Check All Monos for all Prefabs and Scenes", false, 112)]

    // 返回值：是否继续下一步
    public static bool CheckAllMonos()
    {
        bool bContinue = EditorUtility.DisplayDialog("WARNING",
            @"1) You mush save current scene before this action.
2) Make proper settings to 'JSBindingSettings.PathsNotToCheckOrReplace' field before this action.
3) Scenes and prefabs whose names begin with '_' will be skipped.",
            "Continue",
            "Cancel");

        if (!bContinue)
        {
            Debug.Log("Operation canceled.");
            return false;
        }

        DelFilterReplaceFile filter = path =>
        {
            // path begins witn Assets/
            string subPath = path.Substring("Assets/".Length);

            // Skip paths in JSBindingSettings.PathsNotToCheckOrReplace
            foreach (string p in JSBindingSettings.PathsNotToCheckOrReplace)
            {
                if (subPath.IndexOf(p) == 0)
                    return true;
            }
            // Skip underscore
            if (FileNameBeginsWithUnderscore(path))
                return true;

            return false;
        };

        var lstPrefabs = GetAllPrefabPaths(filter);
        var lstScenes = GetAllScenePaths(filter);

        var sb = new StringBuilder();
        foreach (string p in lstPrefabs)
        {
            sb.Append(p.Substring("Assets/".Length) + "\r\n");
        }
        foreach (string p in lstScenes)
        {
            sb.Append(p.Substring("Assets/".Length) + "\r\n");
        }
        string fileName = GetTempFileNameFullPath("CheckList.txt");
        File.WriteAllText(fileName, sb.ToString());

        bContinue = EditorUtility.DisplayDialog("TIP",
            "Files list are in " + fileName + ". please verify.",
            "OK",
            "Cancel");

        if (!bContinue)
        {
            Debug.Log("Operation canceled.");
            return false;
        }

        var sbCheckLog = new StringBuilder();
        sbCheckLog.Append(@"// Usage
// search 'ERROR' to see if any error occurs.
// search 'CHECKED' to see whether a GameObject has been checked or not. (a GameObject will be checked if he has a MonoBehaviour with JsType attribute)

");

        CheckHasError = false;
        var ops = new[] { TraverseOp.Analyze };
        foreach (var op in ops)
        {
            // 遍历所有场景
            foreach (string p in lstScenes)
            {
                var sbLog = new StringBuilder();
                Debug.Log("Check Scene: " + p);
                sbLog.AppendFormat("FILE: {0}\n", p);

                EditorApplication.OpenScene(p);
                foreach (var go in SceneRoots())
                {
                    TraverseGameObject(sbLog, go, 1, op);
                }
                EditorApplication.SaveScene();

                sbCheckLog.Append(sbLog + "\n");
            }

            // 遍历所有Prefab
            foreach (string p in lstPrefabs)
            {
                var mainAsset = AssetDatabase.LoadMainAssetAtPath(p);
                if (mainAsset is GameObject)
                {
                    var sbLog = new StringBuilder();
                    Debug.Log("Check Prefab: " + p);
                    sbLog.AppendFormat("FILE: {0}\n", p);

                    TraverseGameObject(sbLog, (GameObject)mainAsset, 1, op);
                    sbCheckLog.Append(sbLog + "\n");
                }
            }
            EditorApplication.SaveAssets();
        }

        fileName = GetTempFileNameFullPath("CheckResult.txt");
        File.WriteAllText(fileName, sbCheckLog.ToString());
        Debug.Log("Check finished. Output file: " + fileName);
        AssetDatabase.Refresh();

        Debug.Log("CheckAllMonos has error: " + (CheckHasError ? "YES" : "NO"));

        return !CheckHasError;
    }


    [MenuItem("JSB/Check and Replace All Monos", false, 162)]
    private static void CheckAndReplaceAllMonos()
    {
        if (CheckAllMonos())
        {
            Debug.Log("Continue ReplaceAllMonos");
            ReplaceAllMonos();
        }
    }

    /// <summary>
    ///     Iterates all scenes and all prefabs in the project.
    ///     Replaces all MonoBehaviours who has JsType attribute with JSComponent!
    ///     Care muse be taken when executing this menu.
    /// </summary>
    //[MenuItem("JSB/Replace All Monos for all Prefabs and Scenes", false, 113)]
    public static void ReplaceAllMonos()
    {
        bool bContinue = EditorUtility.DisplayDialog("WARNING",
            @"1) This action may cause data loss. You better save current scene and backup whole project before executing this action.
2) Make proper settings to 'JSBindingSettings.PathsNotToCheckOrReplace' field before this action.
3) Execute 'JSB | Check All Monos for all Prefabs and Scenes' menu before this action.
4) Scenes and prefabs whose names begin with '_' will be skipped.",
            "Continue",
            "Cancel");

        if (!bContinue)
        {
            Debug.Log("Operation canceled.");
            return;
        }

        DelFilterReplaceFile filter = path =>
        {
            // path begins witn Assets/
            string subPath = path.Substring("Assets/".Length);

            // Skip paths in JSBindingSettings.PathsNotToCheckOrReplace
            foreach (string p in JSBindingSettings.PathsNotToCheckOrReplace)
            {
                if (subPath.IndexOf(p) == 0)
                    return true;
            }
            // Skip underscore
            if (FileNameBeginsWithUnderscore(path))
                return true;

            return false;
        };

        var lstPrefabs = GetAllPrefabPaths(filter);
        var lstScenes = GetAllScenePaths(filter);

        var sb = new StringBuilder();
        foreach (string p in lstPrefabs)
        {
            sb.Append(p.Substring("Assets/".Length) + "\r\n");
        }
        foreach (string p in lstScenes)
        {
            sb.Append(p.Substring("Assets/".Length) + "\r\n");
        }
        string fileName = GetTempFileNameFullPath("FilesToReplace.txt");
        File.WriteAllText(fileName, sb.ToString());
        bContinue = EditorUtility.DisplayDialog("TIP",
            "Files list are in " + fileName + ". please verify.",
            "OK",
            "Cancel");

        if (!bContinue)
        {
            Debug.Log("Operation canceled.");
            return;
        }

        var sbCheckLog = new StringBuilder();
        sbCheckLog.Append(@"// Usage
// search 'REPLACED' to see whether a GameObject has been replace or not.

");

        // first copy
        // then remove
        var ops = new[] { TraverseOp.CopyMonoBehaviour, TraverseOp.RemoveOldBehaviour };
        foreach (var op in ops)
        {
            foreach (string p in lstScenes)
            {
                var sbLog = new StringBuilder();
                sbLog.AppendFormat("FILE: {0}\n", p);

                EditorApplication.OpenScene(p);
                foreach (var go in SceneRoots())
                {
                    TraverseGameObject(sbLog, go, 0, op);
                }
                EditorApplication.SaveScene();
                sbCheckLog.Append(sbLog + "\n");
            }

            foreach (string p in lstPrefabs)
            {
                var sbLog = new StringBuilder();
                sbLog.AppendFormat("FILE: {0}\n", p);

                var mainAsset = AssetDatabase.LoadMainAssetAtPath(p);
                if (mainAsset is GameObject)
                {
                    TraverseGameObject(sbLog, (GameObject)mainAsset, 1, op);
                    sbCheckLog.Append(sbLog + "\n");
                }
            }
            EditorApplication.SaveAssets();
        }
        fileName = GetTempFileNameFullPath("ReplaceResult.txt");
        File.WriteAllText(fileName, sbCheckLog.ToString());
        Debug.Log("Replace finished. Output file: " + fileName);
        AssetDatabase.Refresh();
    }

    public static IEnumerable<GameObject> SceneRoots()
    {
        var prop = new HierarchyProperty(HierarchyType.GameObjects);
        var expanded = new int[0];
        while (prop.Next(expanded))
        {
            yield return prop.pptrValue as GameObject;
        }
    }

    //     [MenuItem("JSB/Test/Output all GameObject names in current scene", false, 1001)]
    //     public static void OutputAllGameObjectNamesInCurrentScene()
    //     {
    //         foreach (var root in SceneRoots())
    //         {
    //             Debug.Log(root.name);
    //         }
    //     }

    /// <summary>
    ///     Iterates all game objects in the scene.
    ///     NOTE !!! this function can ONLY deal with ACTIVE root GameObjects and their children, inactive root GameObjects and
    ///     their children will be omitted!
    /// </summary>
    /// <param name="op">The operation.</param>
    /// <summary>
    ///     Gets all scene paths.
    ///     path begins with 'Assets/'
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    public static List<string> GetAllScenePaths(DelFilterReplaceFile filter)
    {
        var lst = new List<string>();

        var GUIDs = AssetDatabase.FindAssets("t:Scene");
        foreach (string guid in GUIDs)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (filter(path))
            {
                continue;
            }

            lst.Add(path);
        }
        return lst;
    }

    /// <summary>
    ///     Gets all prefab paths.
    ///     path begins with 'Assets/'
    /// </summary>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    public static List<string> GetAllPrefabPaths(DelFilterReplaceFile filter)
    {
        var lst = new List<string>();

        var GUIDs = AssetDatabase.FindAssets("t:Prefab");
        foreach (string guid in GUIDs)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (filter(path))
            {
                continue;
            }

            lst.Add(path);
        }
        return lst;
    }

    // [MenuItem("JSB/Replace MonoBehaviours of all prefabs")]
    //     public static void IterateAllPrefabs(TraverseOp op, DelFilterReplaceFile filter)
    //     {
    //         initAnalyze();
    //         string[] GUIDs = AssetDatabase.FindAssets("t:Prefab");
    //         foreach (var guid in GUIDs)
    //         {
    //             string path = AssetDatabase.GUIDToAssetPath(guid);
    //             if (filter(path))
    //             {
    //                 continue;
    //             }
    // 
    //             UnityEngine.Object mainAsset = AssetDatabase.LoadMainAssetAtPath(path);
    //             if (mainAsset is GameObject)
    //             {
    //                 TraverseGameObject(sbHierachy, (GameObject)mainAsset, 1, op);
    //             }
    //             sbHierachy.Append("\n");
    //         }
    //         Debug.Log(sbHierachy);
    //     }
    // Alt + Shift + Q
    [MenuItem("JSB/Others/Copy Selected GameObjects MonoBehaviours &#q", false, 171)]
    public static void CopyGameObjectMonoBehaviours()
    {
        Debug.Log("CopyGameObjectMonoBehaviours");
        foreach (var go in Selection.gameObjects)
            JSSerializerEditor.CopyGameObject(go);
    }

    // Alt + Shift + W
    [MenuItem("JSB/Others/Remove Selected GameObjects Other MonoBehaviours &#w", false, 172)]
    public static void RemoveOtherMonoBehaviours()
    {
        Debug.Log("RemoveOtherMonoBehaviours");
        foreach (var go in Selection.gameObjects)
            JSSerializerEditor.RemoveOtherMonoBehaviours(go);
    }

    [MenuItem("JSB/Others/Online Documents", false, 174)]
    public static void OpenHelp()
    {
        Application.OpenURL("http://www.cnblogs.com/answerwinner/p/4469021.html");
        // Application.OpenURL("http://www.cnblogs.com/answerwinner/p/4591144.html"); // English
    }
}