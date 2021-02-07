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
            if (action != null) action.CustomUpdate();
        }

        public override bool IsActionAvailable()
        {
            if (action == null) return false;
            return action.IsGameAction();
        }
        
        public override bool IsCompleted()
        {
            if (action == null) return true;
            if (IsActionAvailable()) return false;
            return action.IsCompleted();
        }

        public override bool AreActionsLeft()
        {
            return IsActionAvailable();
        }

        public override GameAction RemoveAction()
        {
            if (!IsActionAvailable()) throw new NoActionAvailableException();

            GameAction removedAction = (GameAction)action;
            action = null;
            return removedAction;
        }
    }
}