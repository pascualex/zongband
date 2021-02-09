#nullable enable

using UnityEngine;
using System;

using Zongband.Game.Entities;

namespace Zongband.Game.Actions
{
    public class MakePlayerAction : Action
    {
        private readonly SpawnAction? spawnAction;
        private bool isCompleted;

        public MakePlayerAction(Agent agent)
        {
            gameAction = new MakePlayerGameAction(agent);
            isCompleted = true;
        }

        public MakePlayerAction(SpawnAction spawnAction)
        {
            this.spawnAction = spawnAction;
            isCompleted = false;
        }

        public override void CustomStart()
        {
            if (gameAction != null) return;

            var entity = InheritEntity();
            if (entity is Agent agent) gameAction = new MakePlayerGameAction(agent);

            isCompleted = true;
        }

        protected override bool IsAnimationCompleted()
        {
            return isCompleted;
        }

        private Entity? InheritEntity()
        {
            if (spawnAction == null) throw new NullReferenceException();
            if (!spawnAction.IsCompleted()) throw new PreviousActionUnfinishedException();

            return spawnAction.Entity;
        }
    }
}