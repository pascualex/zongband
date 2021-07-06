// // using UnityEngine;

// using Zongband.Games.Logic.Entities;
// using Zongband.Utils;

// namespace Zongband.Games.Logic.Actions
// {
//     public class ControlAction : Action
//     {
//         private readonly Agent Agent;
//         private readonly bool IsPlayer;

//         public ControlAction(Agent agent, bool isPlayer)
//         {
//             Agent = agent;
//             IsPlayer = isPlayer;
//         }

//         protected override bool ExecuteStart()
//         {
//             if (!CheckAlive(Agent)) return true;

//             Agent.IsPlayer = IsPlayer;
//             return true;
//         }
//     }
// }