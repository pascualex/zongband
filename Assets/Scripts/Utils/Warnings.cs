using UnityEngine;

namespace Zongband.Utils
{
    public class Warnings
    {
        public const string missingEntityTransformer =
            "Entity was moved but doesn't have an EntityTransformer attached";

        private Warnings() {

        }

        public static string TileWarning(Vector2Int position) {
            return "In tile at position " + position;
        }
    }
}
