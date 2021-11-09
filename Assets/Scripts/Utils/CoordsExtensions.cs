using RLEngine.Core.Utils;

using UnityEngine;

namespace Zongband.Utils
{
    public static class CoordsExtensions
    {
        public static Vector3Int ToCell(this Coords coords)
        {
            return new Vector3Int(coords.X, coords.Y, 0);
        }
    }
}
