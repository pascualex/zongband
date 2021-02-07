using UnityEngine;
using System;

using Zongband.Game.Entities;

namespace Zongband.Game.Actions
{
    public class SpawnGameAction : GameAction
    {
        public Entity entity { get; private set; }
        public Vector2Int position { get; private set; }
        public bool priority { get; private set; }

        public SpawnGameAction(Entity entity, Vector2Int position)
        : this(entity, position, false)
        {

        }

        public SpawnGameAction(Entity entity, Vector2Int position, bool priority)
        {
            if (entity == null) throw new ArgumentNullException();

            this.entity = entity;
            this.position = position;
            this.priority = priority;
        }
    }
}