using UnityEngine;
using UnityEngine.Tilemaps;

using Zongband.Games.Boards;
using Zongband.Utils;

using Tile = Zongband.Utils.Tile;

namespace Zongband.View.Boards
{
    public class TerrainLayerView : ITerrainLayerView
    {
        public readonly Tilemap Tilemap;

        public TerrainLayerView(Tilemap tilemap)
        {
            Tilemap = tilemap;
        }

        public void Modify(Tile at, object terrainTypeVisuals)
        {
            if (terrainTypeVisuals is not TileBase tilebase)
            {
                Debug.Log(Warnings.UnexpectedVisualsObject);
                return;
            }

            var position = new Vector3Int(at.X, at.Y, 0);
            Tilemap.SetTile(position, tilebase);
        }
    }
}
