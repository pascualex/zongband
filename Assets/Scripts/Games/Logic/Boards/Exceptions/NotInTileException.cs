using UnityEngine;

using Zongband.Games.Logic.Entities;

namespace Zongband.Games.Logic.Boards
{
    public class NotInTileException : TileException
    {
        public NotInTileException(Entity entity)
        : base(entity.Tile) { }
    }
}
