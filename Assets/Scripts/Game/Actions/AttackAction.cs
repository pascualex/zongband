#nullable enable

using UnityEngine;

using Zongband.Game.Entities;

namespace Zongband.Game.Actions
{
    public class AttackAction : Action
    {
        private readonly Agent attacker;
        private readonly Agent target;
        private readonly int damage;

        public AttackAction(Agent attacker, Agent target, int damage)
        {
            this.attacker = attacker;
            this.target = target;
            this.damage = damage;
        }

        protected override bool ProcessStart()
        {
            target.Damage(damage);
            var tileDirection = target.tile - attacker.tile;
            var direction = tileDirection.ToWorldVector3();
            attacker.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            return true;
        }
    }
}