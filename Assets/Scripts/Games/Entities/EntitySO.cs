using UnityEngine;

namespace Zongband.Games.Entities
{
    [CreateAssetMenu(fileName = "Entity", menuName = "ScriptableObjects/Entity")]
    public class EntitySO : ScriptableObject
    {
        public GameObject? GameModel = null;
    }
}
