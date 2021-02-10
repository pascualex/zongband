#nullable enable

using UnityEngine;

using Zongband.Game.Turns;
using Zongband.Game.Boards;
using Zongband.Game.Entities;

namespace Zongband.Game.Actions
{
    public class SpawnAction : Action
    {
        public Entity? Entity { get; private set; }

        private readonly EntitySO entitySO;
        private readonly Board board;
        private readonly TurnManager turnManager;
        private readonly Vector2Int position;
        private readonly bool priority;

        public SpawnAction(EntitySO entitySO, Board board, TurnManager turnManager, Vector2Int position)
        : this(entitySO, board, turnManager, position, false) { }

        public SpawnAction(EntitySO entitySO, Board board, TurnManager turnManager, Vector2Int position, bool priority)
        {
            this.entitySO = entitySO;
            this.board = board;
            this.turnManager = turnManager;
            this.position = position;
            this.priority = priority;
        }

        protected override bool ProcessStart()
        {
            if (entitySO is AgentSO agentSO) Entity = Spawn(agentSO);
            else Entity = Spawn(entitySO);

            if (!AddToBoard(Entity))
            {
                GameObject.Destroy(Entity);
                Entity = null;
                return true;
            }

            if (Entity is Agent agent) AddToTurnManager(agent);
            MoveToSpawnInWorld(Entity);

            return true;
        }

        private Entity Spawn(EntitySO entitySO)
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<Entity>();
            // TODO: Change to turn manager?
            gameObject.transform.SetParent(board.transform);

            var entity = gameObject.GetComponent<Entity>();

            entity.ApplySO(entitySO);

            return entity;
        }

        private Agent Spawn(AgentSO agentSO)
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<Entity>();
            gameObject.AddComponent<Agent>();
            // TODO: Change to turn manager?
            gameObject.transform.SetParent(board.transform);
            
            var agent = gameObject.GetComponent<Agent>();

            agent.ApplySO(agentSO);

            return agent;
        }

        private bool AddToBoard(Entity entity)
        {
            if (!board.IsPositionAvailable(entity, position)) return false;
            board.Add(entity, position);
            return true;
        }

        private void AddToTurnManager(Agent agent)
        {
            turnManager.Add(agent, priority);
        }

        private void MoveToSpawnInWorld(Entity entity)
        {
            entity.transform.position = GetSpawnPosition(entity);
        }

        private Vector3 GetSpawnPosition(Entity entity)
        {
            var position = entity.position;
            var scale = board.Scale;

            var relativePosition = new Vector3(position.x + 0.5f, 0, position.y + 0.5f) * scale;
            var absolutePosition = board.transform.position + relativePosition;

            return absolutePosition;
        }
    }
}