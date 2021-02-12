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
            var directions = Coordinates.RandomizedDirections();
            var selectedDirection = Coordinates.Zero;
            foreach (var direction in directions)
            {
                if (board.AreCoordinatesAvailable(agent, direction))
                {
                    selectedDirection = direction;
                    break;
                }
            }

            if (selectedDirection == Coordinates.Zero) return new NullAction();

            return new MovementAction(agent, board, selectedDirection);
        }
    }
}