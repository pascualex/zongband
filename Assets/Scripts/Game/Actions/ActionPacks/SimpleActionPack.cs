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
            return isActionAvailable && action.IsGameAction();
        }
        
        public override bool IsCompleted()
        {
            if (action == null) return true;
            return !IsActionAvailable() && action.IsCompleted();
        }

        public override bool AreActionsLeft()
        {
            if (action != null) return false;
            return action.IsGameAction();
        }

        public override GameAction ConsumeAction()
        {
            if (!IsActionAvailable()) throw new NoActionAvailableException();

            isActionAvailable = false;
            return (GameAction)action;
        }
    }
}