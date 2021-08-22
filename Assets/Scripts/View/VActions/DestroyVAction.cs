using Zongband.Utils;

using RLEngine.Logs;

using UnityEngine;

namespace Zongband.View.VActions
{
    public class DestroyVAction : VAction
    {
        private readonly DestroyLog log;

        public DestroyVAction(DestroyLog log, Context ctx) : base(ctx)
        {
            this.log = log;
        }

        protected override bool ProcessStart()
        {
            if (!ctx.Entities.TryGetValue(log.Entity, out var model))
            {
                Debug.LogWarning(Warnings.EntityNotPresent(log.Entity));
                return true;
            }

            GameObject.Destroy(model);
            ctx.Entities.Remove(log.Entity);

            return true;
        }
    }
}
