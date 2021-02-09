#nullable enable

using UnityEngine;
using System;

namespace Zongband.Game.Actions
{
    public class BasicActionPack : ActionPack
    {
        private readonly Action action;

        public BasicActionPack(Action action)
        {
            this.action = action;
        }

        public override void CustomStart()
        {
            action.CustomStart();
        }

        public override void CustomUpdate()
        {
            action.CustomUpdate();
        }
        
        public override bool IsCompleted()
        {
            return action.IsCompleted();
        }

        public override bool AreGameActionsLeft()
        {
            return false;
        }

        public override GameAction? RemoveGameAction()
        {
            return action.RemoveGameAction();
        }
    }
}