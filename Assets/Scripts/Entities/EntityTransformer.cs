using UnityEngine;

namespace Zongband.Entities
{
    [DisallowMultipleComponent()]
    [RequireComponent(typeof(Entity))]
    public abstract class EntityTransformer : MonoBehaviour
    {
        public abstract void Transform(Vector2Int to, float scale);
    }
}
