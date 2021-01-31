using UnityEngine;


namespace Zongband.Utils
{
    public static class Checker
    {
        public static bool Range(float value, float max)
        {
            return Range(value, 0, max);
        }

        public static bool Range(float value, float min, float max)
        {
            return value >= 0 && value < max;
        }

        public static bool Range(Vector2 value, Vector2 max)
        {
            return Range(value, Vector2.zero, max);
        }

        public static bool Range(Vector2 value, Vector2 min, Vector2 max)
        {
            return Range(value.x, min.x, max.x) && Range(value.y, min.y, max.y);
        }
    }
}
