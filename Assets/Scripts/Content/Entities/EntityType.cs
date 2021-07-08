using UnityEngine;

using Zongband.Games.Entities;
using Zongband.Utils;

namespace Zongband.Content.Entities
{
    [CreateAssetMenu(fileName = "EntityType", menuName = "Content/EntityType")]
    public class EntityType : ScriptableObject, IEntityType
    {
        public GameObject? visuals;

        public object Visuals => visuals.Value();
    }
}
