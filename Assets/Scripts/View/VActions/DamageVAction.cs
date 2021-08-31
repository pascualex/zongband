using Zongband.Utils;

using RLEngine.Logs;

using UnityEngine;
using DG.Tweening;

namespace Zongband.View.VActions
{
    public class DamageVAction : VAction
    {
        private readonly DamageLog log;
        private Tween? targetTween = null;
        private Tween? attackerTween = null;

        public DamageVAction(DamageLog log, Context ctx) : base(ctx)
        {
            this.log = log;
        }

        protected override bool ProcessAndCheck()
        {
            if (targetTween == null && attackerTween == null)
            {
                AnimateTarget();
                AnimateAttacker();
            }

            return (!targetTween?.IsPlaying() ?? true) && (!attackerTween?.IsPlaying() ?? true);
        }

        private void AnimateTarget()
        {
            if (!ctx.VEntities.TryGetValue(log.Target, out var vEntity))
            {
                Debug.LogWarning(Warnings.EntityNotPresent(log.Target));
                return;
            }

            var capsule = vEntity.Model.transform.GetChild(0);
            var renderer = capsule.GetComponent<MeshRenderer>();
            var originalColor = renderer.material.color;

            var sequence = DOTween.Sequence();
            sequence.Append(renderer.material.DOColor(Color.white, 0.1f).SetEase(Ease.InQuad));
            sequence.Append(renderer.material.DOColor(originalColor, 0.1f).SetEase(Ease.OutQuad));
            targetTween = sequence;
        }

        private void AnimateAttacker()
        {
            if (log.Attacker is null) return;

            if (!ctx.VEntities.TryGetValue(log.Target, out var targetVEntity))
            {
                Debug.LogWarning(Warnings.EntityNotPresent(log.Target));
                return;
            }

            if (!ctx.VEntities.TryGetValue(log.Attacker, out var attackerVEntity))
            {
                Debug.LogWarning(Warnings.EntityNotPresent(log.Attacker));
                return;
            }

            var attackerPosition = attackerVEntity.Position;
            var targetPosition = targetVEntity.Position;
            var animationPosition = attackerPosition.LerpDistance(targetPosition, 0.5f);

            var model = attackerVEntity.Model;
            var direction = targetPosition - attackerPosition;
            model.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

            var sequence = DOTween.Sequence();
            sequence.Append(model.transform.DOMove(animationPosition, 0.1f).SetEase(Ease.InQuad));
            sequence.Append(model.transform.DOMove(attackerPosition, 0.1f).SetEase(Ease.OutQuad));
            attackerTween = sequence;
        }
    }
}
