// // using UnityEngine;
// using System;
// using System.Collections.Generic;

// using Zongband.Games.Logic.Entities;

// namespace Zongband.Games.Logic.Turns
// {
//     public class TurnManager
//     {
//         private readonly LinkedList<Turn> Turns = new();
//         private bool HasStarted = false;

//         public void Add(Agent agent, bool priority)
//         {
//             var additionalTicks = priority ? 0 : agent.TurnCooldown;
//             var turn = new Turn(agent, GetCurrentTick() + additionalTicks);

//             if (!priority)
//             {
//                 for (var node = Turns.Last; node != null; node = node.Previous)
//                 {
//                     if (node.Value.CompareTo(turn) <= 0)
//                     {
//                         Turns.AddAfter(node, turn);
//                         return;
//                     }
//                 }
//             }

//             Turns.AddFirst(turn);
//         }

//         public void Remove(Agent agent)
//         {
//             var node = Turns.First;
//             while (node != null)
//             {
//                 var next = node.Next;
//                 if (node.Value.Agent == agent) Turns.Remove(node);
//                 node = next;
//             }
//         }

//         public void Next()
//         {
//             if (Turns.Count == 0) return;

//             HasStarted = true;

//             Add(Turns.First.Value.Agent, false);
//             Turns.RemoveFirst();
//         }

//         public Agent? GetCurrent()
//         {
//             if (Turns.Count == 0) return null;

//             return Turns.First.Value.Agent;
//         }

//         private int GetCurrentTick()
//         {
//             return HasStarted ? Turns.First.Value.Tick : 0;
//         }
//     }
// }