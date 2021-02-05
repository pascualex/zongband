using UnityEngine;
using System;
using System.Collections.Generic;

using Zongband.Game.Entities;

namespace Zongband.Game.Turns
{
    public class TurnManager : MonoBehaviour
    {
        private LinkedList<Turn> turns;
        private bool hasStarted;

        public TurnManager()
        {
            turns = new LinkedList<Turn>();
            hasStarted = false;
        }

        public void Add(Agent agent, bool priority)
        {
            if (agent == null) throw new ArgumentNullException();

            int additionalTicks = priority ? 0 : agent.GetTurnCooldown();
            Turn turn = new Turn(agent, GetCurrentTick() + additionalTicks);

            if (!priority)
            {
                for (LinkedListNode<Turn> node = turns.Last; node != null; node = node.Previous)
                {
                    if (node.Value.CompareTo(turn) <= 0)
                    {
                        turns.AddAfter(node, turn);
                        return;
                    }
                }
            }

            turns.AddFirst(turn);
        }

        public void Next()
        {
            if (turns.Count == 0) throw new NoTurnsException();

            hasStarted = true;

            Add(turns.First.Value.agent, false);
            turns.RemoveFirst();
        }

        public Agent GetCurrent()
        {
            if (turns.Count == 0) throw new NoTurnsException();

            return turns.First.Value.agent;
        }

        private int GetCurrentTick()
        {
            return hasStarted ? turns.First.Value.tick : 0;
        }
    }
}