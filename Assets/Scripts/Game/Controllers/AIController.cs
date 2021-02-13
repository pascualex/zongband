#nullable enable

using UnityEngine;

using Zongband.Game.Actions;
using Zongband.Game.Entities;
using Zongband.Utils;

namespace Zongband.Game.Controllers
{
    public class AIController : Controller
    {
        public override Action? ProduceAction(Agent agent, Action.Context context)
        {
            return ProduceRandomMovement(agent, context);
        }

        private Action? ProduceRandomMovement(Agent agent, Action.Context context)
        {
            var directions = Tile.RandomizedDirections();
            var selectedDirection = Tile.Zero;
            foreach (var direction in directions)
            {
                if (context.board.IsTileAvailable(agent, direction, true))
                {
                    selectedDirection = direction;
                    break;
                }
            }

            if (selectedDirection == Tile.Zero) return new NullAction();

            return new MovementAction(agent, selectedDirection, true, context);
        }
    }
}