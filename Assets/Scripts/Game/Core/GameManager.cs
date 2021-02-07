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
    public class GameManager : MonoBehaviour, ICustomStartable, ICustomUpdatable
    {
        public Agent agentPrefab;
        public Entity entityPrefab;
        public AgentSO playerAgentSO;
        public AgentSO fastAgentSO;
        public AgentSO normalAgentSO;
        public AgentSO slowAgentSO;
        public EntitySO boxEntitySO;
        public TileSO floorTile;
        public TileSO wallTile;

        public ActionProducer actionProducer;
        public ActionConsumer actionConsumer;
        public TurnManager turnManager;
        public Board board;

        public Agent playerAgent { get; private set; }

        public GameManager()
        {
            playerAgent = null;
        }

        private void Awake()
        {
            if (agentPrefab == null) throw new NullReferenceException();
            if (entityPrefab == null) throw new NullReferenceException();
            if (playerAgentSO == null) throw new NullReferenceException();
            if (fastAgentSO == null) throw new NullReferenceException();
            if (normalAgentSO == null) throw new NullReferenceException();
            if (slowAgentSO == null) throw new NullReferenceException();
            if (boxEntitySO == null) throw new NullReferenceException();
            if (floorTile == null) throw new NullReferenceException();
            if (wallTile == null) throw new NullReferenceException();

            if (actionProducer == null) throw new NullReferenceException();
            if (actionConsumer == null) throw new NullReferenceException();
            if (turnManager == null) throw new NullReferenceException();
            if (board == null) throw new NullReferenceException();
        }

        public void CustomStart()
        {
            playerAgent = Spawn(playerAgentSO, new Vector2Int(3, 3), true);
            Spawn(fastAgentSO, new Vector2Int(3, 5));
            Spawn(normalAgentSO, new Vector2Int(4, 5));
            Spawn(normalAgentSO, new Vector2Int(5, 5));
            Spawn(slowAgentSO, new Vector2Int(6, 5));
            Spawn(boxEntitySO, new Vector2Int(3, 7));

            Vector2Int upRight = board.size - Vector2Int.one;
            Vector2Int downRight = new Vector2Int(board.size.x - 1, 0);
            Vector2Int downLeft = Vector2Int.zero;
            Vector2Int upLeft = new Vector2Int(0, board.size.y - 1);
            board.ModifyBoxTerrain(downLeft, upRight, floorTile);
            board.ModifyBoxTerrain(upLeft, upRight + new Vector2Int(0, -1), wallTile);
            board.ModifyBoxTerrain(upRight, downRight + new Vector2Int(-1, 0), wallTile);
            board.ModifyBoxTerrain(downRight, downLeft + new Vector2Int(0, 1), wallTile);
            board.ModifyBoxTerrain(downLeft, upLeft + new Vector2Int(1, 0), wallTile);
        }

        public void CustomUpdate()
        {
            if (!actionConsumer.IsCompleted())
            {
                actionConsumer.CustomUpdate();
            }
            else if (actionProducer.CanProduceTurnActionPack())
            {
                ActionPack turnActionPack = actionProducer.ProduceTurnActionPack();
                actionConsumer.ConsumeTurnActionPack(turnActionPack);
            }
        }

        public bool IsPlayerTurn()
        {
            return actionProducer.IsPlayerTurn() && actionConsumer.IsCompleted();
        }

        public void SetPlayerActionPack(ActionPack actionPack)
        {
            if (!IsPlayerTurn()) throw new IsNotPlayerTurnException();

            actionProducer.SetPlayerActionPack(actionPack);
        }

        private Entity Spawn(EntitySO entitySO, Vector2Int at)
        {
            if (entitySO == null) throw new ArgumentNullException();
            if (!board.IsPositionValid(at)) throw new ArgumentOutOfRangeException();

            Entity entity = Instantiate(entityPrefab, turnManager.transform);
            entity.entitySO = entitySO;
            entity.gameObject.SetActive(true);

            board.Add(entity, at);

            UpdateEntityPosition(entity);

            return entity;
        }

        private Agent Spawn(AgentSO agentSO, Vector2Int at, bool priority)
        {
            if (agentSO == null) throw new ArgumentNullException();
            if (!board.IsPositionValid(at)) throw new ArgumentOutOfRangeException();

            Agent agent = Instantiate(agentPrefab, turnManager.transform);
            agent.agentSO = agentSO;
            agent.GetEntity().entitySO = agentSO;
            agent.gameObject.SetActive(true);

            board.Add(agent.GetEntity(), at);

            turnManager.Add(agent, priority);

            UpdateEntityPosition(agent.GetEntity());

            return agent;
        }

        private Agent Spawn(AgentSO agentSO, Vector2Int at)
        {
            return Spawn(agentSO, at, false);
        }

        private void UpdateEntityPosition(Entity entity)
        {
            Vector2Int position = entity.position;
            float scale = board.scale;
            Vector3 relativePosition = new Vector3(position.x + 0.5f, 0, position.y + 0.5f) * scale;
            Vector3 absolutePosition = board.transform.position + relativePosition;
            entity.transform.position = absolutePosition;
        }
    }
}
