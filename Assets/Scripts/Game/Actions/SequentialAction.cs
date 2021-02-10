#nullable enable

using UnityEngine;
using System.Collections.Generic;

namespace Zongband.Game.Actions
{
    public class SequentialAction : CombinedAction
    {
        private readonly Queue<Action> actions = new Queue<Action>();

        protected override bool ProcessStart()
        {
            return ProcessUntilNotCompleted();
        }

        protected override bool ProcessUpdate()
        {
            return ProcessUntilNotCompleted(); 
        }

        private bool ProcessUntilNotCompleted()
        {
            while (actions.Count != 0)
            {
                actions.Peek().Process();
                if (!actions.Peek().IsCompleted) return false;
                actions.Dequeue();
            }
            return true;
        }

        public override void Add(Action action)
        {
            base.Add(action);
            if (!action.IsCompleted) actions.Enqueue(action);
        }
    }
}