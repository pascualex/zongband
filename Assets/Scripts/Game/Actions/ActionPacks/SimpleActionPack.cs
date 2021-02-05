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
            action.CustomUpdate();
        }

        public override bool IsActionAvailable()
        {
            return isActionAvailable && action.IsForGame();
        }

        public override GameAction ConsumeAction()
        {
            if (!IsActionAvailable()) throw new NoActionAvailableException();

            isActionAvailable = false;
            return (GameAction)action;
        }
        
        public override bool IsCompleted()
        {
            return !IsActionAvailable() && action.IsCompleted();
        }

        public override bool IsSimple()
        {
            return true;
        }
    }
}