using System;

using Zongband.Games.Logic.Entities;
using Zongband.Utils;

namespace Zongband.Games.Logic.Boards
{
    public class EntityLayer<EntityT> : Layer where EntityT : Entity
    {
        private EntityT?[][] Entities = new EntityT[0][];

        public EntityLayer(Size size) : base(size)
        {
            Entities = new EntityT[Size.Y][];
            for (var i = 0; i < Size.Y; i++) Entities[i] = new EntityT[Size.X];
        }

        public void Add(EntityT entity, Tile at)
        {
            if (!IsTileEmpty(at)) throw new ArgumentOutOfRangeException();

            entity.Tile = at;
            Entities[at.Y][at.X] = entity;
        }

        public void Move(EntityT entity, Tile to)
        {
            if (!CheckEntityTile(entity)) throw new NotInTileException(entity);

            Move(entity.Tile, to);
        }

        public void Move(Tile from, Tile to)
        {
            if (IsTileEmpty(from)) throw new EmptyTileException(from);
            if (!IsTileEmpty(to)) throw new NotEmptyTileException(to);

            Entities[to.Y][to.X] = Entities[from.Y][from.X];
            Entities[from.Y][from.X] = null;
            Entities[to.Y][to.X]!.Tile = to;
        }

        public void Remove(EntityT entity)
        {
            if (!CheckEntityTile(entity)) throw new NotInTileException(entity);

            Remove(entity.Tile);
        }

        public void Remove(Tile at)
        {
            if (IsTileEmpty(at)) throw new EmptyTileException(at);

            Entities[at.Y][at.X] = null;
        }

        public EntityT? Get(Tile at)
        {
            if (!Size.Contains(at)) throw new ArgumentOutOfRangeException();

            return Entities[at.Y][at.X];
        }

        public bool IsTileEmpty(Tile tile)
        {
            if (!Size.Contains(tile)) throw new ArgumentOutOfRangeException();

            return Entities[tile.Y][tile.X] == null;
        }

        public bool CheckEntityTile(EntityT entity)
        {
            if (!Size.Contains(entity.Tile)) throw new ArgumentOutOfRangeException();

            return Entities[entity.Tile.Y][entity.Tile.X] == entity;
        }
    }
}
