using UnityEngine;
using System;

using Random = System.Random;

namespace Zongband.Utils
{
    public static class RandomExtension
    {
        public static Coords Range(this Random random, Size size)
        {
            return new Coords(random.Next(0, size.X), random.Next(0, size.Y));
        }
    }
}
