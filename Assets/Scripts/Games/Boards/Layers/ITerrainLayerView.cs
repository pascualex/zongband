using Zongband.Utils;

namespace Zongband.Games.Boards
{
    public interface ITerrainLayerView<T>
    {
        void Modify(Tile at, T terrainTypeVisuals);
    }
}
