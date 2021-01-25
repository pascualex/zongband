using UnityEngine;

namespace Zongband.Entities {
    [DisallowMultipleComponent()]
    public abstract class EntityTransformer : MonoBehaviour {

        abstract public void Transform(Vector2Int to, float scale);
    }
}
