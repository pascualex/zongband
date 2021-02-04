using UnityEngine;
using System;

namespace Zongband.Game.Entities
{
    [RequireComponent(typeof(Entity))]
    public class Agent : MonoBehaviour
    {
        public AgentSO agentSO;

        private void Awake()
        {
            if (agentSO == null) throw new NullReferenceException();
        }

        public int GetTurnCooldown()
        {
            return agentSO.turnCooldown;
        }

        public int GetTurnPriority()
        {
            return agentSO.turnPriority;
        }

        public Entity GetEntity()
        {
            return GetComponent<Entity>();
        }
    }
}
