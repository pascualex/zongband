using UnityEngine;
using UnityEngine.Tilemaps;

using Zongband.Game.Entities;
using Zongband.Utils;

namespace Zongband.Game.Boards
{
    public class Board : MonoBehaviour
    {
        public BoardSO boardData;
        public Tilemap terrainTilemap;

        public Vector2Int size { get; private set; }
        public float scale { get; private set; }

        private EntityLayer agentLayer;
        private EntityLayer entityLayer;
        private TerrainLayer terrainLayer;

        private void Awake()
        {
            if (boardData == null) throw new ScriptableObjectMissingException();
            if (terrainTilemap == null) throw new GameObjectMissingException();

            size = boardData.size;
            scale = boardData.scale;

            agentLayer = new EntityLayer(size, scale);
            entityLayer = new EntityLayer(size, scale);
            terrainLayer = new TerrainLayer(size, scale);
        }

        public void Add(Entity entity, Vector2Int at)
        {
            if (!IsPositionAvailable(entity, at)) throw new NotEmptyTileException(at);

            if (entity.IsAgent()) agentLayer.Add(entity, at);
            else entityLayer.Add(entity, at);
            entity.Move(at, scale);
        }

        public void Move(Entity entity, Vector2Int to)
        {
            if (!IsPositionAvailable(entity, to)) throw new NotEmptyTileException(to);

            if (entity.IsAgent()) agentLayer.Move(entity, to);
            else entityLayer.Move(entity, to);
            entity.Move(to, scale);
        }

        public void Displace(Entity entity, Vector2Int delta)
        {
            if (!CheckEntityPosition(entity)) throw new NotInTileException(entity);

            Move(entity, entity.position + delta);
        }

        public void Remove(Entity entity)
        {
            if (entity.IsAgent()) agentLayer.Remove(entity);
            else entityLayer.Remove(entity);
            entity.OnRemove();
        }

        public void ModifyTerrain(Vector2Int position, TileSO tile)
        {
            if (!IsPositionAvailable(tile, position)) throw new NotEmptyTileException(position);

            terrainLayer.Modify(position, tile);
            terrainTilemap.SetTile((Vector3Int)position, tile.tileBase);
        }

        public void ModifyBoxTerrain(Vector2Int from, Vector2Int to, TileSO tile)
        {
            Vector2Int lower = new Vector2Int(Mathf.Min(from.x, to.x), Mathf.Min(from.y, to.y));
            Vector2Int higher = new Vector2Int(Mathf.Max(from.x, to.x), Mathf.Max(from.y, to.y));

            for (int i = lower.y; i <= higher.y; i++)
            {
                for (int j = lower.x; j <= higher.x; j++)
                {
                    ModifyTerrain(new Vector2Int(j, i), tile);
                }
            }
        }

        public bool IsPositionValid(Vector2Int position)
        {
            return Checker.Range(position, size);
        }

        public bool IsPositionEmpty(Vector2Int position)
        {
            if (!agentLayer.IsPositionEmpty(position)) return false;
            if (!entityLayer.IsPositionEmpty(position)) return false;
            return true;
        }

        public bool IsPositionAvailable(Entity entity, Vector2Int position)
        {
            if (!IsPositionValid(position)) return false;
            /* Add here special interactions in the future */
            if (!agentLayer.IsPositionEmpty(position)) return false;
            if (!entityLayer.IsPositionEmpty(position)) return false;
            if (terrainLayer.GetTile(position).blocksGround) return false;
            return true;
        }

        public bool IsPositionAvailable(TileSO tile, Vector2Int position)
        {
            if (!IsPositionValid(position)) return false;
            /* Add here special interactions in the future */
            if (tile.blocksGround && !agentLayer.IsPositionEmpty(position)) return false;
            if (tile.blocksGround && !entityLayer.IsPositionEmpty(position)) return false;
            return true;
        }

        public bool IsDisplacementAvailable(Entity entity, Vector2Int delta)
        {
            if (!CheckEntityPosition(entity)) throw new NotInTileException(entity);

            return IsPositionAvailable(entity, entity.position + delta);
        }

        public bool CheckEntityPosition(Entity entity) {
            if (entity.IsAgent()) return agentLayer.CheckEntityPosition(entity);
            else return entityLayer.CheckEntityPosition(entity);
        }
    }
}