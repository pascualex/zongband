using Zongband.Games.Boards;
using Zongband.Utils;

namespace Zongband.Games
{
    public interface IGameContent<T>
    {
        Size BoardSize { get; }
        ITerrainType<T> DefaultTerrainType { get; }
    }
}
