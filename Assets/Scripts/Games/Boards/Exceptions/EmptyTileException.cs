using UnityEngine;

using Zongband.Utils;

namespace Zongband.Games.Boards
{
    public class EmptyTileException : TileException
    {
        public EmptyTileException(Tile tile)
        : base(tile) { }
    }
}
