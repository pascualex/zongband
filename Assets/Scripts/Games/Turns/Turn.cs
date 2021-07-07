// // using UnityEngine;
// using System;

// using Zongband.Games.Entities;

// namespace Zongband.Games.Turns
// {
//     public class Turn : IComparable<Turn>
//     {
//         public readonly Agent Agent;
//         public readonly int Tick;

//         public Turn(Agent agent, int tick)
//         {
//             Agent = agent;
//             Tick = tick;
//         }

//         public int CompareTo(Turn other)
//         {
//             if (other.Tick != Tick) return Tick.CompareTo(other.Tick);
//             var otherTP = other.Agent.TurnPriority;
//             if (otherTP != Agent.TurnPriority) return -Agent.TurnPriority.CompareTo(otherTP);
//             return Agent.TurnCooldown.CompareTo(other.Agent.TurnCooldown);
//         }
//     }
// }