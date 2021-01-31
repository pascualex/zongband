using UnityEngine;
using UnityEngine.InputSystem;

namespace Zongband.Player
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(PlayerAgentController))]
    public class PlayerInputHandler : MonoBehaviour
    {
        public void OnMoveUp()
        {
            GetComponent<PlayerAgentController>().AttemptDisplacement(Vector2Int.up);
        }

        public void OnMoveRight()
        {
            GetComponent<PlayerAgentController>().AttemptDisplacement(Vector2Int.right);
        }

        public void OnMoveDown()
        {
            GetComponent<PlayerAgentController>().AttemptDisplacement(Vector2Int.down);
        }

        public void OnMoveLeft()
        {
            GetComponent<PlayerAgentController>().AttemptDisplacement(Vector2Int.left);
        }
    }
}
