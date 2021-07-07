using UnityEngine;
using System;

using Zongband.Utils;

namespace Zongband.Games.Boards
{
    public class TileException : Exception
    {
        public TileException(Tile tile)
        : base(Warnings.Tile(tile)) { }
    }
}
