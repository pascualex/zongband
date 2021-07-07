using UnityEngine.Tilemaps;

using Zongband.Games.Boards;

namespace Zongband.View.Boards
{
    public class BoardView : IBoardView<TileBase>
    {
        public ITerrainLayerView<TileBase> TerrainLayerView { get; }

        public BoardView(Tilemap tilemap)
        {
            TerrainLayerView = new TerrainLayerView(tilemap);
        }
    }
}
