// // using UnityEngine;
// using System.Collections.Generic;

// using Zongband.Engine.Actions;
// using Zongband.Engine.Entities;

// namespace  Zongband.Engine.Abilities
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