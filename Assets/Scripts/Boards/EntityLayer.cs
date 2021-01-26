using UnityEngine;
using System;

using Zongband.Entities;
using Zongband.Utils;

namespace Zongband.Boards
{    
    public class EntityLayer<EntityT> : Layer where EntityT : Entity
    {
        private EntityT[][] entities;

        public EntityLayer(Vector2Int size, float scale) : base(size, scale)
        {
            entities = new EntityT[size.y][];
            for (int i = 0; i < size.y; i++)
            {
                entities[i] = new EntityT[size.x];
            }
        }

        public void Add(EntityT entity, Vector2Int at)
        {
            if (entity == null) throw new ArgumentNullException();
            if (!IsPositionEmpty(at)) throw new ArgumentOutOfRangeException();

            entities[at.y][at.x] = entity;
            entity.Move(at, scale);
        }

        public void Move(EntityT entity, Vector2Int to)
        {
            if (!CheckEntityPosition(entity)) throw new NotInPositionException();

            Move(entity.position, to);
        }

        public void Move(Vector2Int from, Vector2Int to)
        {
            if (IsPositionEmpty(from)) throw new EmptyCellException();
            if (!IsPositionEmpty(to)) throw new NotEmptyCellException();

            entities[to.y][to.x] = entities[from.y][from.x];
            entities[from.y][from.x] = null;
            entities[to.y][to.x].Move(to, scale);
        }

        public void Displace(EntityT entity, Vector2Int delta)
        {
            if (!CheckEntityPosition(entity)) throw new NotInPositionException();

            Displace(entity.position, delta);
        }

        public void Displace(Vector2Int from, Vector2Int delta)
        {
            Move(from, from + delta);
        }

        public void Remove(EntityT entity)
        {
            if (!CheckEntityPosition(entity)) throw new NotInPositionException();

            Remove(entity.position);
        }

        public void Remove(Vector2Int at)
        {
            if (IsPositionEmpty(at)) throw new EmptyCellException();

            entities[at.y][at.x].Remove();
            entities[at.y][at.x] = null;
        }

        public bool IsPositionValid(Vector2Int position) {
            return Checker.Range(position, size);
        }

        public bool IsPositionEmpty(Vector2Int position)
        {
            if (!IsPositionValid(position)) throw new ArgumentOutOfRangeException();

            return entities[position.y][position.x] == null;
        }
        
        public bool CheckEntityPosition(EntityT entity)
        {
            if (entity == null) throw new ArgumentNullException();
            if (!Checker.Range(entity.position, size)) throw new ArgumentOutOfRangeException();

            return entities[entity.position.y][entity.position.x] == entity;
        }
    }
}
