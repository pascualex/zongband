using Zongband.Utils;
using RLEngine;
using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;

using UnityEngine;

namespace Zongband.Content.Entities
{
    [CreateAssetMenu(fileName = "EntityType", menuName = "ScriptableObjects/EntityType")]
    public class EntityTypeSO : ScriptableObject, IEntityType
    {
        [SerializeField] private string displayName = "DEFAULT NAME";
        [SerializeField] private bool isAgent = false;
        [SerializeField] private int speed = 100;
        [SerializeField] private bool blocksGround = true;
        [SerializeField] private bool blocksAir = false;
        [SerializeField] private bool isGhost = false;
        [SerializeField] private bool isRoamer = true;
        [SerializeField] private GameObject? gameModel = null;
        [SerializeField] private EntityTypeSO? parent = null;

        public string Name => displayName;
        public bool IsAgent => isAgent;
        public int Speed => speed;
        public bool BlocksGround => blocksGround;
        public bool BlocksAir => blocksAir;
        public bool IsGhost => isGhost;
        public bool IsRoamer => isRoamer;
        public object? Visuals => gameModel;
        public IEntityType? Parent => parent;
    }
}