using UnityEngine;

namespace Zongband.Entities
{
    [DisallowMultipleComponent()]
    [RequireComponent(typeof(Entity))]
    public abstract class EntityTransformer : MonoBehaviour
    {
        abstract public void Transform(Vector2Int to, float scale);
    }
}
