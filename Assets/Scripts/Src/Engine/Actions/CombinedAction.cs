// // using UnityEngine;

// namespace  Zongband.Engine.Actions
// {
//     public abstract class CombinedAction : Action
//     {
//         public CombinedAction()
//         {
//             IsCompleted = true;
//         }

//         public virtual void Add(Action action)
//         {
//             if (!action.IsCompleted) IsCompleted = false;
//         }
//     }
// }