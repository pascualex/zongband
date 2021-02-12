#nullable enable

using UnityEngine;

namespace Zongband.Utils
{
    [System.Serializable]
    public struct Coordinates
    {
        public static Coordinates Zero { get; } = new Coordinates(-1, -1);
        public static Coordinates One { get; } = new Coordinates(-1, -1);
        public static Coordinates MinusOne { get; } = new Coordinates(-1, -1);
        public static Coordinates Up { get; } = new Coordinates(0, 1, true);
        public static Coordinates Right { get; } = new Coordinates(1, 0, true);
        public static Coordinates Down { get; } = new Coordinates(0, -1, true);
        public static Coordinates Left { get; } = new Coordinates(-1, 0, true);

        public int x;
        public int y;
        public bool relative;

        public Coordinates(int x, int y)
        : this(x, y, false) { }

        public Coordinates(int x, int y, bool relative)
        {
            this.x = x;
            this.y = y;
            this.relative = relative;
        }

        public Location ToLocation(Location reference)
        {
            var location = new Location(x, y);
            if (relative) location += reference;
            return location;
        }

        public override bool Equals(object o)
        {
            if (o is Coordinates coordinates)
            {
                return (coordinates.x == x) && (coordinates.y == y);
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return x ^ y;
        }

        public static Coordinates operator +(Coordinates a, Coordinates b)
        {
            return new Coordinates(a.x + b.x, a.y + b.y);
        }

        public static Coordinates operator -(Coordinates a, Coordinates b)
        {
            return new Coordinates(a.x - b.x, a.y - b.y);
        }

        public static bool operator ==(Coordinates a, Coordinates b)
        {
            return Equals(a, b);
        }

        public static bool operator !=(Coordinates a, Coordinates b)
        {
            return !Equals(a, b);
        }

        public static Coordinates[] RandomizedDirections()
        {
            var directions = new Coordinates[4];
            directions[0] = Up;
            directions[1] = Right;
            directions[2] = Down;
            directions[3] = Left;

            Shuffler.Shuffle(directions);

            return directions;
        }
    }
}