using UnityEngine;

using Zongband.Utils;

namespace Zongband.Game.Actions
{
    public abstract class ActionPack : ICustomUpdatable
    {
        public abstract void CustomUpdate();
        public abstract bool IsActionAvailable();
        public abstract bool IsCompleted();
        public abstract bool AreActionsLeft();
        public abstract GameAction RemoveAction();
    }
}