// // using UnityEngine;
// using UnityEditor;

// namespace ZongbandEditor
// {
//     public class CustomEditor : Editor
//     {
//         protected void DrawObject(string label, Object obj, bool enabled = true)
//         {
//             GUI.enabled = enabled;
//             EditorGUILayout.ObjectField(label, obj, obj.GetType(), false);
//             GUI.enabled = true;
//         }

//         protected void DrawField(string path, int indent = 0)
//         {
//             EditorGUI.indentLevel += indent;
//             var property = serializedObject.FindProperty(path);
//             if (property != null) EditorGUILayout.PropertyField(property, true);
//             else EditorGUILayout.HelpBox($"\"{path}\" property not found", MessageType.Error);
//             EditorGUI.indentLevel -= indent;
//         }
//     }
// }