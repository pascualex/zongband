using UnityEngine;
using System;

namespace Zongband.Game.Actions
{
    public abstract class SyncAction : Action
    {
        public override void CustomUpdate()
        {
            throw new NotImplementedException();
        }

        public override bool IsCompleted()
        {
            return true;
        }

        public override bool IsSync()
        {
            return true;
        }
    }
}