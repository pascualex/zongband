#nullable enable

using UnityEngine;
using System;

using Zongband.Game.Boards;
using Zongband.Game.Turns;
using Zongband.Game.Actions;
using Zongband.Game.Entities;
using Zongband.Utils;

namespace Zongband.Game.Core
{
    public class ActionConsumer : MonoBehaviour, ICustomUpdatable
    {
        [SerializeField] private GameManager? gameManager;
        private ActionPack turnActionPack;

        public ActionConsumer(GameManager gameManager)
        {
            this.gameManager = gameManager;
            turnActionPack = new NullActionPack();
        }

        public void CustomUpdate()
        {
            UpdateTurnActionPack();
        }

        public void ConsumeTurnActionPack(ActionPack actionPack)
        {
            if (!IsCompleted()) throw new ActionPackNotCompletedException();

            turnActionPack = actionPack;
        }

        public void TryToConsumeActionPack(ActionPack actionPack)
        {
            actionPack.CustomStart();
            GameAction? action;
            while ((action = actionPack.RemoveGameAction()) != null)
            {
                ConsumeGameAction(action);
            }
        }

        public void ConsumeGameAction(GameAction action)
        {
            if (!IsCompleted()) throw new ActionPackNotCompletedException();

            switch (action)
            {
                case SpawnGameAction castedAction:
                    ConsumeGameAction(castedAction);
                    break;
                case MovementGameAction castedAction:
                    ConsumeGameAction(castedAction);
                    break;
                case MakePlayerGameAction castedAction:
                    ConsumeGameAction(castedAction);
                    break;
            }
        }

        public bool IsCompleted()
        {
            return turnActionPack?.IsCompleted() ?? true;
        }

        private void UpdateTurnActionPack()
        {

            GameAction? action;
            while ((action = turnActionPack.RemoveGameAction()) != null)
            {
                ConsumeGameAction(action);
            }

            turnActionPack.CustomUpdate();
        }

        private void ConsumeGameAction(SpawnGameAction action)
        { 
            if (gameManager == null) return;

            var board = gameManager.board;
            if (board == null) return;

            var turnManager = gameManager.turnManager;
            if (turnManager == null) return;

            if (board.IsPositionAvailable(action.entity, action.position))
            {
                board.Add(action.entity, action.position);
                if (action.entity is Agent agent) turnManager.Add(agent, action.priority);
            }
        }

        private void ConsumeGameAction(MovementGameAction action)
        {
            if (gameManager == null) return;

            var entity = action.entity;
            var position = action.position;
            var absolute = action.absolute;

            var board = gameManager.board;
            if (board == null) return;

            if (absolute && board.IsPositionAvailable(entity, position))
            {
                board.Move(entity, position);
            }
            else if (!absolute && board.IsDisplacementAvailable(entity, position))
            {
                board.Displace(entity, position);
            }
        }

        private void ConsumeGameAction(MakePlayerGameAction action)
        {
            if (gameManager == null) return;

            gameManager.PlayerAgent = action.agent;
        }
    }
}
