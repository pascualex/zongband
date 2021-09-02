using Zongband.Utils;

using RLEngine.Logs;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace Zongband.View.VActions
{
    public class ModifyVAction : ContextVAction
    {
        private readonly ModifyLog log;

        public ModifyVAction(ModifyLog log, Context ctx) : base(ctx)
        {
            this.log = log;
        }

        protected override bool ProcessAndCheck()
        {
            if (log.NewType.Visuals is not TileBase tilebase)
            {
                Debug.LogWarning(Warnings.VisualsType(log.NewType.Visuals, typeof(TileBase)));
                return true;
            }

            var position = new Vector3Int(log.At.X, log.At.Y, 0);
            ctx.Tilemap.SetTile(position, tilebase);

            return true;
        }
    }
}
