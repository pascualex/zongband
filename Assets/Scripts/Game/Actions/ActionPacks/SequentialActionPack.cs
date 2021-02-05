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

        public override void CustomUpdate()
        {
            if (IsActionAvailable()) return;
            if (IsCompleted()) return;

            if (actionPacks.Peek().IsCompleted()) actionPacks.Dequeue();
            else if (!actionPacks.Peek().IsActionAvailable()) actionPacks.Peek().CustomUpdate();
        }

        public void Add(ActionPack actionPack)
        {
            actionPacks.Enqueue(actionPack);
        }

        public void Add(Action action)
        {
            Add(new SimpleActionPack(action));
        }

        public override bool IsActionAvailable()
        {
            if (actionPacks.Count == 0) return false;
            return actionPacks.Peek().IsActionAvailable();
        }

        public override bool IsCompleted()
        {
            return (actionPacks.Count == 0);
        }

        public override bool AreActionsLeft()
        {
            foreach (ActionPack actionPack in actionPacks)
            {
                if (actionPack.AreActionsLeft()) return true;
            }

            return false;
        }

        public override GameAction ConsumeAction()
        {
            if (!IsActionAvailable()) throw new NoActionAvailableException();

            return actionPacks.Peek().ConsumeAction();
        }
    }
}