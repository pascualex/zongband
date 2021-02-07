using UnityEngine;
using System;

namespace Zongband.Game.Actions
{
    public class NullActionPack : ActionPack
    {
        public override void CustomUpdate()
        {
            
        }

        public override bool IsActionAvailable()
        {
            return false;
        }
        
        public override bool IsCompleted()
        {
            return true;
        }

        public override bool AreActionsLeft()
        {
            return false;
        }

        public override GameAction RemoveAction()
        {
            throw new NoActionAvailableException();
        }
    }
}