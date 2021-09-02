using System.Collections.Generic;

namespace Zongband.View.VActions
{
    public class ParallelVAction : CombinedVAction
    {
        private readonly LinkedList<VAction> vActions = new();

        protected override bool ProcessAndCheck()
        {
            for (var node = vActions.First; node != null; )
            {
                node.Value.Process();
                var next = node.Next;
                if (node.Value.IsCompleted) vActions.Remove(node);
                node = next;
            }

            return vActions.Count == 0;
        }

        protected override void AddToCollection(VAction vAction)
        {
            vActions.AddLast(vAction);
        }
    }
}
