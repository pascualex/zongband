#nullable enable

using UnityEngine;

using Zongband.Game.Turns;
using Zongband.Game.Boards;
using Zongband.Game.Entities;
using Zongband.Utils;

namespace Zongband.Game.Actions
{
    public class SpawnAction : Action
    {
        public Entity? Entity { get; private set; }

        private readonly EntitySO entitySO;
        private readonly Board board;
        private readonly TurnManager turnManager;
        private readonly Location location;
        private readonly bool priority;

        public SpawnAction(EntitySO entitySO, Board board, TurnManager turnManager, Location location)
        : this(entitySO, board, turnManager, location, false) { }

        public SpawnAction(EntitySO entitySO, Board board, TurnManager turnManager, Location location, bool priority)
        {
            this.entitySO = entitySO;
            this.board = board;
            this.turnManager = turnManager;
            this.location = location;
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
            if (!board.IsLocationAvailable(entity, location)) return false;
            board.Add(entity, location);
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
            var location = entity.location;
            var scale = board.Scale;

            var relativePosition = new Vector3(location.x + 0.5f, 0, location.y + 0.5f) * scale;
            var absolutePosition = board.transform.position + relativePosition;

            return absolutePosition;
        }
    }
}