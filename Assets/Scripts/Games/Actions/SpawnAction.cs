// // using UnityEngine;

// using Zongband.Games.Turns;
// using Zongband.Games.Boards;
// using Zongband.Games.Entities;
// using Zongband.Utils;

// namespace Zongband.Games.Actions
// {
//     public class SpawnAction : Action
//     {
//         public Agent Agent { get; private set; }

//         private readonly Tile Tile;
//         private readonly Context Ctx;
//         private readonly bool Priority;

//         public SpawnAction(AgentSO agentSO, Tile tile, Context ctx)
//         : this(agentSO, tile, ctx, false) { }

//         public SpawnAction(AgentSO agentSO, Tile tile, Context ctx, bool priority)
//         {
//             Ctx = ctx;
//             Tile = tile;
//             Priority = priority;

//             Agent = Spawn(agentSO);
//         }

//         protected override bool ExecuteStart()
//         {
//             if (!AddToBoard(Agent))
//             {
//                 GameObject.Destroy(Agent);
//                 return true;
//             }

//             AddToTurnManager(Agent);
//             MoveToSpawn(Agent);
//             Agent.gameObject.SetActive(true);
//             Agent.OnSpawn();

//             return true;
//         }

//         private Agent Spawn(AgentSO agentSO)
//         {
//             // TODO: var parent = Ctx.TurnManager.transform;
//             var agent = GameObject.Instantiate(Ctx.AgentPrefab);
//             agent.ApplySO(agentSO);
//             agent.gameObject.SetActive(false);
//             return agent;
//         }

//         private bool AddToBoard(Agent agent)
//         {
//             if (!Ctx.Board.IsTileAvailable(agent, Tile, false)) return false;
//             Ctx.Board.Add(agent, Tile);
//             return true;
//         }

//         private void AddToTurnManager(Agent agent)
//         {
//             Ctx.TurnManager.Add(agent, Priority);
//         }

//         private void MoveToSpawn(Agent agent)
//         {
//             var spawnPosition = agent.Tile.ToWorld(Ctx.Board.Scale, Ctx.Board.Position);
//             agent.transform.position = spawnPosition;
//         }
//     }
// }