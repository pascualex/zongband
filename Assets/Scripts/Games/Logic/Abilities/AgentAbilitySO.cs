// // using UnityEngine;
// using System.Collections.Generic;

// using Zongband.Games.Logic.Actions;
// using Zongband.Games.Logic.Entities;

// namespace Zongband.Games.Logic.Abilities
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