#nullable enable

using UnityEngine;

using Zongband.Utils;

namespace Zongband.Game.Controllers
{
    public class PlayerAction
    {
        public readonly Location location;
        public readonly bool relative;

        public PlayerAction(Location location, bool relative)
        {
            this.location = location;
            this.relative = relative;
        }
    }
}
