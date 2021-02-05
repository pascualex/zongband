using UnityEngine;

namespace Zongband.Game.Actions
{
    public abstract class AsyncAction : Action
    {
        public override bool IsSync()
        {
            return false;
        }
    }
}