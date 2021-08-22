using Zongband.Utils;

using RLEngine.Logs;
using RLEngine.Utils;

using UnityEngine;

namespace Zongband.View.VActions
{
    public class SpawnVAction : VAction
    {
        private readonly SpawnLog log;

        public SpawnVAction(SpawnLog log, Context ctx) : base(ctx)
        {
            this.log = log;
        }

        protected override bool ProcessStart()
        {
            if (ctx.Entities.ContainsKey(log.Entity))
            {
                Debug.LogWarning(Warnings.EntityAlreadyPresent(log.Entity));
                return true;
            }

            if (log.Entity.Visuals is not GameObject prefab)
            {
                Debug.LogWarning(Warnings.VisualsType(log.Entity.Visuals, typeof(GameObject)));
                return true;
            }

            var position = ctx.CoordsToWorld(log.At);
            var model = GameObject.Instantiate(prefab, position, Quaternion.identity);
            ctx.Entities.Add(log.Entity, model);

            return true;
        }
    }
}
