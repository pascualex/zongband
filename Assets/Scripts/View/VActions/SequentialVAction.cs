using System.Collections.Generic;

namespace Zongband.View.VActions
{
    public class SequentialVAction : CombinedVAction
    {
        private readonly Queue<VAction> vActions = new();

        protected override bool ProcessAndCheck()
        {
            while (vActions.Count > 0)
            {
                vActions.Peek().Process();
                if (!vActions.Peek().IsCompleted) break;
                vActions.Dequeue();
            }

            return vActions.Count == 0;
        }

        protected override void AddToCollection(VAction vAction)
        {
            vActions.Enqueue(vAction);
        }
    }
}
