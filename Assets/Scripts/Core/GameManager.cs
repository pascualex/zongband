using UnityEngine;
using UnityEngine.Tilemaps;

using Zongband.Boards;
using Zongband.Entities;

namespace Zongband.Core
{
    public class GameManager : MonoBehaviour
    {
        public Board board;
        public Agent playerPrefab;
        public Entity entityPrefab;
        public TileBase floorTile;
        public TileBase wallTile;

        private Agent playerAgent;
        private Entity entity;

        private void Start()
        {
            entity = Instantiate(entityPrefab);
            board.Add(entity, new Vector2Int(5, 3));

            playerAgent = Instantiate(playerPrefab);
            board.Add(playerAgent, new Vector2Int(3, 3));

            Vector2Int upRight = board.size - Vector2Int.one;
            Vector2Int downRight = new Vector2Int(board.size.x - 1, 0);
            Vector2Int downLeft = Vector2Int.zero;
            Vector2Int upLeft = new Vector2Int(0, board.size.y - 1);
            board.ModifyBoxTerrain(downLeft, upRight, false, floorTile);
            board.ModifyBoxTerrain(upLeft, upRight + new Vector2Int(0, -1), true, wallTile);
            board.ModifyBoxTerrain(upRight, downRight + new Vector2Int(-1, 0), true, wallTile);
            board.ModifyBoxTerrain(downRight, downLeft + new Vector2Int(0, 1), true, wallTile);
            board.ModifyBoxTerrain(downLeft, upLeft + new Vector2Int(1, 0), true, wallTile);
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
