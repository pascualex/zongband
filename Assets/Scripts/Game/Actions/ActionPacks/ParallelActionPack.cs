#nullable enable

using UnityEngine;
using System.Collections.Generic;

namespace Zongband.Game.Actions
{
    public class ParallelActionPack : ActionPack
    {
        private readonly LinkedList<ActionPack> actionPacks;

        public ParallelActionPack()
        {
            actionPacks = new LinkedList<ActionPack>();
        }

        public override void CustomStart()
        {
            var node = actionPacks.First;
            while (node != null)
            {
                var next = node.Next;
                node.Value.CustomStart();
                if (node.Value.IsCompleted()) actionPacks.Remove(node);
                node = next;
            }
        }

        public override void CustomUpdate()
        {
            var node = actionPacks.First;
            while (node != null)
            {
                var next = node.Next;
                node.Value.CustomUpdate();
                if (node.Value.IsCompleted()) actionPacks.Remove(node);
                node = next;
            }
        }

        public void Add(ActionPack actionPack)
        {
            if (actionPack.IsCompleted()) return;
            actionPacks.AddLast(actionPack);
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
            var node = actionPacks.First;
            while (node != null)
            {
                var gameAction = node.Value.RemoveGameAction();
                if (gameAction != null)
                {
                    actionPacks.Remove(node);
                    return gameAction;
                }
                node = node.Next;
            }
            return null;
        }
    }
}