#nullable enable

using UnityEngine;

using Zongband.Utils;

namespace Zongband.Game.Controllers
{
    public class PlayerAction
    {
        public readonly Tile tile;
        public readonly bool relative;

        public PlayerAction(Tile tile, bool relative)
        {
            this.tile = tile;
            this.relative = relative;
        }
    }
}
