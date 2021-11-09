using Zongband.Utils;

using RLEngine.Core.Logs;

using UnityEngine;
using DG.Tweening;

namespace Zongband.View.Actions
{
    public partial class VisualActionExecutor
    {
        public Sequence Heal(HealingLog log)
        {
            var sequence = DOTween.Sequence();

            if (!entities.TryGetValue(log.Target, out var targetGO))
            {
                Debug.LogWarning(Warnings.EntityNotPresent(log.Target));
                return sequence;
            }

            var capsule = targetGO.transform.GetChild(0);
            var renderer = capsule.GetComponent<MeshRenderer>();
            var originalColor = renderer.material.color;

            sequence.Append(renderer.material.DOColor(Color.green, 0.1f).SetEase(Ease.InQuad));
            sequence.Append(renderer.material.DOColor(originalColor, 0.1f).SetEase(Ease.OutQuad));

            return sequence;
        }
    }
}
