#nullable enable

using UnityEngine;
using UnityEngine.Tilemaps;

namespace Zongband.Game.Boards
{
    [CreateAssetMenu(fileName = "Terrain", menuName = "ScriptableObjects/Terrain")]
    public class TerrainSO : ScriptableObject
    {
        public bool blocksGround = false;
        public bool blocksAir = false;
        public TileBase? tileBase;
    }
}
