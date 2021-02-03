using UnityEngine;

using Zongband.Utils;

namespace Zongband.Game.Boards
{
    public abstract class Layer
    {
        public Vector2Int size { get; private set; }
        public float scale { get; private set; }

        public Layer(Vector2Int size, float scale)
        {
            this.size = size;
            this.scale = scale;
        }

        public bool IsPositionValid(Vector2Int position)
        {
            return Checker.Range(position, size);
        }
    }
}