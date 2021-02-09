#nullable enable

using UnityEngine;

namespace Zongband.Game.Controllers
{
    public class PlayerAction
    {
        public readonly Vector2Int position;
        public readonly bool relative;

        public PlayerAction(Vector2Int position) : this(position, false)
        {

        }

        public PlayerAction(Vector2Int position, bool relative)
        {
            this.position = position;
            this.relative = relative;
        }
    }
}
