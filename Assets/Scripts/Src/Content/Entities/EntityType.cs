using UnityEngine;

using Zongband.Engine.Entities;
using Zongband.Utils;

namespace Zongband.Content.Entities
{
    [CreateAssetMenu(fileName = "EntityType", menuName = "Content/EntityType")]
    public class EntityType : ScriptableObject, IEntityType
    {
        public object Visuals => visuals.Value();

        [SerializeField] private GameObject? visuals;
    }
}
