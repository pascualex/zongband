#nullable enable

using UnityEngine;

namespace Zongband.Utils
{
    public static class Warnings
    {
        public const string missingEntityTransformer =
            "Entity was moved but doesn't have an EntityTransformer attached";

        public static string TileWarning(Vector2Int position) {
            return "In tile at position " + position;
        }
    }
}
