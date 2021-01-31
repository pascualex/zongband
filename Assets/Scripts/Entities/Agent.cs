using UnityEngine;

using Zongband.Utils;

namespace Zongband.Entities
{
    [RequireComponent(typeof(Entity))]
    public class Agent : MonoBehaviour
    {
        public Entity GetEntity() {
            return GetComponent<Entity>();
        }
    }
}
