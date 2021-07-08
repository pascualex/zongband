using UnityEngine;

using Zongband.Content.Boards;
using Zongband.Content.Entities;
using Zongband.Games;
using Zongband.Games.Boards;
using Zongband.Games.Entities;
using Zongband.Utils;

namespace Zongband.Content
{
    [CreateAssetMenu(fileName = "Game", menuName = "Content/Game")]
    public class GameContent : ScriptableObject, IGameContent
    {
        public Size boardSize;
        public TerrainType? defaultTerrainType;
        public EntityType? entityType;

        public Size BoardSize => boardSize;
        public ITerrainType DefaultTerrainType => defaultTerrainType.Value();
        public IEntityType PlayerEntityType => entityType.Value();
    }
}
