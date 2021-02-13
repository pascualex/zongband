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
            var directions = Tile.RandomizedDirections();
            var selectedDirection = Tile.Zero;
            foreach (var direction in directions)
            {
                if (board.IsTileAvailable(agent, direction, true))
                {
                    selectedDirection = direction;
                    break;
                }
            }

            if (selectedDirection == Tile.Zero) return new NullAction();

            return new MovementAction(agent, board, selectedDirection, true);
        }
    }
}