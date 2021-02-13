#nullable enable

using UnityEngine;

using Zongband.Game.Entities;

namespace Zongband.Game.Actions
{
    public class AttackAction : Action
    {
        private readonly Agent agent;
        private readonly int damage;

        public AttackAction(Agent agent, int damage)
        {
            this.agent = agent;
            this.damage = damage;
        }

        protected override bool ProcessStart()
        {
            agent.Damage(damage);
            return true;
        }
    }
}