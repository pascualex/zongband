using Zongband.Engine.Boards;
using Zongband.Engine.Entities;
using Zongband.Utils;

namespace  Zongband.Engine
{
    public interface IGameContent
    {
        Size BoardSize { get; }
        ITerrain FloorTerrain { get; }
        ITerrain WallTerrain { get; }
        IEntityType PlayerEntityType { get; }
    }
}
