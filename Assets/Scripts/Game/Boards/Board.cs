#nullable enable

using UnityEngine;
using UnityEngine.Tilemaps;

using Zongband.Game.Entities;
using Zongband.Utils;

namespace Zongband.Game.Boards
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private BoardSO? initialBoardSO;
        [SerializeField] private Tilemap? terrainTilemap;

        public Vector2Int Size { get; private set; } = Vector2Int.zero;
        public float Scale { get; private set; } = 1.0f;

        private readonly EntityLayer agentLayer = new EntityLayer();
        private readonly EntityLayer entityLayer = new EntityLayer();
        private readonly TerrainLayer terrainLayer = new TerrainLayer();

        private void Awake()
        {
            if (initialBoardSO != null) ApplySO(initialBoardSO);
        }

        public void ApplySO(BoardSO boardSO)
        {
            Size = boardSO.size;
            Scale = boardSO.scale;
            agentLayer.ChangeSize(Size);
            entityLayer.ChangeSize(Size);
            terrainLayer.ChangeSize(Size);
        }

        public void Add(Entity entity, Vector2Int at)
        {
            if (!IsPositionAvailable(entity, at)) throw new NotEmptyTileException(at);

            if (entity is Agent) agentLayer.Add(entity, at);
            else entityLayer.Add(entity, at);
        }

        public void Move(Entity entity, Vector2Int to)
        {
            Move(entity, to, false);
        }

        public void Move(Entity entity, Vector2Int to, bool relative)
        {
            if (!IsPositionAvailable(entity, to, relative)) throw new NotEmptyTileException(to);

            if (entity is Agent) agentLayer.Move(entity, to, relative);
            else entityLayer.Move(entity, to, relative);
        }

        public void Remove(Entity entity)
        {
            if (entity is Agent) agentLayer.Remove(entity);
            else entityLayer.Remove(entity);
        }

        public void ModifyTerrain(Vector2Int position, TileSO tile)
        {
            if (!IsPositionAvailable(tile, position)) throw new NotEmptyTileException(position);

            terrainLayer.Modify(position, tile);
            terrainTilemap?.SetTile((Vector3Int)position, tile.tileBase);
        }

        public void ModifyBoxTerrain(Vector2Int from, Vector2Int to, TileSO tile)
        {
            var lower = new Vector2Int(Mathf.Min(from.x, to.x), Mathf.Min(from.y, to.y));
            var higher = new Vector2Int(Mathf.Max(from.x, to.x), Mathf.Max(from.y, to.y));

            for (var i = lower.y; i <= higher.y; i++)
            {
                for (var j = lower.x; j <= higher.x; j++)
                {
                    ModifyTerrain(new Vector2Int(j, i), tile);
                }
            }
        }

        public bool IsPositionValid(Vector2Int position)
        {
            return Checker.Range(position, Size);
        }

        public bool IsPositionEmpty(Vector2Int position)
        {
            if (!agentLayer.IsPositionEmpty(position)) return false;
            if (!entityLayer.IsPositionEmpty(position)) return false;
            return true;
        }
        
        public bool IsPositionAvailable(Entity entity, Vector2Int position)
        {
            return IsPositionAvailable(entity, position, false);
        }

        public bool IsPositionAvailable(Entity entity, Vector2Int position, bool relative)
        {
            if (relative)
            {
                if (!CheckEntityPosition(entity)) throw new NotInTileException(entity);
                position += entity.position;
            }

            if (!IsPositionValid(position)) return false;
            /* Add here special interactions in the future */
            if (!agentLayer.IsPositionEmpty(position)) return false;
            var isGhost = (entity is Agent agent) && agent.IsGhost;
            if (!isGhost && !entityLayer.IsPositionEmpty(position)) return false;
            if (!isGhost && terrainLayer.GetTile(position).BlocksGround) return false;
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

        public bool CheckEntityPosition(Entity entity) {
            if (entity is Agent) return agentLayer.CheckEntityPosition(entity);
            else return entityLayer.CheckEntityPosition(entity);
        }
    }
}