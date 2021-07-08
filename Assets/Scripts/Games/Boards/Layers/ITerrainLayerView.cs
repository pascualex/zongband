using Zongband.Utils;

namespace Zongband.Games.Boards
{
    public interface ITerrainLayerView
    {
        void Modify(Tile at, object terrainTypeVisuals);
    }
}
