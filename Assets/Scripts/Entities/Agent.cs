using UnityEngine;

using Zongband.Utils;

namespace Zongband.Entities
{
    [RequireComponent(typeof(Entity))]
    public class Agent : MonoBehaviour
    {
        public int tickWait = 10;

        public Entity GetEntity() {
            return GetComponent<Entity>();
        }
    }
}
