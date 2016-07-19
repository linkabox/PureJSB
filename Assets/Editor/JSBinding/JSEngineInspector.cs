using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JSEngine))]
public class JSEngineInspector : Editor
{
    public override void OnInspectorGUI()
    {
		serializedObject.Update ();

        SerializedProperty propDebug = serializedObject.FindProperty("debug");
        EditorGUILayout.PropertyField(propDebug);

        // JSEngine je = target as JSEngine;

        if (propDebug.boolValue)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("port"));
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("GCInterval"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("UseJSC"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("showStatistics"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("scrollPos"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("InitLoadScripts"), true);

        EditorGUILayout.HelpBox(@"1. Check 'Debug' to enable js debugging with Firefox.
2. Add 'ErrorHandler' to InitLoadScripts to enable printing calling stack on error.
     It will make the execution a little slower but easier to locate bugs.", MessageType.Info);

        var jsEngine = target as JSEngine;
        if (jsEngine)
        {
            if (GUILayout.Button("Dump JsComMap Info"))
            {
                jsEngine.DumpJsComMapInfo();
            }

            if (jsEngine.showStatistics)
            {
                jsEngine.scrollPos = jsEngine.DrawStatistics(jsEngine.scrollPos, 400f);
                this.Repaint();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}