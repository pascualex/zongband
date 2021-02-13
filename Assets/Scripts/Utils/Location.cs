#nullable enable

using UnityEngine;

namespace Zongband.Utils
{
    public struct Location
    {
        public static Location Zero { get; } = new Location(0, 0);
        public static Location One { get; } = new Location(1, 1);
        public static Location MinusOne { get; } = new Location(-1, -1);
        public static Location Up { get; } = new Location(0, 1);
        public static Location Right { get; } = new Location(1, 0);
        public static Location Down { get; } = new Location(0, -1);
        public static Location Left { get; } = new Location(-1, 0);

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

        public override bool Equals(object o)
        {
            if (o is Location location)
            {
                return (location.x == x) && (location.y == y);
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return x ^ y;
        }

        public static Location operator +(Location a, Location b)
        {
            return new Location(a.x + b.x, a.y + b.y);
        }

        public static Location operator -(Location a, Location b)
        {
            return new Location(a.x - b.x, a.y - b.y);
        }

        public static bool operator ==(Location a, Location b)
        {
            return Equals(a, b);
        }

        public static bool operator !=(Location a, Location b)
        {
            return !Equals(a, b);
        }

        public static Location[] RandomizedDirections()
        {
            var directions = new Location[4];
            directions[0] = Up;
            directions[1] = Right;
            directions[2] = Down;
            directions[3] = Left;

            Shuffler.Shuffle(directions);

            return directions;
        }
    }
}