using UnityEngine;

namespace Zongband.Utils
{
    public static class Vector3Extensions
    {
        public static Vector3 LerpDistance(this Vector3 current, Vector3 target, float distance)
        {
            return current + (distance * Vector3.Normalize(target - current));
        }
    }
}
