#nullable enable

using UnityEngine;

using Zongband.Utils;

namespace Zongband.Game.Boards
{
    public abstract class Layer
    {
        public readonly Vector2Int size = Vector2Int.zero;

        public abstract void ChangeSize(Vector2Int size);

        public bool IsPositionValid(Vector2Int position)
        {
            return Checker.Range(position, size);
        }
    }
}