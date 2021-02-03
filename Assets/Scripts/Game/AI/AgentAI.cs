using UnityEngine;
using System;

using Zongband.Game.Boards;
using Zongband.Game.Actions;
using Zongband.Game.Entities;
using Zongband.Utils;

namespace Zongband.Game.AI
{
    public class AgentAI : MonoBehaviour
    {
        public ActionPack GenerateActionPack(Agent agent, Board board)
        {
            ActionPack actionPack = new ActionPack();

            MovementAction randomMovement = GenerateRandomMovement(agent, board);
            actionPack.AddMovementAction(randomMovement);

            return actionPack;
        }

        private MovementAction GenerateRandomMovement(Agent agent, Board board)
        {
            if (agent == null) throw new NullReferenceException();
            if (board == null) throw new NullReferenceException();

            Vector2Int[] directions = Directions.Randomized();
            Vector2Int selectedDirection = Vector2Int.zero;
            foreach (Vector2Int direction in directions)
            {
                if (board.IsDisplacementAvailable(agent.GetEntity(), direction))
                {
                    selectedDirection = direction;
                    break;
                }
            }

            return new MovementAction(agent.GetEntity(), selectedDirection);
        }
    }
}