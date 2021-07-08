using NUnit.Framework;

namespace ZongbandTests.Utils
{
    public static class ObjectExtension
    {
        public static T FailIfNull<T>(this T? obj)
        {
            return obj ?? throw new AssertionException("Expected: not null\n  But was:  null");
        }
    }
}
