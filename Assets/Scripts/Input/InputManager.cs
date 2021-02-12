#nullable enable

using UnityEngine;
using UnityEngine.InputSystem;

using Zongband.UI;
using Zongband.Game.Controllers;
using Zongband.Utils;

namespace Zongband.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour
    {
        public UIManager? uiManager;
        public PlayerController? playerController;

        public void ProcessInput()
        {
            InputSystem.Update();
        }

        public void ClearInput()
        {
            playerController?.Clear();
        }

        public void OnMove(InputValue value)
        {
            if (playerController == null) return;
            var vector = value.Get<Vector2>();
            var direction = new Coordinates((int)vector.x, (int)vector.y, true);
            playerController.PlayerAction = new PlayerAction(direction);
        }

        public void OnMoveMouse(InputValue value)
        {
            var mousePosition = value.Get<Vector2>();
            uiManager?.SetMousePosition(mousePosition);
        }
    }
}
