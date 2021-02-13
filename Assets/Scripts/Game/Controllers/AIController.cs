#nullable enable

using UnityEngine;

using Zongband.Game.Actions;
using Zongband.Game.Boards;
using Zongband.Game.Entities;
using Zongband.Utils;

namespace Zongband.Game.Controllers
{
    public class AIController : Controller
    {
        public override Action? ProduceAction(Agent agent, Board board)
        {
            return ProduceRandomMovement(agent, board);
        }

        private Action? ProduceRandomMovement(Agent agent, Board board)
        {
            var directions = Location.RandomizedDirections();
            var selectedDirection = Location.Zero;
            foreach (var direction in directions)
            {
                if (board.IsLocationAvailable(agent, direction, true))
                {
                    selectedDirection = direction;
                    break;
                }
            }

            if (selectedDirection == Location.Zero) return new NullAction();

            return new MovementAction(agent, board, selectedDirection, true);
        }
    }
}