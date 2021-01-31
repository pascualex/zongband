using UnityEngine;

using System;

namespace Zongband.Entities
{
    public class Entity : MonoBehaviour
    {
        public Vector2Int position { get; private set; }

        private void Awake()
        {
            position = Vector2Int.zero;
        }

        public void Move(Vector2Int to, float scale)
        {
            this.position = to;
            transform.position = new Vector3(to.x + 0.5f, 0, to.y + 0.5f) * scale;
        }

        public void OnRemove()
        {
            Destroy(this);
        }

        public bool IsAgent() {
            return GetComponent<Agent>() != null;
        }

        public Agent GetAgent() {
            if (!IsAgent()) throw new NullReferenceException();
            return GetComponent<Agent>();
        }
    }
}
