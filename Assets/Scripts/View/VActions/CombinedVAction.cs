using Zongband.Utils;

using UnityEngine;

namespace Zongband.View.VActions
{
    public abstract class CombinedVAction : VAction
    {
        public void Add(VAction vAction)
        {
            if (HasStarted) Debug.LogWarning(Warnings.CombinedActionRunning);
            if (IsCompleted || vAction.IsCompleted) return;

            AddToCollection(vAction);
        }

        protected abstract void AddToCollection(VAction vAction);

        public static CombinedVAction FromIsParallel(bool isParallel)
        {
            return isParallel ? new ParallelVAction() : new SequentialVAction();
        }
    }
}
