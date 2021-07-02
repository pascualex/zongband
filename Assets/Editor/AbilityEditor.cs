#nullable enable

using UnityEngine;
using UnityEditor;

using Zongband.Game.Abilities;

namespace ZongbandEditor
{
    [UnityEditor.CustomEditor(typeof(AbilitySO), true)]
    public class AbilitySOEditor : CustomEditor
    {
        public override void OnInspectorGUI()
        {
            var AbilitySO = (AbilitySO)target;

            DrawObject("Script", MonoScript.FromScriptableObject(AbilitySO), false);
            DrawField(nameof(AbilitySO.ManaCost));
            DrawField(nameof(AbilitySO.Effects));
        }
    }
}