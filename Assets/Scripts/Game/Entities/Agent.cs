#nullable enable

using UnityEngine;
using System;

namespace Zongband.Game.Entities
{
    public class Agent : Entity
    {
        public bool IsPlayer = false;
        
        public string Name { get; private set; } = "Example name";
        public int TurnCooldown { get; private set; } = 100;
        public int TurnPriority { get; private set; } = 0;
        public int MaxHealth { get; private set; } = 100;
        public int Attack { get; private set; } = 10;
        public bool IsGhost { get; private set; } = false;
        public bool IsRoamer { get; private set; } = true;
        public int Health { get; private set; }

        public Agent()
        {
            Health = MaxHealth;
        }

        public void ApplySO(AgentSO agentSO)
        {
            base.ApplySO(agentSO);

            Name = agentSO.Name;
            TurnCooldown = agentSO.TurnCooldown;
            TurnPriority = agentSO.TurnPriority;
            MaxHealth = agentSO.MaxHealth;
            Attack = agentSO.Attack;
            IsGhost = agentSO.IsGhost;
            IsRoamer = agentSO.IsRoamer;

            Health = MaxHealth;
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
            Health = Mathf.Max(Mathf.Min(Health + amount, MaxHealth), 0);
        }
    }
}
