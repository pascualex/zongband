#nullable enable

using UnityEngine;
using System;
using System.Collections.Generic;

using Zongband.Game.Actions;
using Zongband.Game.Entities;

namespace Zongband.Game.Core
{
    public class ActionProducer : MonoBehaviour
    {
        [SerializeField] private GameManager? gameManager;
        [SerializeField] private ActionConsumer? actionConsumer;
        private ActionPack? playerActionPack;

        public ActionProducer(GameManager gameManager, ActionConsumer actionConsumer)
        {
            this.gameManager = gameManager;
            this.actionConsumer = actionConsumer;
        }

        public ActionPack ProduceTurnActionPack()
        {
            throw new CannotProduceActionPackException();
            /*if (!CanProduceTurnActionPack()) throw new CannotProduceActionPackException();
            if (gameManager == null) return;

            var turnActionPack = new ParallelActionPack();
            var processedAgents = new HashSet<Agent>();
            do
            {
                var agent = gameManager.turnManager.GetCurrent();

                if (processedAgents.Contains(agent)) break;
                
                var actionPack = RemovePlayerActionPack();
                // if (IsPlayerTurn()) actionPack = RemovePlayerActionPack();
                // TODO: else actionPack = AgentAI.GenerateActionPack(agent, gameManager.board);

                actionConsumer.TryToConsumeActionPack(actionPack);

                if (!actionPack.IsCompleted()) turnActionPack.Add(actionPack);

                processedAgents.Add(agent);
                gameManager.turnManager.Next();

                if (actionPack.AreGameActionsLeft()) break;
            }
            while (!IsPlayerTurn());

            return turnActionPack;*/
        }

        public bool CanProduceTurnActionPack()
        {
            if (!IsPlayerTurn()) return true;
            return playerActionPack != null;
        }

        public void SetPlayerActionPack(ActionPack actionPack)
        {
            playerActionPack = actionPack;
        }

        public bool IsPlayerTurn()
        {
            throw new NullReferenceException();
            /*if (gameManager.PlayerAgent == null) throw new NullReferenceException();

            return gameManager.turnManager.GetCurrent() == gameManager.PlayerAgent;*/
        }

        private ActionPack RemovePlayerActionPack()
        {
            if (playerActionPack == null) throw new NullReferenceException();
            var removedPlayerActionPack = playerActionPack;
            playerActionPack = null;
            return removedPlayerActionPack;
        }
    }
}
