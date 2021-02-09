#nullable enable

using UnityEngine;


namespace Zongband.Utils
{
    public static class Extensions
    {
        public static Vector2Int ToInt(this Vector2 vector2)
        {
            return new Vector2Int((int)vector2.x, (int)vector2.y);
        }
    }
}
