#nullable enable

using UnityEngine;

namespace Zongband.Game.Boards
{
    public class EmptyTileException : TileException
    {
        public EmptyTileException(Vector2Int position)
        : base(position) { }
    }
}
