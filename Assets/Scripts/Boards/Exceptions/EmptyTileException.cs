using UnityEngine;
using System;

namespace Zongband.Boards
{
    public class EmptyTileException : TileException
    {
        public EmptyTileException(Vector2Int position) : base(position)
        {
            
        }
    }
}
