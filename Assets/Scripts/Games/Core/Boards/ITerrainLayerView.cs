#nullable enable

using Zongband.Utils;

namespace Zongband.Games.Core.Boards
{
    public interface ITerrainLayerView<T>
    {
        void Modify(Tile at, T terrainTypeVisuals);
    }
}
