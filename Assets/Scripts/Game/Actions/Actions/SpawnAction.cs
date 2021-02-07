using UnityEngine;
using System;

using Zongband.Game.Boards;
using Zongband.Game.Entities;

namespace Zongband.Game.Actions
{
    public class SpawnAction : Action
    {
        public Entity entity { get; private set; }

        private EntitySO entitySO;
        private Board board;
        private Vector2Int position;
        private bool priority;
        private bool isCompleted;

        public SpawnAction(EntitySO entitySO, Board board, Vector2Int position)
        : this(entitySO, board, position, false)
        {

        }

        public SpawnAction(EntitySO entitySO, Board board, Vector2Int position, bool priority)
        {
            if (entitySO == null) throw new ArgumentNullException();

            this.entitySO = entitySO;
            this.board = board;
            this.position = position;
            isCompleted = false;
        }

        public override void CustomStart()
        {
            entity = SpawnEntity();
            entity.transform.position = GetSpawnPosition();

            this.gameAction = new SpawnGameAction(entity, position, priority);

            isCompleted = true;
        }

        private Entity SpawnEntity()
        {
            GameObject gameObject = new GameObject();

            if (entitySO is AgentSO) gameObject.AddComponent<Agent>().Setup((AgentSO)entitySO);
            else gameObject.AddComponent<Entity>().Setup(entitySO);

            GameObject spawnedGameObject = GameObject.Instantiate(gameObject, board.transform);

            return spawnedGameObject.GetComponent<Entity>();
        }

        private Vector3 GetSpawnPosition()
        {
            float scale = board.scale;

            Vector3 relativePosition = new Vector3(position.x + 0.5f, 0, position.y + 0.5f) * scale;
            Vector3 absolutePosition = board.transform.position + relativePosition;

            return absolutePosition;
        }
    }
}