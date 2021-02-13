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
        public bool SkipTurn { private get; set; } = false;

        public override Action? ProduceAction(Agent agent, Board board)
        {
            if (PlayerAction != null) return ProduceMovement(PlayerAction, agent, board);
            if (SkipTurn) return new NullAction();
            return null;
        }

        public void Clear()
        {
            PlayerAction = null;
        }

        private Action? ProduceMovement(PlayerAction playerAction, Agent agent, Board board)
        {
            var tile = playerAction.tile;
            var relative = playerAction.relative;

            if (!board.IsTileAvailable(agent, tile, relative)) return null;

            return new MovementAction(agent, board, tile, relative);
        }
    }
}
