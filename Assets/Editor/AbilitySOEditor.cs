#nullable enable

using UnityEngine;
using UnityEditor;

using Zongband.Game.Abilities;

namespace ZongbandEditor
{
    // [UnityEditor.CustomEditor(typeof(AbilitySO), true)]
    public class AbilitySOEditor : CustomEditor
    {
        public override void OnInspectorGUI()
        {
            var abilitySO = (AbilitySO)target;

            DrawObject("Script", MonoScript.FromScriptableObject(abilitySO), false);
            DrawField(nameof(abilitySO.ManaCost));
            DrawField(nameof(abilitySO.Effects));
        }
    }
}