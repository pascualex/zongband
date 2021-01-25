using UnityEngine;
using System;

using Zongband.Entities;
using Zongband.Utils;

namespace Zongband.Boards
{
    public class Board : MonoBehaviour
    {
        public BoardSO boardData;

        public Vector2Int size { get; private set; }
        public float scale { get; private set; }

        private Entity[][] entities;

        private void Awake()
        {
            if (boardData == null) throw new ScriptableObjectMissingException();

            size = boardData.size;
            scale = boardData.scale;

            entities = new Entity[size.y][];
            for (int i = 0; i < size.y; i++)
            {
                entities[i] = new Entity[size.x];
            }
        }

        public void AddEntity(Entity entity, Vector2Int at)
        {
            if (entity == null) throw new ArgumentNullException();
            if (!IsPositionEmpty(at)) throw new ArgumentOutOfRangeException();

            entities[at.y][at.x] = entity;
            entity.Move(at, scale);
        }

        public void MoveEntity(Entity entity, Vector2Int to)
        {
            if (!CheckEntityPosition(entity)) throw new NotInPositionException();

            MoveEntity(entity.position, to);
        }

        public void MoveEntity(Vector2Int from, Vector2Int to)
        {
            if (IsPositionEmpty(from)) throw new EmptyCellException();
            if (!IsPositionEmpty(to)) throw new NotEmptyCellException();

            entities[to.y][to.x] = entities[from.y][from.x];
            entities[from.y][from.x] = null;
            entities[to.y][to.x].Move(to, scale);
        }

        public void DisplaceEntity(Entity entity, Vector2Int delta)
        {
            if (!CheckEntityPosition(entity)) throw new NotInPositionException();

            DisplaceEntity(entity.position, delta);
        }

        public void DisplaceEntity(Vector2Int from, Vector2Int delta)
        {
            MoveEntity(from, from + delta);
        }

        public void RemoveEntity(Entity entity)
        {
            if (!CheckEntityPosition(entity)) throw new NotInPositionException();

            RemoveEntity(entity.position);
        }

        public void RemoveEntity(Vector2Int at)
        {
            if (IsPositionEmpty(at)) throw new EmptyCellException();

            entities[at.y][at.x].Remove();
            entities[at.y][at.x] = null;
        }

        public bool IsPositionAvailable(Entity entity, Vector2Int delta)
        {
            if (entity == null) throw new ArgumentNullException();

            return IsPositionAvailable(entity.position + delta);
        }

        public bool IsPositionAvailable(Vector2Int position)
        {
            return Checker.Range(position, size) && IsPositionEmpty(position);
        }

        private bool IsPositionEmpty(Vector2Int position)
        {
            if (!Checker.Range(position, size)) throw new ArgumentOutOfRangeException();

            return entities[position.y][position.x] == null;
        }

        private bool CheckEntityPosition(Entity entity)
        {
            if (entity == null) throw new ArgumentNullException();
            if (!Checker.Range(entity.position, size)) throw new ArgumentOutOfRangeException();

            return entities[entity.position.y][entity.position.x] == entity;
        }
    }
}
