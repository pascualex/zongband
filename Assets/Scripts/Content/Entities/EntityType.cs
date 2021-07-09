using UnityEngine;

using Zongband.Engine.Entities;
using Zongband.Utils;

namespace Zongband.Content.Entities
{
    [CreateAssetMenu(fileName = "EntityType", menuName = "Content/EntityType")]
    public class EntityType : ScriptableObject, IEntityType
    {
        public bool IsAgent => isAgent;
        public bool BlocksGround => blocksGround;
        public bool BlocksAir => blocksAir;
        public bool IsGhost => isGhost;
        public object? Visuals => visuals;

        [SerializeField] private bool isAgent = false;
        [SerializeField] private bool blocksGround = true;
        [SerializeField] private bool blocksAir = false;
        [SerializeField] private bool isGhost = false;
        [SerializeField] private GameObject? visuals;
    }
}
