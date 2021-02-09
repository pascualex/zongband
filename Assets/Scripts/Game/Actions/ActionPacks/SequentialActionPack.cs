#nullable enable

using UnityEngine;
using System.Collections.Generic;

namespace Zongband.Game.Actions
{
    public class SequentialActionPack : ActionPack
    {
        private readonly Queue<ActionPack> actionPacks;

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
            if (IsCompleted()) return;

            actionPacks.Peek().CustomUpdate();
            RemoveCompletedActionPacks();
        }

        public void Add(ActionPack actionPack)
        {
            if (actionPack.IsCompleted()) return;
            actionPacks.Enqueue(actionPack);
        }

        public void Add(Action action)
        {
            Add(new BasicActionPack(action));
        }

        public override bool IsCompleted()
        {
            return actionPacks.Count == 0;
        }

        public override bool AreGameActionsLeft()
        {
            foreach (var actionPack in actionPacks)
            {
                if (actionPack.AreGameActionsLeft()) return true;
            }

            return false;
        }

        public override GameAction? RemoveGameAction()
        {
            var removedAction = actionPacks.Peek().RemoveGameAction();
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