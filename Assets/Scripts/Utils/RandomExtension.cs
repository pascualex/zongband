using UnityEngine;
using System;

using Random = System.Random;

namespace Zongband.Utils
{
    public static class RandomExtension
    {
        public static Tile Range(this Random random, Size size)
        {
            return new Tile(random.Next(0, size.X), random.Next(0, size.Y));
        }
    }
}
