using UnityEngine;
using System;

namespace Zongband.Game.Entities
{
    public class Agent : Entity
    {
        public AgentSO agentSO;

        private int currentHealth;

        public Agent()
        {
            currentHealth = 0;
        }

        public void Setup(AgentSO agentSO)
        {
            if (agentSO == null) throw new ArgumentNullException();

            base.Setup(agentSO);

            this.agentSO = agentSO;
        }

        public override void Setup(EntitySO entitySO)
        {
            throw new NotImplementedException();
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
    }
}
