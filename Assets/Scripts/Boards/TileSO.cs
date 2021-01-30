using UnityEngine;
using UnityEngine.Tilemaps;

namespace Zongband.Boards
{
    [CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/Tile")]
    public class TileSO : ScriptableObject
    {
        public bool blocksGround = false;
        public bool blocksAir = false;
        public TileBase tileBase;
        
        private void OnValidate()
        {
            
        }
    }
}
