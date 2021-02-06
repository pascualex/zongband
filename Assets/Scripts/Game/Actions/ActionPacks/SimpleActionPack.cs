using UnityEngine;
using System;

namespace Zongband.Game.Actions
{
    public class SimpleActionPack : ActionPack
    {
        private Action action;
        private bool isActionAvailable;

        public SimpleActionPack(Action action)
        {
            if (action == null) throw new ArgumentNullException();

            this.action = action;
            isActionAvailable = true;
        }

        public override void CustomUpdate()
        {
            if (action != null) action.CustomUpdate();
        }

        public override bool IsActionAvailable()
        {
            if (action == null) return false;
            if (!action.IsGameAction()) return false;
            return isActionAvailable;
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

        public override GameAction ConsumeAction()
        {
            if (!IsActionAvailable()) throw new NoActionAvailableException();

            isActionAvailable = false;
            return (GameAction)action;
        }
    }
}