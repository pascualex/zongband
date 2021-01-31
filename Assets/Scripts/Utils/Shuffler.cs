using UnityEngine;

namespace Zongband.Utils
{
    public static class Shuffler
    {
        public static void Shuffle<T>(T[] array)
        {
            int n = array.Length;
            for (int i = n - 1; i >= 0; i--)
            {
                int k = Random.Range(0, i + 1);
                T temp = array[i];
                array[i] = array[k];
                array[k] = temp;
            }
        }
    }
}
