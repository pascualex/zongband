#nullable enable

using UnityEngine;
using System.Collections.Generic;

namespace Zongband.Game.Actions
{
    public class ParallelAction : CombinedAction
    {
        private readonly LinkedList<Action> actions = new LinkedList<Action>();

        protected override bool ProcessStart()
        {
            return ProcessAll();
        }

        protected override bool ProcessUpdate()
        {
            return ProcessAll();
        }

        private bool ProcessAll()
        {
            var node = actions.First;
            while (node != null)
            {
                var next = node.Next;
                node.Value.Process();
                if (node.Value.IsCompleted) actions.Remove(node);
                node = next;
            }
            return actions.Count == 0;
        }

        public override void Add(Action action)
        {
            base.Add(action);
            if (!action.IsCompleted) actions.AddLast(action);
        }
    }
}