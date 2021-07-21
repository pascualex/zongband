using Zongband.Content.Boards;
using Zongband.Content.Entities;
using Zongband.Utils;
using RLEngine;
using RLEngine.Boards;
using RLEngine.Entities;
using RLEngine.Utils;

using UnityEngine;

namespace Zongband.Content
{
    [CreateAssetMenu(fileName = "GameContent", menuName = "ScriptableObjects/GameContent")]
    public class GameContentSO : ScriptableObject, IGameContent
    {
        [SerializeField] private int boardWidth = 10;
        [SerializeField] private int boardHeight = 10;
        [SerializeField] private TileTypeSO? floorType = null;
        [SerializeField] private TileTypeSO? wallType = null;
        [SerializeField] private EntityTypeSO? playerType = null;
        [SerializeField] private EntityTypeSO? goblinType = null;

        public Size BoardSize => new Size(boardWidth, boardHeight);
        public ITileType FloorType => floorType.Value();
        public ITileType WallType => wallType.Value();
        public IEntityType PlayerType => playerType.Value();
        public IEntityType GoblinType => goblinType.Value();
    }
}