using UnityEngine;
using System;

using Zongband.Game.Entities;

namespace Zongband.Game.Actions
{
    public class MakePlayerGameAction : GameAction
    {
        public Agent agent { get; private set; }

        public MakePlayerGameAction(Agent agent)
        {
            if (agent == null) throw new ArgumentNullException();

            this.agent = agent;
        }
    }
}