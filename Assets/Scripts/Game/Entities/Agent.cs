using UnityEngine;
using System;

namespace Zongband.Game.Entities
{
    [RequireComponent(typeof(Entity))]
    public class Agent : MonoBehaviour
    {
        public AgentSO agentSO;

        private int currentHealth;

        public Agent()
        {
            currentHealth = 0;
        }

        private void Start()
        {
            if (agentSO == null) throw new NullReferenceException();
        }

        public void SetAgentSO(AgentSO agentSO)
        {
            this.agentSO = agentSO;
            currentHealth = GetMaxHealth();
        }

        public int GetTurnCooldown()
        {
            return agentSO.turnCooldown;
        }

        public int GetTurnPriority()
        {
            return agentSO.turnPriority;
        }

        public int GetMaxHealth()
        {
            return agentSO.maxHealth;
        }

        public int GetCurrentHealth()
        {
            return currentHealth;
        }

        public void Damage(int amount)
        {
            if (amount <= 0) return;
            ChangeHealth(-amount);
        }

        public void Heal(int amount)
        {
            if (amount <= 0) return;
            ChangeHealth(amount);
        }

        private void ChangeHealth(int amount)
        {
            currentHealth = Mathf.Max(Mathf.Min(currentHealth + amount, GetMaxHealth()), 0);
        }

        public Entity GetEntity()
        {
            return GetComponent<Entity>();
        }
    }
}
