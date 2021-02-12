#nullable enable

using UnityEngine;

using Zongband.Utils;

namespace Zongband.Game.Boards
{
    public class NotEmptyTileException : TileException
    {
        public NotEmptyTileException(Location location)
        : base(location) { }
    }
}
