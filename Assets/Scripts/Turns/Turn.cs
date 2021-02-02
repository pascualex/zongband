using UnityEngine;
using System;

using Zongband.Entities;

namespace Zongband.Turns
{
    public class Turn : IComparable<Turn>
    {
        public readonly Agent agent;
        public readonly int tick;

        public Turn(Agent agent, int tick)
        {
            if (agent == null) throw new NullReferenceException();

            this.agent = agent;
            this.tick = tick;
        }

        public int CompareTo(Turn other)
        {
            if (other == null) throw new NullReferenceException();

            if (other.tick != tick) return tick - other.tick;
            return agent.GetTurnCooldown() - other.agent.GetTurnCooldown();
        }
    }
}