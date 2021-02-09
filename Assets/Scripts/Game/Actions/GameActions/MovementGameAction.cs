#nullable enable

using UnityEngine;
using System;

using Zongband.Game.Entities;

namespace Zongband.Game.Actions
{
    public class MovementGameAction : GameAction
    {
        public readonly Entity entity;
        public readonly Vector2Int position;
        public readonly bool relative;

        public MovementGameAction(Entity entity, Vector2Int delta) : this(entity, delta, false)
        {

        }

        public MovementGameAction(Entity entity, Vector2Int position, bool relative)
        {
            this.entity = entity;
            this.position = position;
            this.relative = relative;
        }
    }
}