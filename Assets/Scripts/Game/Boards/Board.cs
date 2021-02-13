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

        public Size Size { get; private set; } = Size.Zero;
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

        public void Add(Entity entity, Location at)
        {
            if (!IsLocationAvailable(entity, at, false)) throw new NotEmptyTileException(at);

            if (entity is Agent) agentLayer.Add(entity, at);
            else entityLayer.Add(entity, at);
        }

        public void Move(Entity entity, Location to, bool relative)
        {
            if (relative) to += entity.location;

            if (!IsLocationAvailable(entity, to, false)) throw new NotEmptyTileException(to);

            if (entity is Agent) agentLayer.Move(entity, to);
            else entityLayer.Move(entity, to);
        }

        public void Remove(Entity entity)
        {
            if (entity is Agent) agentLayer.Remove(entity);
            else entityLayer.Remove(entity);
        }

        public void ModifyTerrain(Location at, TerrainSO terrainSO)
        {
            if (!IsLocationAvailable(terrainSO, at)) throw new NotEmptyTileException(at);

            terrainLayer.Modify(at, terrainSO);
            terrainTilemap?.SetTile(at.ToVector3Int(), terrainSO.tileBase);
        }

        public void ModifyBoxTerrain(Location from, Location to, TerrainSO terrainSO)
        {
            var lower = new Location(Mathf.Min(from.x, to.x), Mathf.Min(from.y, to.y));
            var higher = new Location(Mathf.Max(from.x, to.x), Mathf.Max(from.y, to.y));
            for (var i = lower.y; i <= higher.y; i++)
            {
                for (var j = lower.x; j <= higher.x; j++)
                {
                    ModifyTerrain(new Location(j, i), terrainSO);
                }
            }
        }

        public bool IsLocationEmpty(Location location)
        {
            if (!agentLayer.IsLocationEmpty(location)) return false;
            if (!entityLayer.IsLocationEmpty(location)) return false;
            return true;
        }

        public bool IsLocationAvailable(Entity entity, Location location, bool relative)
        {
            if (relative) location += entity.location;
            if (!Size.Contains(location)) return false;
            /* Add here special interactions in the future */
            if (!agentLayer.IsLocationEmpty(location)) return false;
            var isGhost = (entity is Agent agent) && agent.IsGhost;
            if (!isGhost && !entityLayer.IsLocationEmpty(location)) return false;
            if (!isGhost && terrainLayer.GetTile(location).BlocksGround) return false;
            return true;
        }

        public bool IsLocationAvailable(TerrainSO terrainSO, Location location)
        {
            if (!Size.Contains(location)) return false;
            /* Add here special interactions in the future */
            if (terrainSO.blocksGround && !agentLayer.IsLocationEmpty(location)) return false;
            if (terrainSO.blocksGround && !entityLayer.IsLocationEmpty(location)) return false;
            return true;
        }
    }
}