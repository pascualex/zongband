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
        private readonly Tile tile;
        private readonly Context context;
        private readonly bool priority;

        public SpawnAction(EntitySO entitySO, Tile tile, Context context)
        : this(entitySO, tile, context, false) { }

        public SpawnAction(EntitySO entitySO, Tile tile, Context context, bool priority)
        {
            this.entitySO = entitySO;
            this.context = context;
            this.tile = tile;
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
            gameObject.transform.SetParent(context.board.transform);

            var entity = gameObject.GetComponent<Entity>();

            entity.ApplySO(entitySO);

            return entity;
        }

        private Agent Spawn(AgentSO agentSO)
        {
            var gameObject = new GameObject();
            gameObject.AddComponent<Entity>();
            gameObject.AddComponent<Agent>();
            gameObject.transform.SetParent(context.turnManager.transform);
            
            var agent = gameObject.GetComponent<Agent>();

            agent.ApplySO(agentSO);

            return agent;
        }

        private bool AddToBoard(Entity entity)
        {
            if (!context.board.IsTileAvailable(entity, tile, false)) return false;
            context.board.Add(entity, tile);
            return true;
        }

        private void AddToTurnManager(Agent agent)
        {
            context.turnManager.Add(agent, priority);
        }

        private void MoveToSpawnInWorld(Entity entity)
        {
            entity.transform.position = GetSpawnPosition(entity);
        }

        private Vector3 GetSpawnPosition(Entity entity)
        {
            var tile = entity.tile;
            var scale = context.board.Scale;

            var relativePosition = new Vector3(tile.x + 0.5f, 0, tile.y + 0.5f) * scale;
            var absolutePosition = context.board.transform.position + relativePosition;

            return absolutePosition;
        }
    }
}