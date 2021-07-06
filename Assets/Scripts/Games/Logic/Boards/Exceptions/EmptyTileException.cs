#nullable enable

using UnityEngine;

using Zongband.Utils;

namespace Zongband.Games.Logic.Boards
{
    public class EmptyTileException : TileException
    {
        public EmptyTileException(Tile tile)
        : base(tile) { }
    }
}
