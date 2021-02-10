#nullable enable

using UnityEngine;

using Zongband.Utils;

namespace Zongband.Game.Boards
{
    public abstract class Layer
    {
        public Vector2Int size = Vector2Int.zero;

        public virtual void ChangeSize(Vector2Int size)
        {
            this.size = size;
        }

        public bool IsPositionValid(Vector2Int position)
        {
            return Checker.Range(position, size);
        }
    }
}