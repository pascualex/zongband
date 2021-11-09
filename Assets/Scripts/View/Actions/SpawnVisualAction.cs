using Zongband.Utils;

using RLEngine.Core.Logs;
using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

using UnityEngine;
using DG.Tweening;

namespace Zongband.View.Actions
{
    public partial class VisualActionExecutor
    {
        public Sequence Spawn(SpawnLog log)
        {
            var sequence = DOTween.Sequence();

            Spawn(log.Entity, log.At);

            return sequence;
        }

        public void Spawn(IEntity entity, Coords at)
        {
            if (entities.ContainsKey(entity))
            {
                Debug.LogWarning(Warnings.EntityAlreadyPresent(entity));
                return;
            }

            var prefab = assetLoaders.EntityType.Get(entity.Type);
            var position = tilemap.GetCellCenterWorld(at.ToCell());
            var parent = entitiesParent;
            var entityGO = GameObject.Instantiate(prefab, position, Quaternion.identity, parent);
            entityGO.name = entity.Name;
            entities.Add(entity, entityGO);
        }
    }
}
