using UnityEngine;
using System.Collections.Generic;

namespace Zongband.Game.Actions
{
    public class SequentialActionPack : ActionPack
    {
        private Queue<ActionPack> actionPacks;

        public SequentialActionPack()
        {
            actionPacks = new Queue<ActionPack>();
        }

        public override void CustomStart()
        {
            if (actionPacks.Count > 0) actionPacks.Peek().CustomStart();
        }

        public override void CustomUpdate()
        {
            if (IsGameActionAvailable()) return;
            if (IsCompleted()) return;

            if (!actionPacks.Peek().IsGameActionAvailable() && !actionPacks.Peek().IsCompleted())
            {
                actionPacks.Peek().CustomUpdate();
            }

            RemoveCompletedActionPacks();
        }

        public void Add(ActionPack actionPack)
        {
            actionPacks.Enqueue(actionPack);
        }

        public void Add(Action action)
        {
            Add(new BasicActionPack(action));
        }

        public override bool IsGameActionAvailable()
        {
            if (actionPacks.Count == 0) return false;
            return actionPacks.Peek().IsGameActionAvailable();
        }

        public override bool IsCompleted()
        {
            return (actionPacks.Count == 0);
        }

        public override bool AreGameActionsLeft()
        {
            foreach (ActionPack actionPack in actionPacks)
            {
                if (actionPack.AreGameActionsLeft()) return true;
            }

            return false;
        }

        public override GameAction RemoveGameAction()
        {
            if (!IsGameActionAvailable()) throw new NoGameActionAvailableException();

            GameAction removedAction = actionPacks.Peek().RemoveGameAction();
            RemoveCompletedActionPacks();
            return removedAction;
        }

        private void RemoveCompletedActionPacks()
        {
            while ((actionPacks.Count > 0) && actionPacks.Peek().IsCompleted())
            {
                actionPacks.Dequeue();
                if (actionPacks.Count > 0) actionPacks.Peek().CustomStart();
            }
        }
    }
}