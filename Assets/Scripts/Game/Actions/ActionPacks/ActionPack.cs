using UnityEngine;

using Zongband.Utils;

namespace Zongband.Game.Actions
{
    public abstract class ActionPack : ICustomStartable, ICustomUpdatable
    {
        public abstract void CustomStart();
        public abstract void CustomUpdate();
        public abstract bool IsGameActionAvailable();
        public abstract bool IsCompleted();
        public abstract bool AreGameActionsLeft();
        public abstract GameAction RemoveGameAction();
    }
}