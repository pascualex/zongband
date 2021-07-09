using Zongband.Engine.Boards;
using Zongband.Engine.Entities;
using Zongband.Utils;

namespace  Zongband.Engine
{
    public interface IGameContent
    {
        Size BoardSize { get; }
        ITileType FloorType { get; }
        ITileType WallType { get; }
        IEntityType PlayerType { get; }
    }
}
