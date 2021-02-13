#nullable enable

using UnityEngine;

using Zongband.Game.Actions;
using Zongband.Game.Entities;

namespace Zongband.Game.Controllers
{
    public class PlayerController : Controller
    {
        public PlayerAction? PlayerAction { private get; set; }
        public bool SkipTurn { private get; set; } = false;

        public override Action? ProduceAction(Agent agent, Action.Context context)
        {
            var agentAction = ProduceMovement(agent, context);
            if (agentAction != null) return agentAction;

            if (SkipTurn) return new NullAction();

            return null;
        }

        public void Clear()
        {
            PlayerAction = null;
        }

        private Action? ProduceMovement(Agent agent, Action.Context context)
        {
            if (PlayerAction == null) return null;

            var tile = PlayerAction.tile;
            var relative = PlayerAction.relative;

            if (!context.board.IsTileAvailable(agent, tile, relative)) return null;

            return new MovementAction(agent, tile, relative, context);
        }
    }
}
