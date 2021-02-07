using UnityEngine;
using System;

using Zongband.Game.Entities;

namespace Zongband.Game.Actions
{
    public class MakePlayerAction : Action
    {
        private SpawnAction spawnAction;
        private bool isCompleted;

        public MakePlayerAction(Agent agent)
        {
            if (agent == null) throw new ArgumentNullException();

            this.gameAction = new MakePlayerGameAction(agent);
            isCompleted = true;
        }

        public MakePlayerAction(SpawnAction spawnAction)
        {
            if (spawnAction == null) throw new ArgumentNullException();

            this.spawnAction = spawnAction;
            isCompleted = false;
        }

        public override void CustomStart()
        {
            if (gameAction != null) return;

            Entity entity = InheritEntity();
            if (entity is Agent) gameAction = new MakePlayerGameAction((Agent)entity);

            isCompleted = true;
        }

        protected override bool IsAnimationCompleted()
        {
            return isCompleted;
        }

        private Entity InheritEntity()
        {
            if (!spawnAction.IsCompleted()) throw new PreviousActionUnfinishedException();

            return spawnAction.entity;
        }
    }
}