// // using UnityEngine;
// using UnityEditor;
// using UnityEditor.UIElements;
// using UnityEngine.UIElements;

// using Zongband.Engine.Abilities;

// namespace ZongbandEditor
// {
//     [UnityEditor.CustomEditor(typeof(AbilitySO), true)]
//     public class AbilitySOEditorTK : Editor
//     {
//         public override VisualElement CreateInspectorGUI()
//         {            
//             var uxml = Resources.Load<VisualTreeAsset>("AbilitySOEditorTK");
//             return uxml.CloneTree();
//         }
//     }
// }