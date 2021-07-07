using UnityEngine;
using UnityEngine.Tilemaps;

using Zongband.Games.Boards;
using Zongband.Utils;

namespace Zongband.Content.Boards
{
    [CreateAssetMenu(fileName = "TerrainType", menuName = "Content/TerrainType")]
    public class TerrainType : ScriptableObject, ITerrainType<TileBase>
    {
        public bool _BlocksGround = false;
        public bool BlocksGround => _BlocksGround;
        public bool _BlockAir = false;
        public bool BlocksAir => _BlockAir;
        public TileBase? _Visuals;
        public TileBase Visuals => _Visuals.Value();
    }
}
