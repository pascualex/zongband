#nullable enable

using UnityEngine;

using Zongband.Game.Actions;
using Zongband.Game.Boards;
using Zongband.Game.Entities;

namespace Zongband.Game.Controllers
{
    public class PlayerController : Controller
    {
        public PlayerAction? PlayerAction { private get; set; }

        public override Action? ProduceAction(Agent agent, Board board)
        {
            if (PlayerAction != null) return ProduceMovement(PlayerAction, agent, board);
            return null;
        }

        public void Clear()
        {
            PlayerAction = null;
        }

        private Action? ProduceMovement(PlayerAction playerAction, Agent agent, Board board)
        {
            if (!board.AreCoordinatesAvailable(agent, playerAction.coordinates)) return null;

            return new MovementAction(agent, board, playerAction.coordinates);
        }
    }
}
