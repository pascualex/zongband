using System.Collections.Generic;

using Zongband.Engine.Entities;

namespace  Zongband.Engine.Boards
{
    public interface IReadOnlyTile
    {
        IReadOnlyCollection<Entity> Entities { get; }
        ITileType Type { get; }
    }
}