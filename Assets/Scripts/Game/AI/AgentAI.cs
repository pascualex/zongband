#nullable enable

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

            var directions = Directions.Randomized();
            var selectedDirection = Vector2Int.zero;
            foreach (var direction in directions)
            {
                if (board.IsDisplacementAvailable(agent, direction))
                {
                    selectedDirection = direction;
                    break;
                }
            }

            if (selectedDirection == Vector2Int.zero) return new NullActionPack();

            var action = new MovementAction(agent, board, selectedDirection);
            return new BasicActionPack(action);
        }
    }
}