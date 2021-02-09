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
        public override ActionPack? GetActionPack(Agent agent, Board board)
        {
            return GenerateRandomMovement(agent, board);
        }

        private ActionPack? GenerateRandomMovement(Agent agent, Board board)
        {
            var directions = Directions.Randomized();
            var selectedDirection = Vector2Int.zero;
            foreach (var direction in directions)
            {
                if (board.IsPositionAvailable(agent, direction, true))
                {
                    selectedDirection = direction;
                    break;
                }
            }

            if (selectedDirection == Vector2Int.zero) return new NullActionPack();

            var action = new MovementAction(agent, board, selectedDirection, true);
            return new BasicActionPack(action);
        }
    }
}