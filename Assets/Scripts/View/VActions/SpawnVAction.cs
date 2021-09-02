using Zongband.View.Entities;
using Zongband.Utils;

using RLEngine.Logs;
using RLEngine.Utils;

using UnityEngine;

namespace Zongband.View.VActions
{
    public class SpawnVAction : ContextVAction
    {
        private readonly SpawnLog log;

        public SpawnVAction(SpawnLog log, Context ctx) : base(ctx)
        {
            this.log = log;
        }

        protected override bool ProcessAndCheck()
        {
            if (ctx.VEntities.ContainsKey(log.Entity))
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
            var parent = ctx.EntitiesParent;
            var model = GameObject.Instantiate(prefab, position, Quaternion.identity, parent);
            model.name = log.Entity.Name;
            var vEntity = new VEntity(model, position);
            ctx.VEntities.Add(log.Entity, vEntity);

            return true;
        }
    }
}
