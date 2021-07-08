using UnityEngine;
using UnityEngine.Tilemaps;

using Zongband.Games.Boards;

namespace Zongband.View.Boards
{
    public class BoardView : IBoardView
    {
        public IEntityLayerView EntityLayerView { get; }
        public ITerrainLayerView TerrainLayerView { get; }

        public BoardView(Tilemap tilemap)
        {
            EntityLayerView = new EntityLayerView(tilemap.transform.position, tilemap.cellSize);
            TerrainLayerView = new TerrainLayerView(tilemap);
        }
    }
}
