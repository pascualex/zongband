using Zongband.Utils;

using RLEngine.Logs;
using RLEngine.Utils;

using UnityEngine;
using DG.Tweening;

namespace Zongband.View.VActions
{
    public class MoveVAction : VAction
    {
        private readonly MoveLog log;
        private Tween? tween = null;

        public MoveVAction(MoveLog log, Context ctx) : base(ctx)
        {
            this.log = log;
        }

        protected override bool ProcessAndCheck()
        {
            if (tween == null)
            {
                if (!ctx.VEntities.TryGetValue(log.Entity, out var vEntity))
                {
                    Debug.LogWarning(Warnings.EntityNotPresent(log.Entity));
                    return true;
                }

                var model = vEntity.Model;

                var oldPosition = model.transform.position;
                var newPosition = ctx.CoordsToWorld(log.To);
                tween = model.transform.DOMove(newPosition, ctx.MovementDuration);
                tween.SetEase(ctx.MovementEase);

                var direction = newPosition - oldPosition;
                model.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

                vEntity.Position = newPosition;

                return false;
            }

            return !tween?.IsPlaying() ?? true;
        }
    }
}
