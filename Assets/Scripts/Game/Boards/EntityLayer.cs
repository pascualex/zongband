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

        public void Add(Entity entity, Tile at)
        {
            if (!IsTileEmpty(at)) throw new ArgumentOutOfRangeException();

            entity.tile = at;
            entities[at.y][at.x] = entity;
        }

        public void Move(Entity entity, Tile to)
        {
            if (!CheckEntityTile(entity)) throw new NotInTileException(entity);

            Move(entity.tile, to);
        }

        public void Move(Tile from, Tile to)
        {
            if (IsTileEmpty(from)) throw new EmptyTileException(from);
            if (!IsTileEmpty(to)) throw new NotEmptyTileException(to);

            entities[to.y][to.x] = entities[from.y][from.x];
            entities[from.y][from.x] = null;
            entities[to.y][to.x]!.tile = to;
        }

        public void Remove(Entity entity)
        {
            if (!CheckEntityTile(entity)) throw new NotInTileException(entity);

            Remove(entity.tile);
        }

        public void Remove(Tile at)
        {
            if (IsTileEmpty(at)) throw new EmptyTileException(at);

            entities[at.y][at.x]!.removed = true;
            entities[at.y][at.x] = null;
        }

        public bool IsTileEmpty(Tile tile)
        {
            if (!Size.Contains(tile)) throw new ArgumentOutOfRangeException();

            return entities[tile.y][tile.x] == null;
        }

        public bool CheckEntityTile(Entity entity)
        {
            if (!Size.Contains(entity.tile)) throw new ArgumentOutOfRangeException();

            return entities[entity.tile.y][entity.tile.x] == entity;
        }
    }
}
