#nullable enable

using UnityEngine;
using System;

using Zongband.Game.Boards;
using Zongband.Game.Entities;

namespace Zongband.Game.Actions
{
    public class SpawnAction : Action
    {
        public Entity? Entity { get; private set; }

        private readonly EntitySO entitySO;
        private readonly Board board;
        private readonly Vector2Int position;
        private readonly bool priority;
        private bool isCompleted;

        public SpawnAction(EntitySO entitySO, Board board, Vector2Int position)
        : this(entitySO, board, position, false)
        {

        }

        public SpawnAction(EntitySO entitySO, Board board, Vector2Int position, bool priority)
        {
            this.entitySO = entitySO;
            this.board = board;
            this.position = position;
            this.priority = priority;
            isCompleted = false;
        }

        public override void CustomStart()
        {
            // GameObject gameObject = new GameObject();
            // TODO: entity.transform.position = GetSpawnPosition();
            // gameAction = new SpawnGameAction(entity, position, priority);
            isCompleted = true;
        }
    }
}