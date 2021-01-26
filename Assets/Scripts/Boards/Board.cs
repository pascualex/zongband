using UnityEngine;

using Zongband.Entities;

namespace Zongband.Boards {
    public class Board : MonoBehaviour {
        public BoardSO boardData;

        public Vector2Int size { get; private set; }
        public float scale { get; private set; }

        private EntityLayer entityLayer;
        
        private void Awake() {
            if (boardData == null) throw new ScriptableObjectMissingException();

            size = boardData.size;
            scale = boardData.scale;

            entityLayer = new EntityLayer(size, scale);
        }

        public void AddEntity(Entity entity, Vector2Int at)
        {
            entityLayer.AddEntity(entity, at);
        }

        public void MoveEntity(Entity entity, Vector2Int to)
        {
            entityLayer.MoveEntity(entity, to);
        }

        public void MoveEntity(Vector2Int from, Vector2Int to)
        {
            entityLayer.MoveEntity(from, to);
        }

        public void DisplaceEntity(Entity entity, Vector2Int delta)
        {
            entityLayer.DisplaceEntity(entity, delta);
        }

        public void DisplaceEntity(Vector2Int from, Vector2Int delta)
        {
            entityLayer.DisplaceEntity(from, delta);
        }

        public void RemoveEntity(Entity entity)
        {
            entityLayer.RemoveEntity(entity);
        }

        public void RemoveEntity(Vector2Int at)
        {
            entityLayer.RemoveEntity(at);
        }

        public bool IsPositionAvailable(Entity entity, Vector2Int delta)
        {
            return entityLayer.IsPositionAvailable(entity, delta);
        }

        public bool IsPositionAvailable(Vector2Int position)
        {
            return entityLayer.IsPositionAvailable(position);
        }
    }
}