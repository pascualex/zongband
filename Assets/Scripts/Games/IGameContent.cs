using Zongband.Games.Boards;
using Zongband.Games.Entities;
using Zongband.Utils;

namespace Zongband.Games
{
    public interface IGameContent
    {
        Size BoardSize { get; }
        ITerrainType DefaultTerrainType { get; }
        IEntityType PlayerEntityType { get; }
    }
}
