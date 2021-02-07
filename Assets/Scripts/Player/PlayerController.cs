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

        public bool IsActionPackAvailable()
        {
            return actionPack != null;
        }

        public ActionPack RemoveActionPack()
        {
            ActionPack removedActionPack = actionPack;
            ClearActionpack();
            return removedActionPack;
        }

        public void ClearActionpack()
        {
            actionPack = null;
        }

        public void AttemptDisplacement(Vector2Int delta)
        {
            if (!AcceptsNewAction()) return;
            
            Board board = gameManager.board;
            Entity entity = gameManager.playerAgent.GetEntity();

            if (!board.IsDisplacementAvailable(entity, delta)) return;

            MovementGameAction gameAction = new MovementGameAction(entity, delta);
            MovementAction action = new MovementAction(gameAction, board, false);
            actionPack = new BasicActionPack(action);
        }

        private bool AcceptsNewAction()
        {
            if (IsActionPackAvailable()) return false;
            return gameManager.IsPlayerTurn();
        }
    }
}
