#nullable enable

using UnityEngine;

using Zongband.Game.Boards;
using Zongband.Game.Actions;
using Zongband.Game.Entities;

namespace Zongband.Game.Controllers
{
    public abstract class Controller : MonoBehaviour
    {
        public abstract Action? ProduceAction(Agent agent, Board board);
    }
}