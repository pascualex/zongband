using UnityEngine;
using System;

using Zongband.Boards;
using Zongband.Actions;
using Zongband.Entities;

namespace Zongband.Player
{
    public class PlayerAgentController : MonoBehaviour
    {
        public Agent agent { get; private set; }
        public Board board { get; private set; }

        private ActionPack actionPack;

        public void Setup(Agent agent, Board board)
        {
            this.agent = agent;
            this.board = board;
            actionPack = null;
        }

        public ActionPack GetActionPack()
        {
            return actionPack;
        }

        public bool ActionPerformed()
        {
            return actionPack != null;
        }

        public void AttemptDisplacement(Vector2Int delta)
        {
            if (agent == null) return;
            if (board == null) return;
            if (ActionPerformed()) return;

            if (!board.IsDisplacementAvailable(agent.GetEntity(), delta)) return;

            ActionPack actionPack = new ActionPack();
            MovementAction movementAction = new MovementAction(agent.GetEntity(), delta);
            actionPack.AddMovementAction(movementAction);

            this.actionPack = actionPack;
        }
    }
}
