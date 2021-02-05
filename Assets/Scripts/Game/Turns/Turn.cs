using UnityEngine;
using System;

using Zongband.Game.Entities;

namespace Zongband.Game.Turns
{
    public class Turn : IComparable<Turn>
    {
        public readonly Agent agent;
        public readonly int tick;

        public Turn(Agent agent, int tick)
        {
            if (agent == null) throw new ArgumentNullException();

            this.agent = agent;
            this.tick = tick;
        }

        public int CompareTo(Turn other)
        {
            if (other == null) throw new ArgumentNullException();

            if (other.tick != tick) return tick - other.tick;
            int priorityDifference = other.agent.GetTurnPriority() - agent.GetTurnPriority();
            if (priorityDifference != 0) return priorityDifference;
            return agent.GetTurnCooldown() - other.agent.GetTurnCooldown();
        }
    }
}