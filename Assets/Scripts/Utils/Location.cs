#nullable enable

using UnityEngine;

namespace Zongband.Utils
{
    public struct Location
    {
        public static Location Zero { get; } = new Location(0, 0);
        public static Location One { get; } = new Location(1, 1);
        public static Location MinusOne { get; } = new Location(-1, -1);

        public int x;
        public int y;

        public Location(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector3Int ToVector3Int()
        {
            return new Vector3Int(x, y, 0);
        }

        public static Location operator +(Location a, Location b)
        {
            return new Location(a.x + b.x, a.y + b.y);
        }
    }
}