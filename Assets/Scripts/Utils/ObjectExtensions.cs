using System;

namespace Zongband.Utils
{
    public static class ObjectExtensions
    {
        public static T Value<T>(this T? obj)
        {
            return obj ?? throw new NullReferenceException();
        }
    }
}
