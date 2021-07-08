using System.Collections.Generic;

using Zongband.Engine.Entities;

namespace  Zongband.Engine.Boards
{
    public interface IReadOnlyTile
    {
        ITerrain Terrain { get; }
        IReadOnlyCollection<Entity> Entities { get; }
    }
}