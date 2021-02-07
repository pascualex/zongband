using UnityEngine;
using System;

namespace Zongband.Game.Actions
{
    public class NullActionPack : ActionPack
    {
        public override void CustomStart()
        {
            
        }

        public override void CustomUpdate()
        {
            
        }

        public override bool IsGameActionAvailable()
        {
            return false;
        }
        
        public override bool IsCompleted()
        {
            return true;
        }

        public override bool AreGameActionsLeft()
        {
            return false;
        }

        public override GameAction RemoveGameAction()
        {
            throw new NoGameActionAvailableException();
        }
    }
}