using UnityEngine;
using System;

using Zongband.Game.Entities;

namespace Zongband.Game.Actions
{
    public class MovementAction
    {
        public Entity entity { get; private set; }
        public Vector2Int movement { get; private set; }
        public bool absolute { get; private set; }

        public MovementAction(Entity entity, Vector2Int to) : this(entity, to, false)
        {

        }

        public MovementAction(Entity entity, Vector2Int movement, bool absolute)
        {
            if (entity == null) throw new ArgumentNullException();
            this.entity = entity;
            this.movement = movement;
            this.absolute = absolute;
        }
    }
}