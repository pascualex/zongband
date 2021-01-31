using UnityEngine;
using System;

using Zongband.Boards;
using Zongband.Entities;
using Zongband.Utils;

namespace Zongband.Turns
{
    public class AgentAI : MonoBehaviour
    {
        public Vector2Int GenerateMovement(Agent agent, Board board)
        {
            if (agent == null) throw new NullReferenceException();
            if (board == null) throw new NullReferenceException();

            Vector2Int[] directions = Directions.Randomized();
            foreach (Vector2Int direction in directions)
            {
                if (!board.IsDisplacementAvailable(agent, direction)) continue;
                return direction;
            }

            return Vector2Int.zero;
        }
    }
}