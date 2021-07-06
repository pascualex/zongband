#nullable enable

using Zongband.Utils;

namespace Zongband.Games.Core.Boards
{
    public interface IBoardData<T>
    {
        Size Size { get; }
        ITerrainTypeData<T> DefaultTerrainType { get; }
    }
}
