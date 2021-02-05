using UnityEngine;

namespace Zongband.Game.Actions
{
    public abstract class GameAction : SyncAction
    {
        public override bool IsGameAction()
        {
            return true;
        }
    }
}