using UnityEngine;

using Zongband.Boards;
using Zongband.Entities;

namespace Zongband.Core {
    public class GameManager : MonoBehaviour {
        public Board board;
        public Entity playerPrefab;
        public Entity entityPrefab;

        private Entity playerEntity;

        private void Start() {
            Entity entity = Instantiate(entityPrefab);
            board.AddEntity(entity, new Vector2Int(5, 3));

            playerEntity = Instantiate(playerPrefab);
            board.AddEntity(playerEntity, new Vector2Int(1, 1));
        }

        public void AttemptMovePlayer(Vector2Int movement) {
            if (!board.IsPositionAvailable(playerEntity, movement)) return;
            MovePlayer(movement);
        }

        public void MovePlayer(Vector2Int movement) {
            board.DisplaceEntity(playerEntity, movement);
        }
    }
}
