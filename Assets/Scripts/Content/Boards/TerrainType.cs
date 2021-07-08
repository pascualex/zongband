using UnityEngine;
using UnityEngine.Tilemaps;

using Zongband.Games.Boards;
using Zongband.Utils;

namespace Zongband.Content.Boards
{
    [CreateAssetMenu(fileName = "TerrainType", menuName = "Content/TerrainType")]
    public class TerrainType : ScriptableObject, ITerrainType
    {
        public bool blocksGround = false;
        public bool blockAir = false;
        public TileBase? visuals;

        public bool BlocksGround => blocksGround;
        public bool BlocksAir => blockAir;
        public object Visuals => visuals.Value();
    }
}
