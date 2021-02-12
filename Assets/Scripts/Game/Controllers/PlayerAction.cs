#nullable enable

using UnityEngine;

using Zongband.Utils;

namespace Zongband.Game.Controllers
{
    public class PlayerAction
    {
        public readonly Coordinates coordinates;

        public PlayerAction(Coordinates coordinates)
        {
            this.coordinates = coordinates;
        }
    }
}
