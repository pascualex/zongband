#nullable enable

using UnityEngine;

using Zongband.Utils;

namespace Zongband.Game.Actions
{
    public abstract class Action : ICustomStartable, ICustomUpdatable
    {
        protected GameAction? gameAction;

        public virtual void CustomStart()
        {

        }

        public virtual void CustomUpdate()
        {

        }

        protected virtual bool IsAnimationCompleted()
        {
            return true;
        }

        public bool IsCompleted()
        {
            return (gameAction == null) && IsAnimationCompleted();
        }

        public GameAction? RemoveGameAction()
        {
            return gameAction;
        }
    }
}