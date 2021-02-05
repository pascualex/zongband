using UnityEngine;

using Zongband.Utils;

namespace Zongband.Game.Actions
{
    public abstract class ActionPack : ICustomUpdatable
    {
        public abstract void CustomUpdate();
        public abstract bool IsActionAvailable();
        public abstract GameAction ConsumeAction();
        public abstract bool IsCompleted();
        public abstract bool IsSimple();
    }
}