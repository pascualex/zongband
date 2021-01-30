using UnityEngine;

using Zongband.Utils;

namespace Zongband.Entities
{
    public class Entity : MonoBehaviour
    {
        public Vector2Int position { get; private set; } = Vector2Int.zero;

        public void Move(Vector2Int to, float scale)
        {
            this.position = to;
            transform.position = new Vector3(to.x + 0.5f, 0, to.y + 0.5f) * scale;
        }

        public void OnRemove()
        {
            Destroy(this);
        }
    }
}
