#nullable enable

using UnityEngine;
using UnityEngine.Tilemaps;

namespace Zongband.Game.Boards
{
    [CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/Tile")]
    public class TileSO : ScriptableObject
    {
        public bool blocksGround = false;
        public bool blocksAir = false;
        public TileBase? tileBase;
    }
}
