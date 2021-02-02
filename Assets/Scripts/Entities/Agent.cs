using UnityEngine;
using System;

namespace Zongband.Entities
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

        public Entity GetEntity()
        {
            return GetComponent<Entity>();
        }
    }
}
