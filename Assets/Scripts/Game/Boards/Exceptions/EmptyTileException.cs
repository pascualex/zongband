#nullable enable

using UnityEngine;

using Zongband.Utils;

namespace Zongband.Game.Boards
{
    public class EmptyTileException : TileException
    {
        public EmptyTileException(Tile tile)
        : base(tile) { }
    }
}
