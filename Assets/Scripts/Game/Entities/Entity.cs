using UnityEngine;
using System;

namespace Zongband.Game.Entities
{
    public class Entity : MonoBehaviour
    {
        public Vector2Int position { get; set; }
        public bool removed { get; set; }

        private EntitySO entitySO;

        public Entity()
        {
            position = Vector2Int.zero;
            removed = false;
        }

        public virtual void Setup(EntitySO entitySO)
        {
            if (entitySO == null) throw new ArgumentNullException();

            this.entitySO = entitySO;

            if (entitySO.model != null) Instantiate(entitySO.model, transform);
        }
    }
}
