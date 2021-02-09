#nullable enable

using UnityEngine;

namespace Zongband.Utils
{
    public static class Directions
    {
        public static Vector2Int[] Randomized() {
            var directions = new Vector2Int[4];
            directions[0] = Vector2Int.up;
            directions[1] = Vector2Int.right;
            directions[2] = Vector2Int.down;
            directions[3] = Vector2Int.left;
    
            Shuffler.Shuffle(directions);

            return directions;
        }
    }
}
