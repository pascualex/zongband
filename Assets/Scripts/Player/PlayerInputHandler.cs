using UnityEngine;
using UnityEngine.InputSystem;

using Zongband.Core;

namespace Zongband.Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputHandler : MonoBehaviour
    {
        public GameManager gameManager;

        public void OnMoveUp()
        {
            gameManager.AttemptMovePlayer(Vector2Int.up);
        }

        public void OnMoveRight()
        {
            gameManager.AttemptMovePlayer(Vector2Int.right);
        }

        public void OnMoveDown()
        {
            gameManager.AttemptMovePlayer(Vector2Int.down);
        }

        public void OnMoveLeft()
        {
            gameManager.AttemptMovePlayer(Vector2Int.left);
        }
    }
}
