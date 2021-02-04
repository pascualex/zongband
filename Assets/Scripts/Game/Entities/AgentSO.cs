using UnityEngine;

namespace Zongband.Game.Entities
{
    [CreateAssetMenu(fileName = "Agent", menuName = "ScriptableObjects/Agent")]
    public class AgentSO : ScriptableObject
    {
        public int turnCooldown = 100;
        public int turnPriority = 0;

        private void OnValidate()
        {
            turnCooldown = Mathf.Max(turnCooldown, 1);
            turnPriority = Mathf.Max(turnPriority, 0);
        }
    }
}
