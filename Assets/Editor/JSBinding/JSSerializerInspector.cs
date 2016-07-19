using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JSSerializer), true)]
public class JSSerializerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        //serializedObject.Update();
        base.OnInspectorGUI();
    }
}