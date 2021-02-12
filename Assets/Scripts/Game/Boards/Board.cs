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
            if (!IsLocationAvailable(entity, at)) throw new NotEmptyTileException(at);

            if (entity is Agent) agentLayer.Add(entity, at);
            else entityLayer.Add(entity, at);
        }

        public void Move(Entity entity, Coordinates to)
        {
            var location = to.ToLocation(entity.location);

            if (!IsLocationAvailable(entity, location)) throw new NotEmptyTileException(location);

            if (entity is Agent) agentLayer.Move(entity, location);
            else entityLayer.Move(entity, location);
        }

        public void Remove(Entity entity)
        {
            if (entity is Agent) agentLayer.Remove(entity);
            else entityLayer.Remove(entity);
        }

        public void ModifyTerrain(Location at, TileSO tile)
        {
            if (!IsLocationAvailable(tile, at)) throw new NotEmptyTileException(at);

            terrainLayer.Modify(at, tile);
            terrainTilemap?.SetTile(at.ToVector3Int(), tile.tileBase);
        }

        public void ModifyBoxTerrain(Location from, Location to, TileSO tile)
        {
            var lower = new Location(Mathf.Min(from.x, to.x), Mathf.Min(from.y, to.y));
            var higher = new Location(Mathf.Max(from.x, to.x), Mathf.Max(from.y, to.y));
            for (var i = lower.y; i <= higher.y; i++)
            {
                for (var j = lower.x; j <= higher.x; j++)
                {
                    ModifyTerrain(new Location(j, i), tile);
                }
            }
        }

        public bool IsLocationEmpty(Location location)
        {
            if (!agentLayer.IsLocationEmpty(location)) return false;
            if (!entityLayer.IsLocationEmpty(location)) return false;
            return true;
        }

        public bool AreCoordinatesAvailable(Entity entity, Coordinates coordinates)
        {
            return IsLocationAvailable(entity, coordinates.ToLocation(entity.location));
        }

        public bool IsLocationAvailable(Entity entity, Location location)
        {
            if (!Size.Contains(location)) return false;
            /* Add here special interactions in the future */
            if (!agentLayer.IsLocationEmpty(location)) return false;
            var isGhost = (entity is Agent agent) && agent.IsGhost;
            if (!isGhost && !entityLayer.IsLocationEmpty(location)) return false;
            if (!isGhost && terrainLayer.GetTile(location).BlocksGround) return false;
            return true;
        }

        public bool IsLocationAvailable(TileSO tile, Location location)
        {
            if (!Size.Contains(location)) return false;
            /* Add here special interactions in the future */
            if (tile.blocksGround && !agentLayer.IsLocationEmpty(location)) return false;
            if (tile.blocksGround && !entityLayer.IsLocationEmpty(location)) return false;
            return true;
        }
    }
}