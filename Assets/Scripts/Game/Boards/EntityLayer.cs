#nullable enable

using UnityEngine;
using System;

using Zongband.Game.Entities;
using Zongband.Utils;

namespace Zongband.Game.Boards
{
    public class EntityLayer : Layer
    {
        private Entity?[][] entities = new Entity[0][];

        public override void ChangeSize(Size size)
        {
            base.ChangeSize(size);

            foreach (var row in entities)
            {
                foreach (var entity in row)
                {
                    if (entity != null) Remove(entity);
                }
            }

            entities = new Entity[Size.y][];
            for (var i = 0; i < Size.y; i++)
            {
                entities[i] = new Entity[Size.x];
            }
        }

        public void Add(Entity entity, Location at)
        {
            if (!IsLocationEmpty(at)) throw new ArgumentOutOfRangeException();

            entity.location = at;
            entities[at.y][at.x] = entity;
        }

        public void Move(Entity entity, Location to)
        {
            if (!CheckEntityLocation(entity)) throw new NotInTileException(entity);

            Move(entity.location, to);
        }

        public void Move(Location from, Location to)
        {
            if (IsLocationEmpty(from)) throw new EmptyTileException(from);
            if (!IsLocationEmpty(to)) throw new NotEmptyTileException(to);

            entities[to.y][to.x] = entities[from.y][from.x];
            entities[from.y][from.x] = null;
            entities[to.y][to.x]!.location = to;
        }

        public void Remove(Entity entity)
        {
            if (!CheckEntityLocation(entity)) throw new NotInTileException(entity);

            Remove(entity.location);
        }

        public void Remove(Location at)
        {
            if (IsLocationEmpty(at)) throw new EmptyTileException(at);

            entities[at.y][at.x]!.removed = true;
            entities[at.y][at.x] = null;
        }

        public bool IsLocationEmpty(Location location)
        {
            if (!Size.Contains(location)) throw new ArgumentOutOfRangeException();

            return entities[location.y][location.x] == null;
        }

        public bool CheckEntityLocation(Entity entity)
        {
            if (!Size.Contains(entity.location)) throw new ArgumentOutOfRangeException();

            return entities[entity.location.y][entity.location.x] == entity;
        }
    }
}
