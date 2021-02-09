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

        public override ActionPack? GetActionPack(Agent agent, Board board)
        {
            if (PlayerAction != null) return AttemptMovement(PlayerAction, agent, board);
            return null;
        }

        private ActionPack? AttemptMovement(PlayerAction playerAction, Agent agent, Board board)
        {
            var position = playerAction.position;
            var relative = playerAction.relative;

            if (!board.IsPositionAvailable(agent, position, relative)) return null;

            var action = new MovementAction(agent, board, position, relative);
            return new BasicActionPack(action);
        }
    }
}
