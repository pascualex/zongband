using UnityEngine;
using System;
using System.Collections.Generic;

using Zongband.Entities;

namespace Zongband.Turns
{
    public class TurnManager : MonoBehaviour
    {
        private List<Agent> agents;
        private int index;

        public TurnManager()
        {
            agents = new List<Agent>();
            index = 0;
        }

        public void Add(Agent agent)
        {
            agents.Add(agent);
        }

        public Agent GetCurrent()
        {
            if (agents.Count == 0) throw new NoAgentsException();
            if (index < 0 || index >= agents.Count) throw new IndexOutOfRangeException();

            return agents[index];
        }

        public void NextTurn()
        {
            if (agents.Count == 0) throw new NoAgentsException();
            if (index < 0 || index >= agents.Count) throw new IndexOutOfRangeException();

            index = (index + 1) % agents.Count;
        }
    }
}