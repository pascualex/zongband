using UnityEngine;
using System;

namespace Zongband.Game.Actions
{
    public class BasicActionPack : ActionPack
    {
        private Action action;

        public BasicActionPack(Action action)
        {
            if (action == null) throw new ArgumentNullException();

            this.action = action;
        }

        public override void CustomUpdate()
        {
            action.CustomUpdate();
        }

        public override bool IsGameActionAvailable()
        {
            return action.IsGameActionAvailable();
        }
        
        public override bool IsCompleted()
        {
            if (IsGameActionAvailable()) return false;
            return action.IsCompleted();
        }

        public override bool AreGameActionsLeft()
        {
            return IsGameActionAvailable();
        }

        public override GameAction RemoveGameAction()
        {
            if (!IsGameActionAvailable()) throw new NoGameActionAvailableException();

            return action.RemoveGameAction();
        }
    }
}