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
            
            Entity entity = gameManager.playerAgent;
            Board board = gameManager.board;

            if (!board.IsDisplacementAvailable(entity, delta)) return;

            MovementAction action = new MovementAction(entity, board, delta);
            actionPack = new BasicActionPack(action);
        }

        private bool AcceptsNewAction()
        {
            if (IsActionPackAvailable()) return false;
            return gameManager.IsPlayerTurn();
        }
    }
}
