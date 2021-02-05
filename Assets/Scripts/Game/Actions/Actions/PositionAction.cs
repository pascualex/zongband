using UnityEngine;
using System;

using Zongband.Game.Entities;

namespace Zongband.Game.Actions
{
    public class PositionAction : GameAction
    {
        public Entity entity { get; private set; }
        public Vector2Int position { get; private set; }
        public bool absolute;

        public PositionAction(Entity entity, Vector2Int delta) : this(entity, delta, false)
        {

        }

        public PositionAction(Entity entity, Vector2Int position, bool absolute)
        {
            if (entity == null) throw new ArgumentNullException();
            this.entity = entity;
            this.position = position;
            this.absolute = absolute;
        }
    }
}