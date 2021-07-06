// // using UnityEngine;
// using System.Collections.Generic;

// using Zongband.Games.Logic.Actions;
// using Zongband.Games.Logic.Entities;

// namespace Zongband.Games.Logic.Abilities
// {
//     public abstract class AbilitySO : ScriptableObject
//     {
//         public int ManaCost = 10;
//         public List<string> Targets = new List<string>();
//         public Effect[] Effects = new Effect[0];

//         protected Action CreateAction(List<Agent> agents, Action.Context ctx)
//         {
//             var action = new SequentialAction();

//             foreach (var effect in Effects)
//                 action.Add(effect.CreateAction(agents, ctx));

//             return action;
//         }

//         private void OnValidate()
//         {
//             foreach (var effect in Effects)
//             {
//                 effect.ClearOld();
//                 effect.OnValidate();
//             }
//         }
//     }
// }