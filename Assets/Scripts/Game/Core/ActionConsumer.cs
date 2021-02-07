using UnityEngine;
using System;
using System.Collections.Generic;

using Zongband.Game.AI;
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

        public void ConsumeAction(GameAction action)
        {
            if (action is PositionAction) ConsumeAction((PositionAction)action);
        }

        public bool IsCompleted()
        {
            return turnActionPack.IsCompleted();
        }

        private void UpdateTurnActionPack()
        {
            while (turnActionPack.IsActionAvailable())
            {
                GameAction action = turnActionPack.RemoveAction();
                ConsumeAction(action);
            }

            if (!turnActionPack.IsCompleted()) turnActionPack.CustomUpdate();
        }

        private void ConsumeAction(PositionAction action)
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
    }
}
