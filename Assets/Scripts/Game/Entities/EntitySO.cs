using UnityEngine;

namespace Zongband.Game.Entities
{
    [CreateAssetMenu(fileName = "Entity", menuName = "ScriptableObjects/Entity")]
    public class EntitySO : ScriptableObject
    {
        public GameObject model = null;
    }
}
