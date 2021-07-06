using UnityEngine.Tilemaps;

using Zongband.Games.Core.Boards;

namespace Zongband.Games.View.Boards
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
