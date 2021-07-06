using UnityEngine;
using UnityEngine.Tilemaps;

using Zongband.Games.Core.Boards;
using Zongband.Utils;

namespace Zongband.Games.Data.Boards
{
    [CreateAssetMenu(fileName = "TerrainType", menuName = "ScriptableObjects/TerrainType")]
    public class TerrainTypeData : ScriptableObject, ITerrainTypeData<TileBase>
    {
        public bool BlocksGround => _BlocksGround;
        public bool _BlocksGround = false;
        public bool BlocksAir => _BlocksAir;
        public bool _BlocksAir = false;
        public TileBase Visuals => _Visuals.Value();
        public TileBase? _Visuals;
    }
}
