using Zongband.Utils;

using RLEngine.Core.Logs;

using UnityEngine;
using DG.Tweening;

namespace Zongband.View.Actions
{
    public partial class VisualActionExecutor
    {
        private const float height = 1.0f;

        public Sequence Shoot(ProjectileLog log)
        {
            var sequence = DOTween.Sequence();

            if (log.Target == log.Source) return sequence;

            var from = tilemap.GetCellCenterWorld(log.Source.Position.ToCell());
            var to = tilemap.GetCellCenterWorld(log.Target.Position.ToCell());

            from.y += height;
            to.y += height;

            var prefab = assetLoaders.EntityType.GetDefault();
            var parent = entitiesParent;
            var projectileGO = GameObject.Instantiate(prefab, from, Quaternion.identity, parent);
            projectileGO.name = "Projectile";

            sequence.Append(projectileGO.transform
                .DOMove(to, movementDuration)
                .SetEase(movementEase)
                .OnComplete(() => GameObject.Destroy(projectileGO)));

            return sequence;
        }
    }
}
