using UnityEngine;

using Zongband.Content.Boards;
using Zongband.Content.Entities;
using Zongband.Engine;
using Zongband.Engine.Boards;
using Zongband.Engine.Entities;
using Zongband.Utils;

namespace Zongband.Content
{
    [CreateAssetMenu(fileName = "Game", menuName = "Content/Game")]
    public class GameContent : ScriptableObject, IGameContent
    {
        public Size BoardSize => boardSize;
        public ITileType FloorType => floorType.Value();
        public ITileType WallType => wallType.Value();
        public IEntityType PlayerType => playerType.Value();

        [SerializeField] private Size boardSize;
        [SerializeField] private TileType? floorType;
        [SerializeField] private TileType? wallType;
        [SerializeField] private EntityType? playerType;
    }
}
