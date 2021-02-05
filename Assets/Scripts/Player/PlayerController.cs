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
            if (!AcceptsNewAction()) return;
            
            Board board = gameManager.board;
            Agent agent = gameManager.playerAgent;

            if (!board.IsDisplacementAvailable(agent.GetEntity(), delta)) return;

            SequentialActionPack newActionPack = new SequentialActionPack();
            newActionPack.Add(new PositionAction(agent.GetEntity(), delta));
            newActionPack.Add(new MovementAnimation(agent.GetEntity(), board));

            actionPack = newActionPack;
        }

        private bool AcceptsNewAction()
        {
            if (ActionPackAvailable()) return false;
            return gameManager.CanSetPlayerActionPack();
        }
    }
}
