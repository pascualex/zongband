#nullable enable

using UnityEngine;
using System;

using Zongband.Utils;

namespace Zongband.Game.Boards
{
    public class TileException : Exception
    {
        public TileException(Vector2Int position) : base(Warnings.TileWarning(position))
        {

        }
    }
}
