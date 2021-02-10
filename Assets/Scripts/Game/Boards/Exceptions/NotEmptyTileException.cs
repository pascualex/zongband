#nullable enable

using UnityEngine;

namespace Zongband.Game.Boards
{
    public class NotEmptyTileException : TileException
    {
        public NotEmptyTileException(Vector2Int position)
        : base(position) { }
    }
}
