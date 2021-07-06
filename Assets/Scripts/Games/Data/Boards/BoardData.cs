#nullable enable // TODO: Remove all

using UnityEngine;
using UnityEngine.Tilemaps;

using Zongband.Games.Core.Boards;
using Zongband.Utils;

namespace Zongband.Games.Data.Boards
{
    [CreateAssetMenu(fileName = "Board", menuName = "ScriptableObjects/Board")]
    public class BoardData : ScriptableObject, IBoardData<TileBase>
    {
        public Size Size { get; set; } = new(10, 10);
        public ITerrainTypeData<TileBase> DefaultTerrainType => _DefaultTerrainType.Value();
        public TerrainTypeData? _DefaultTerrainType;
    }
}
