using UnityEngine;
using System;
using System.Collections.Generic;

using Zongband.Entities;

namespace Zongband.Turns
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

        public void Add(Agent agent)
        {
            int currentTick = hasStarted ? turns.First.Value.tick : 0;
            int additionalTicks = agent.GetTurnCooldown();
            Turn turn = new Turn(agent, currentTick + additionalTicks);

            for (LinkedListNode<Turn> node = turns.Last; node != null; node = node.Previous)
            {
                if (node.Value.CompareTo(turn) <= 0)
                {
                    turns.AddAfter(node, turn);
                    return;
                }
            }

            turns.AddLast(turn);
        }

        public void Next()
        {
            if (turns.Count == 0) throw new NoTurnsException();

            hasStarted = true;

            Add(turns.First.Value.agent);
            turns.RemoveFirst();
        }

        public Agent GetCurrent()
        {
            if (turns.Count == 0) throw new NoTurnsException();

            return turns.First.Value.agent;
        }
    }
}