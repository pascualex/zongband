// using UnityEngine;
// using System.Collections.Generic;

// using ANE = System.ArgumentNullException;

// namespace Zongband.Games.Entities
// {
//     public class EntityAnimator : MonoBehaviour
//     {
//         [SerializeField] private Animator? Animator;

//         private readonly LinkedList<AnimationState> AttackStates = new LinkedList<AnimationState>();

//         public AnimationState Attack()
//         {
//             if (Animator == null) throw new ANE(nameof(Animator));

//             Animator.SetTrigger("Attack");
//             var state = new AnimationState();
//             AttackStates.AddLast(state);
//             return state;
//         }

//         public void ReadyAttack()
//         {
//             if (Animator == null) throw new ANE(nameof(Animator));

//             Animator.ResetTrigger("Attack");
//             foreach (var state in AttackStates) state.IsReady = true;
//         }

//         public void CompleteAttack()
//         {
//             while (AttackStates.Count > 0)
//             {
//                 if (!AttackStates.First.Value.IsReady) break;
//                 AttackStates.First.Value.IsCompleted = true;
//                 AttackStates.RemoveFirst();
//             }
//         }

//         public class AnimationState
//         {
//             public bool IsReady = false;
//             public bool IsCompleted = false;
//         }
//     }
// }
