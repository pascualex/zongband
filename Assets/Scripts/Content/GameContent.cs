using UnityEngine;
using UnityEngine.Tilemaps;

using Zongband.Content.Boards;
using Zongband.Games;
using Zongband.Games.Boards;
using Zongband.Utils;

namespace Zongband.Content
{
    [CreateAssetMenu(fileName = "Game", menuName = "Content/Game")]
    public class GameContent : ScriptableObject, IGameContent<TileBase>
    {
        public Size _Size;
        public Size BoardSize => _Size;
        public TerrainType? _DefaultTerrainType;
        public ITerrainType<TileBase> DefaultTerrainType => _DefaultTerrainType.Value();
    }
}
