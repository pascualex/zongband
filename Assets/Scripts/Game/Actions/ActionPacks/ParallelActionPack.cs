using UnityEngine;
using System.Collections.Generic;

namespace Zongband.Game.Actions
{
    public class ParallelActionPack : ActionPack
    {
        private LinkedList<ActionPack> actionPacks;
        private Queue<ActionPack> actionPacksAvailable;

        public ParallelActionPack()
        {
            actionPacks = new LinkedList<ActionPack>();
            actionPacksAvailable = new Queue<ActionPack>();
        }

        public override void CustomUpdate()
        {
            if (IsActionAvailable()) return;
            if (IsCompleted()) return;

            LinkedListNode<ActionPack> node = actionPacks.First;
            while (node != null)
            {
                LinkedListNode<ActionPack> next = node.Next;

                if (node.Value.IsCompleted()) actionPacks.Remove(node);
                else if (!node.Value.IsActionAvailable()) node.Value.CustomUpdate();

                if (node.Value.IsActionAvailable())
                {
                    actionPacksAvailable.Enqueue(node.Value);
                    actionPacks.Remove(node);
                }

                node = next;
            }
        }

        public void Add(ActionPack actionPack)
        {
            actionPacks.AddLast(actionPack);
        }

        public void Add(Action action)
        {
            Add(new BasicActionPack(action));
        }

        public override bool IsActionAvailable()
        {
            return (actionPacksAvailable.Count > 0);
        }

        public override bool IsCompleted()
        {
            return (actionPacks.Count == 0) && (actionPacksAvailable.Count == 0);
        }

        public override bool AreActionsLeft()
        {
            foreach (ActionPack actionPack in actionPacks)
            {
                if (actionPack.AreActionsLeft()) return true;
            }

            foreach (ActionPack actionPack in actionPacksAvailable)
            {
                if (actionPack.AreActionsLeft()) return true;
            }

            return false;
        }

        public override GameAction ConsumeAction()
        {
            if (!IsActionAvailable()) throw new NoActionAvailableException();

            GameAction action = actionPacksAvailable.Peek().ConsumeAction();

            if (!actionPacksAvailable.Peek().IsActionAvailable())
            {
                ActionPack actionPack = actionPacksAvailable.Dequeue();
                actionPacks.AddLast(actionPack);
            }

            return action;
        }
    }
}