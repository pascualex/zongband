#nullable enable

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
        [SerializeField] private GameManager? gameManager;

        private ActionPack? actionPack;

        public bool IsActionPackAvailable()
        {
            return actionPack != null;
        }

        public ActionPack? RemoveActionPack()
        {
            var removedActionPack = actionPack;
            ClearActionpack();
            return removedActionPack;
        }

        public void ClearActionpack()
        {
            actionPack = null;
        }

        public void AttemptDisplacement(Vector2Int delta)
        {
            if (gameManager == null) return;
            if (!AcceptsNewAction()) return;
            
            var entity = gameManager.PlayerAgent;
            if (entity == null) return;

            var board = gameManager.board;
            if (board == null) return;

            if (!board.IsDisplacementAvailable(entity, delta)) return;

            var action = new MovementAction(entity, board, delta);
            actionPack = new BasicActionPack(action);
        }

        private bool AcceptsNewAction()
        {
            if (gameManager )
            if (IsActionPackAvailable()) return false;
            return gameManager?.IsPlayerTurn() ?? false;
        }
    }
}
