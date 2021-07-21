using System;

namespace Zongband.Utils
{
    public static class ObjectExtension
    {
        public static T Value<T>(this T? obj)
        {
            return obj ?? throw new NullReferenceException();
        }
    }
}
