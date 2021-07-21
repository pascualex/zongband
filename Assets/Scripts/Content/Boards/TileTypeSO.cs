using RLEngine.Boards;

using UnityEngine;
using UnityEngine.Tilemaps;

namespace Zongband.Content.Boards
{
    [CreateAssetMenu(fileName = "TileType", menuName = "ScriptableObjects/TileType")]
    public class TileTypeSO : ScriptableObject, ITileType
    {
        [SerializeField] private string displayName = "DEFAULT NAME";
        [SerializeField] private bool blocksGround = false;
        [SerializeField] private bool blocksAir = false;
        [SerializeField] private TileBase? tileBase = null;

        public string Name => displayName;
        public bool BlocksGround => blocksGround;
        public bool BlocksAir => blocksAir;
        public object? Visuals => tileBase;
    }
}