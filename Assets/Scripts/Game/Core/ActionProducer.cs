using UnityEngine;
using System;
using System.Collections.Generic;

using Zongband.Game.AI;
using Zongband.Game.Actions;
using Zongband.Game.Entities;

namespace Zongband.Game.Core
{
    public class ActionProducer : MonoBehaviour
    {
        public GameManager gameManager;
        public ActionConsumer actionConsumer;
        public AgentAI agentAI;

        private ActionPack playerActionPack;

        public ActionProducer()
        {
            playerActionPack = null;
        }

        private void Awake()
        {
            if (gameManager == null) throw new NullReferenceException();
            if (actionConsumer == null) throw new NullReferenceException();
            if (agentAI == null) throw new NullReferenceException();
        }

        public ActionPack ProduceTurnActionPack()
        {
            if (!CanProduceTurnActionPack()) throw new CannotProduceActionPackException();

            ParallelActionPack turnActionPack = new ParallelActionPack();

            HashSet<Agent> processedAgents = new HashSet<Agent>();
            do
            {
                Agent agent = gameManager.turnManager.GetCurrent();

                if (processedAgents.Contains(agent)) break;

                ActionPack actionPack;
                if (IsPlayerTurn()) actionPack = RemovePlayerActionPack();
                else actionPack = agentAI.GenerateActionPack(agent, gameManager.board);

                actionConsumer.TryToConsumeActionPack(actionPack);

                if (!actionPack.IsCompleted()) turnActionPack.Add(actionPack);

                processedAgents.Add(agent);
                gameManager.turnManager.Next();

                if (actionPack.AreGameActionsLeft()) break;
            }
            while (!IsPlayerTurn());

            return turnActionPack;
        }

        public bool CanProduceTurnActionPack()
        {
            if (!IsPlayerTurn()) return true;
            return IsPlayerActionPackAvailable();
        }

        public void SetPlayerActionPack(ActionPack actionPack)
        {
            playerActionPack = actionPack;
        }

        public bool IsPlayerTurn()
        {
            if (gameManager.playerAgent == null) throw new NullReferenceException();

            return (gameManager.turnManager.GetCurrent() == gameManager.playerAgent);
        }

        private ActionPack RemovePlayerActionPack()
        {
            if (!IsPlayerActionPackAvailable()) throw new NullReferenceException();
            ActionPack removedPlayerActionPack = playerActionPack;
            playerActionPack = null;
            return removedPlayerActionPack;
        }

        private bool IsPlayerActionPackAvailable()
        {
            return (playerActionPack != null);
        }
    }
}
