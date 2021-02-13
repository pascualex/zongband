#nullable enable

using UnityEngine;

using Zongband.Utils;

namespace Zongband.Game.Boards
{
    public class NotEmptyTileException : TileException
    {
        public NotEmptyTileException(Tile tile)
        : base(tile) { }
    }
}
