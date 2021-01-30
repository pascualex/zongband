using UnityEngine;
using UnityEngine.Tilemaps;

using Zongband.Entities;
using Zongband.Utils;

namespace Zongband.Boards
{
    public class Board : MonoBehaviour
    {
        public BoardSO boardData;
        public Tilemap terrainTilemap;

        public Vector2Int size { get; private set; }
        public float scale { get; private set; }

        private EntityLayer<Agent> agentLayer;
        private EntityLayer<Entity> entityLayer;
        private TerrainLayer terrainLayer;

        private void Awake()
        {
            if (boardData == null) throw new ScriptableObjectMissingException();
            if (terrainTilemap == null) throw new GameObjectMissingException();

            size = boardData.size;
            scale = boardData.scale;

            agentLayer = new EntityLayer<Agent>(size, scale);
            entityLayer = new EntityLayer<Entity>(size, scale);
            terrainLayer = new TerrainLayer(size, scale);
        }

        public void Add(Agent agent, Vector2Int at)
        {
            if (!IsPositionAvailable(agent, at)) throw new NotEmptyTileException(at);

            agentLayer.Add(agent, at);
            agent.Move(at, scale);
        }

        public void Add(Entity entity, Vector2Int at)
        {
            if (!IsPositionAvailable(entity, at)) throw new NotEmptyTileException(at);

            entityLayer.Add(entity, at);
            entity.Move(at, scale);
        }

        public void Move(Agent agent, Vector2Int to)
        {
            if (!IsPositionAvailable(agent, to)) throw new NotEmptyTileException(to);

            agentLayer.Move(agent, to);
            agent.Move(to, scale);
        }

        public void Move(Entity entity, Vector2Int to)
        {
            if (!IsPositionAvailable(entity, to)) throw new NotEmptyTileException(to);

            entityLayer.Move(entity, to);
            entity.Move(to, scale);
        }

        public void Displace(Agent agent, Vector2Int delta)
        {
            if (!agentLayer.CheckEntityPosition(agent)) throw new NotInTileException(agent);

            Move(agent, agent.position + delta);
        }

        public void Displace(Entity entity, Vector2Int delta)
        {
            if (!entityLayer.CheckEntityPosition(entity)) throw new NotInTileException(entity);

            Move(entity, entity.position + delta);
        }

        public void Remove(Agent agent)
        {
            agentLayer.Remove(agent);
            agent.OnRemove();
        }

        public void Remove(Entity entity)
        {
            entityLayer.Remove(entity);
            entity.OnRemove();
        }

        public void ModifyTerrain(Vector2Int position, bool isWall, TileBase tilebase)
        {
            if (!IsPositionAvailable(isWall, position)) throw new NotEmptyTileException(position);

            terrainLayer.Modify(position, isWall);
            terrainTilemap.SetTile((Vector3Int) position, tilebase);
        }

        public void ModifyBoxTerrain(Vector2Int from, Vector2Int to, bool isWall, TileBase tilebase)
        {
            Vector2Int lower = new Vector2Int(Mathf.Min(from.x, to.x), Mathf.Min(from.y, to.y));
            Vector2Int higher = new Vector2Int(Mathf.Max(from.x, to.x), Mathf.Max(from.y, to.y));

            for (int i = lower.y; i <= higher.y; i++) {
                for (int j = lower.x; j <= higher.x; j++) {
                    ModifyTerrain(new Vector2Int(j, i), isWall, tilebase);
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

        public bool IsPositionAvailable(Agent agent, Vector2Int position)
        {
            if (!IsPositionValid(position)) return false;
            if (!agentLayer.IsPositionEmpty(position)) return false;
            /* Add here special interactions in the future */
            if (!entityLayer.IsPositionEmpty(position)) return false;
            if (!terrainLayer.IsPositionEmpty(position)) return false;
            return true;
        }

        public bool IsPositionAvailable(Entity entity, Vector2Int position)
        {
            if (!IsPositionValid(position)) return false;
            if (!entityLayer.IsPositionEmpty(position)) return false;
            /* Add here special interactions in the future */
            if (!agentLayer.IsPositionEmpty(position)) return false;
            if (!terrainLayer.IsPositionEmpty(position)) return false;
            return true;
        }

        public bool IsPositionAvailable(bool isWall, Vector2Int position)
        {
            if (!IsPositionValid(position)) return false;
            /* Add here special interactions in the future */
            if (isWall && !agentLayer.IsPositionEmpty(position)) return false;
            if (isWall && !entityLayer.IsPositionEmpty(position)) return false;
            return true;
        }

        public bool IsDisplacementAvailable(Agent agent, Vector2Int delta)
        {
            if (!agentLayer.CheckEntityPosition(agent)) throw new NotInTileException(agent);

            return IsPositionAvailable(agent, agent.position + delta);
        }

        public bool IsDisplacementAvailable(Entity entity, Vector2Int delta)
        {
            if (!entityLayer.CheckEntityPosition(entity)) throw new NotInTileException(entity);

            return IsPositionAvailable(entity, entity.position + delta);
        }
    }
}