using UnityEngine;

using Zongband.Utils;

namespace Zongband.Game.Actions
{
    public abstract class Action : ICustomUpdatable
    {
        public abstract void CustomUpdate();
        public abstract bool IsCompleted();
        public abstract bool IsSync();

        public virtual bool IsForGame()
        {
            return false;
        }
    }
}