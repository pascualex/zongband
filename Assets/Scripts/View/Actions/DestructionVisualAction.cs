using Zongband.Utils;

using RLEngine.Core.Logs;

using UnityEngine;
using DG.Tweening;

namespace Zongband.View.Actions
{
    public partial class VisualActionExecutor
    {
        public Sequence Destroy(DestructionLog log)
        {
            var sequence = DOTween.Sequence();

            if (!entities.TryGetValue(log.Entity, out var entityGO))
            {
                Debug.LogWarning(Warnings.EntityNotPresent(log.Entity));
                return sequence;
            }

            GameObject.Destroy(entityGO);
            entities.Remove(log.Entity);

            return sequence;
        }
    }
}
