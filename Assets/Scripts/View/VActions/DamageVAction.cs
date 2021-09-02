using Zongband.Utils;

using RLEngine.Logs;

using UnityEngine;
using DG.Tweening;

namespace Zongband.View.VActions
{
    public class DamageVAction : ContextVAction
    {
        private readonly DamageLog log;
        private Tween? targetTween = null;

        public DamageVAction(DamageLog log, Context ctx) : base(ctx)
        {
            this.log = log;
        }

        protected override bool ProcessAndCheck()
        {
            if (targetTween == null) AnimateTarget();

            return !targetTween?.IsPlaying() ?? true;
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
    }
}
