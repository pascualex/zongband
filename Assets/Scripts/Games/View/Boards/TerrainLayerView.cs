using UnityEngine;
using UnityEngine.Tilemaps;

using Zongband.Games.Core.Boards;

using Tile = Zongband.Utils.Tile;

namespace Zongband.Games.View.Boards
{
    public class TerrainLayerView : ITerrainLayerView<TileBase>
    {
        public readonly Tilemap Tilemap;

        public TerrainLayerView(Tilemap tilemap)
        {
            Tilemap = tilemap;
        }

        public void Modify(Tile at, TileBase terrainTypeVisuals)
        {
            if (Tilemap == null) return;
            var position = new Vector3Int(at.X, at.Y, 0);
            Tilemap.SetTile(position, terrainTypeVisuals);
        }
    }
}
