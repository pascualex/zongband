#nullable enable

using UnityEngine;

using Zongband.Game.Turns;
using Zongband.Game.Boards;

namespace Zongband.Game.Actions
{
    public abstract class Action
    {
        public bool IsCompleted { get; protected set; } = false;
        
        private bool hasStarted = false;

        public void Process()
        {
            if (IsCompleted) return;
            if (!hasStarted) 
            {
                IsCompleted = ProcessStart();
                hasStarted = true;
            }
            else IsCompleted = ProcessUpdate();
        }

        protected virtual bool ProcessStart()
        {
            return false;
        }

        protected virtual bool ProcessUpdate()
        {
            return true;
        }

        public class Context
        {
            public readonly TurnManager turnManager;
            public readonly Board board;

            public Context(TurnManager turnManager, Board board)
            {
                this.turnManager = turnManager;
                this.board = board;
            }
        }
    }
}