#nullable enable

using UnityEngine;
using System;

using Zongband.Game.Entities;

namespace Zongband.Game.Actions
{
    public class MakePlayerGameAction : GameAction
    {
        public readonly Agent agent;

        public MakePlayerGameAction(Agent agent)
        {
            this.agent = agent;
        }
    }
}