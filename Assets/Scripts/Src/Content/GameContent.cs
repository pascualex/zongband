using UnityEngine;

using Zongband.Content.Entities;
using Zongband.Engine;
using Zongband.Engine.Boards;
using Zongband.Engine.Entities;
using Zongband.Utils;

using Terrain = Zongband.Content.Boards.Terrain;

namespace Zongband.Content
{
    [CreateAssetMenu(fileName = "Game", menuName = "Content/Game")]
    public class GameContent : ScriptableObject, IGameContent
    {
        public Size BoardSize => boardSize;
        public ITerrain FloorTerrain => floorTerrain.Value();
        public ITerrain WallTerrain => wallTerrain.Value();
        public IEntityType PlayerEntityType => entityType.Value();

        [SerializeField] private Size boardSize;
        [SerializeField] private Terrain? floorTerrain;
        [SerializeField] private Terrain? wallTerrain;
        [SerializeField] private EntityType? entityType;
    }
}
