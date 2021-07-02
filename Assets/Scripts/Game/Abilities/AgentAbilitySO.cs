#nullable enable

using UnityEngine;
using System.Collections.Generic;

using Zongband.Game.Actions;
using Zongband.Game.Entities;

namespace Zongband.Game.Abilities
{
    [CreateAssetMenu(fileName = "AgentAbility", menuName = "ScriptableObjects/AgentAbility")]
    public class AgentAbilitySO : AbilitySO
    {
        public AgentAbilitySO()
        {
            Targets.Add("Main Caster");
            Targets.Add("Main Target");
        }

        public Action CreateAction(Agent caster, Agent target, Action.Context ctx)
        {
            var agents = new List<Agent>() { caster, target };
            return CreateAction(agents, ctx);
        }
    }
}