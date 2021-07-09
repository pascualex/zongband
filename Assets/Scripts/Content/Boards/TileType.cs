using UnityEngine;
using UnityEngine.Tilemaps;

using Zongband.Engine.Boards;
using Zongband.Utils;

namespace Zongband.Content.Boards
{
    [CreateAssetMenu(fileName = "TileType", menuName = "Content/TileType")]
    public class TileType : ScriptableObject, ITileType
    {
        public bool BlocksGround => blocksGround;
        public bool BlocksAir => blockAir;
        public object? Visuals => visuals;

        [SerializeField] private bool blocksGround = false;
        [SerializeField] private bool blockAir = false;
        [SerializeField] private TileBase? visuals;
    }
}
