using UnityEngine;

using Zongband.Games.Entities;

namespace Zongband.Games.Boards
{
    public class NotInTileException : TileException
    {
        public NotInTileException(Entity entity)
        : base(entity.Tile) { }
    }
}
