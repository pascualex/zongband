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
        public GameManager gameManager;

        private ActionPack turnActionPack;

        public ActionConsumer()
        {
            turnActionPack = new NullActionPack();
        }

        private void Awake()
        {
            if (gameManager == null) throw new NullReferenceException();
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
            while (actionPack.IsGameActionAvailable())
            {
                GameAction action = actionPack.RemoveGameAction();
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
            return turnActionPack.IsCompleted();
        }

        private void UpdateTurnActionPack()
        {
            while (turnActionPack.IsGameActionAvailable())
            {
                GameAction action = turnActionPack.RemoveGameAction();
                ConsumeGameAction(action);
            }

            if (!turnActionPack.IsCompleted()) turnActionPack.CustomUpdate();
        }

        private void ConsumeGameAction(SpawnGameAction action)
        {
            if (action == null) throw new ArgumentNullException();

            Entity entity = action.entity;
            Vector2Int position = action.position;
            bool priority = action.priority;
            Board board = gameManager.board;
            TurnManager turnManager = gameManager.turnManager;

            if (board.IsPositionAvailable(entity, position))
            {
                board.Add(entity, position);
                if (entity is Agent) turnManager.Add((Agent)entity, priority);
            }
        }

        private void ConsumeGameAction(MovementGameAction action)
        {
            if (action == null) throw new ArgumentNullException();

            Entity entity = action.entity;
            Vector2Int position = action.position;
            bool absolute = action.absolute;
            Board board = gameManager.board;

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
            if (action == null) throw new ArgumentNullException();

            gameManager.playerAgent = action.agent;
        }
    }
}
