using UnityEngine;

using Zongband.Utils;

namespace Zongband.Games.Logic.Boards
{
    public class NotEmptyTileException : TileException
    {
        public NotEmptyTileException(Tile tile)
        : base(tile) { }
    }
}
