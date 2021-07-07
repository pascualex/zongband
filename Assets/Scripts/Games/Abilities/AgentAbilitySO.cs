// // using UnityEngine;
// using System.Collections.Generic;

// using Zongband.Games.Actions;
// using Zongband.Games.Entities;

// namespace Zongband.Games.Abilities
// {
//     [CreateAssetMenu(fileName = "AgentAbility", menuName = "ScriptableObjects/AgentAbility")]
//     public class AgentAbilitySO : AbilitySO
//     {
//         public AgentAbilitySO()
//         {
//             Targets.Add("Main Caster");
//             Targets.Add("Main Target");
//         }

//         public Action CreateAction(Agent caster, Agent target, Action.Context ctx)
//         {
//             var agents = new List<Agent>() { caster, target };
//             return CreateAction(agents, ctx);
//         }
//     }
// }