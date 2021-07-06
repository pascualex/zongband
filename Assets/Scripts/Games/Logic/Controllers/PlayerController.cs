// #nullable enable

// using UnityEngine;

// using Zongband.Games.Logic.Abilities;
// using Zongband.Games.Logic.Actions;
// using Zongband.Games.Logic.Entities;

// namespace Zongband.Games.Logic.Controllers
// {
//     public class PlayerController : Controller
//     {
//         public readonly MoveAction.Parameters DefaultMovement;
//         public readonly AbilitySO TestAbilitySO;

//         public PlayerAction? PlayerAction { private get; set; }
//         public bool SkipTurn { private get; set; } = false;

//         public PlayerController(MoveAction.Parameters defaultMovement, AbilitySO testAbilitySO)
//         {
//             DefaultMovement = defaultMovement;
//             TestAbilitySO = testAbilitySO;
//         }

//         public override Action? ProduceAction(Agent agent, Action.Context ctx)
//         {
//             var agentAction = ProduceMovementOrAttack(agent, ctx);
//             if (agentAction == null && SkipTurn) agentAction = new NullAction();

//             Clear();
//             return agentAction;
//         }

//         public void Clear()
//         {
//             PlayerAction = null;
//             SkipTurn = false;
//         }

//         private Action? ProduceMovementOrAttack(Agent agent, Action.Context ctx)
//         {
//             if (PlayerAction == null) return null;

//             var tile = PlayerAction.Tile;
//             var relative = PlayerAction.Relative;
//             var canAttack = PlayerAction.CanAttack;

//             var isTileAvailable = ctx.Board.IsTileAvailable(agent, tile, relative);
//             if (isTileAvailable) return new MoveAction(agent, tile, relative, DefaultMovement, ctx);

//             var targetAgent = ctx.Board.GetAgent(agent, tile, relative);
//             if (canAttack && targetAgent != agent && targetAgent != null)
//                 if (TestAbilitySO is AgentAbilitySO a)
//                     return a.CreateAction(agent, targetAgent, ctx);

//             return null;
//         }
//     }
// }
