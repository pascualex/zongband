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
            if (IsGameActionAvailable()) return;
            if (IsCompleted()) return;

            LinkedListNode<ActionPack> node = actionPacks.First;
            while (node != null)
            {
                LinkedListNode<ActionPack> next = node.Next;

                if (node.Value.IsCompleted()) actionPacks.Remove(node);
                else if (!node.Value.IsGameActionAvailable()) node.Value.CustomUpdate();

                if (node.Value.IsGameActionAvailable())
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

        public override bool IsGameActionAvailable()
        {
            return (actionPacksAvailable.Count > 0);
        }

        public override bool IsCompleted()
        {
            return (actionPacks.Count == 0) && (actionPacksAvailable.Count == 0);
        }

        public override bool AreGameActionsLeft()
        {
            foreach (ActionPack actionPack in actionPacks)
            {
                if (actionPack.AreGameActionsLeft()) return true;
            }

            foreach (ActionPack actionPack in actionPacksAvailable)
            {
                if (actionPack.AreGameActionsLeft()) return true;
            }

            return false;
        }

        public override GameAction RemoveGameAction()
        {
            if (!IsGameActionAvailable()) throw new NoGameActionAvailableException();

            GameAction action = actionPacksAvailable.Peek().RemoveGameAction();

            if (!actionPacksAvailable.Peek().IsGameActionAvailable())
            {
                ActionPack actionPack = actionPacksAvailable.Dequeue();
                actionPacks.AddLast(actionPack);
            }

            return action;
        }
    }
}