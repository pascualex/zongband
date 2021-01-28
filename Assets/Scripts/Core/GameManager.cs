using UnityEngine;

using Zongband.Boards;
using Zongband.Entities;

namespace Zongband.Core
{
    public class GameManager : MonoBehaviour
    {
        public Board board;
        public Agent playerPrefab;
        public Entity entityPrefab;

        private Agent playerAgent;
        private Entity entity;

        private void Start()
        {
            entity = Instantiate(entityPrefab);
            board.Add(entity, new Vector2Int(5, 3));

            playerAgent = Instantiate(playerPrefab);
            board.Add(playerAgent, new Vector2Int(2, 1));

            for (int i = 1; i < (board.size.y - 1); i++)
            {
                board.ModifyTerrain(new Vector2Int(1, i), true);
            }
        }

        public void AttemptMovePlayer(Vector2Int movement)
        {
            if (!board.IsDisplacementAvailable(playerAgent, movement)) return;
            MovePlayer(movement);
        }

        public void MovePlayer(Vector2Int movement)
        {
            board.Displace(playerAgent, movement);
        }
    }
}
