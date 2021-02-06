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
            return GenerateRandomMovement(agent, board);
        }

        private ActionPack GenerateRandomMovement(Agent agent, Board board)
        {
            if (agent == null) throw new ArgumentNullException();
            if (board == null) throw new ArgumentNullException();

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

            if (selectedDirection == Vector2Int.zero) return new NullActionPack();

            SequentialActionPack actionPack = new SequentialActionPack();
            actionPack.Add(new PositionAction(agent.GetEntity(), selectedDirection));
            actionPack.Add(new MovementAnimation(agent.GetEntity(), board));
            return actionPack;
        }
    }
}