using Zongband.View.Assets;

using RLEngine.Core.Logs;
using RLEngine.Core.Entities;

using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;
using System.Collections.Generic;

namespace Zongband.View.Actions
{
    public partial class VisualActionExecutor
    {
        private readonly Dictionary<IEntity, GameObject> entities = new();
        private readonly Transform entitiesParent;
        private readonly Tilemap tilemap;
        private readonly AssetLoaders assetLoaders = new();

        public VisualActionExecutor(Transform entitiesParent, Tilemap tilemap)
        {
            this.entitiesParent = entitiesParent;
            this.tilemap = tilemap;
        }

        public Sequence? Execute(ILog log) => log switch
        {
            // AbilityLog abilityLog => Cast(abilityLog),
            DamageLog       damageLog       => Damage (damageLog      ),
            DestructionLog  destructionLog  => Destroy(destructionLog ),
            HealingLog      healingLog      => Heal   (healingLog     ),
            ModificationLog modificationLog => Modify (modificationLog),
            MovementLog     movementLog     => Move   (movementLog    ),
            // ProjectileLog projectileLog => Shoot(projectileLog),
            SpawnLog        spawnLog        => Spawn  (spawnLog       ),
            _ => null,
        };

        public void Clear()
        {
            tilemap.ClearAllTiles();
            foreach (var entityGO in entities.Values)
            {
                GameObject.Destroy(entityGO);
            }
            entities.Clear();
        }
    }
}
