using UnityEngine;

using Zongband.Utils;

namespace Zongband.Game.Actions
{
    public abstract class Action : ICustomUpdatable
    {
        protected GameAction gameAction;

        public virtual void CustomUpdate()
        {

        }

        public bool IsGameActionAvailable()
        {
            return (gameAction != null);
        }

        protected virtual bool IsAnimationCompleted()
        {
            return true;
        }

        public bool IsCompleted()
        {
            return !IsGameActionAvailable() && IsAnimationCompleted();
        }

        public GameAction RemoveGameAction()
        {
            if (!IsGameActionAvailable()) throw new NoGameActionAvailableException();

            GameAction removedGameAction = gameAction;
            gameAction = null;
            return removedGameAction;
        }
    }
}