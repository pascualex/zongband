using Zongband.Utils;

using RLEngine.Logs;

using UnityEngine;

namespace Zongband.View.VActions
{
    public class DestroyVAction : ContextVAction
    {
        private readonly DestroyLog log;

        public DestroyVAction(DestroyLog log, Context ctx) : base(ctx)
        {
            this.log = log;
        }

        protected override bool ProcessAndCheck()
        {
            if (!ctx.VEntities.TryGetValue(log.Entity, out var vEntity))
            {
                Debug.LogWarning(Warnings.EntityNotPresent(log.Entity));
                return true;
            }

            GameObject.Destroy(vEntity.Model);
            ctx.VEntities.Remove(log.Entity);

            return true;
        }
    }
}
