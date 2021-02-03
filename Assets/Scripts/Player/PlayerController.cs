using UnityEngine;
using System;

using Zongband.Game.Core;
using Zongband.Game.Actions;
using Zongband.Game.Boards;
using Zongband.Game.Entities;

namespace Zongband.Player
{
    public class PlayerController : MonoBehaviour
    {
        public GameManager gameManager;

        private ActionPack actionPack;

        private void Awake()
        {
            if (gameManager == null) throw new NullReferenceException();
        }

        public bool ActionPackAvailable()
        {
            return actionPack != null;
        }

        public ActionPack ConsumeActionPack()
        {
            if (actionPack == null) throw new NullReferenceException();

            ActionPack consumedActionPack = actionPack;
            ClearActionPack();
            return consumedActionPack;
        }

        public void ClearActionPack()
        {
            actionPack = null;
        }

        public void AttemptDisplacement(Vector2Int delta)
        {
            if (ActionPackAvailable()) return;
            if (gameManager.playerAgent == null) return;
            
            Board board = gameManager.board;
            Agent agent = gameManager.playerAgent;

            if (!board.IsDisplacementAvailable(agent.GetEntity(), delta)) return;

            ActionPack actionPack = new ActionPack();
            MovementAction movementAction = new MovementAction(agent.GetEntity(), delta);
            actionPack.AddMovementAction(movementAction);

            this.actionPack = actionPack;
        }
    }
}
