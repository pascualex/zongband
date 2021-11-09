using Zongband.Utils;

using RLEngine.Core.Logs;

using UnityEngine;
using DG.Tweening;

namespace Zongband.View.Actions
{
    public partial class VisualActionExecutor
    {
        private const float movementDuration = 0.2f;
        private readonly Ease movementEase = Ease.OutQuad;

        public Sequence Move(MovementLog log)
        {
            var sequence = DOTween.Sequence();

            if (!entities.TryGetValue(log.Entity, out var entityGO))
            {
                Debug.LogWarning(Warnings.EntityNotPresent(log.Entity));
                return sequence;
            }

            var oldPosition = entityGO.transform.position;
            var newPosition = tilemap.GetCellCenterWorld(log.To.ToCell());
            sequence.Append(entityGO.transform
                .DOMove(newPosition, movementDuration)
                .SetEase(movementEase));

            var direction = newPosition - oldPosition;
            entityGO.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

            return sequence;
        }
    }
}
