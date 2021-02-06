using UnityEngine;

using System;

namespace Zongband.Game.Entities
{
    public class Entity : MonoBehaviour
    {
        public EntitySO entitySO;
        public GameObject defaultModel;

        public Vector2Int position { get; set; }
        public bool removed { get; set; }

        public Entity()
        {
            position = Vector2Int.zero;
            removed = false;
        }

        private void Start()
        {
            if (entitySO == null) throw new NullReferenceException();
            if (defaultModel == null) throw new NullReferenceException();

            GameObject model = (entitySO.model != null) ? entitySO.model : defaultModel;
            Instantiate(model, transform);
        }

        public bool IsAgent()
        {
            return GetComponent<Agent>() != null;
        }

        public Agent GetAgent()
        {
            if (!IsAgent()) throw new NullReferenceException();
            return GetComponent<Agent>();
        }
    }
}
