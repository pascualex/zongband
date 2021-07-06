using UnityEngine;

namespace Zongband.Utils
{
    public static class Shuffler
    {
        public static void Shuffle<T>(T[] array)
        {
            var n = array.Length;
            for (var i = n - 1; i >= 0; i--)
            {
                var k = Random.Range(0, i + 1);
                var temp = array[i];
                array[i] = array[k];
                array[k] = temp;
            }
        }
    }
}
