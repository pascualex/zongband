using UnityEngine;
using System;

namespace Zongband.Boards
{
    public class NotEmptyTileException : TileException
    {
        public NotEmptyTileException(Vector2Int position) : base(position)
        {

        }
    }
}
