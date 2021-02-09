#nullable enable

using UnityEngine;
using System;

using Zongband.Game.Entities;

namespace Zongband.Game.Actions
{
    public class SpawnGameAction : GameAction
    {
        public readonly Entity entity;
        public readonly Vector2Int position;
        public readonly bool priority;

        public SpawnGameAction(Entity entity, Vector2Int position)
        : this(entity, position, false)
        {

        }

        public SpawnGameAction(Entity entity, Vector2Int position, bool priority)
        {
            this.entity = entity;
            this.position = position;
            this.priority = priority;
        }
    }
}