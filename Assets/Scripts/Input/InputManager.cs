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
            if (playerController == null) return;
            playerController.PlayerAction = null;
        }

        public void OnMove(InputValue value)
        {
            if (playerController == null) return;
            var direction = value.Get<Vector2>();
            playerController.PlayerAction = new PlayerAction(direction.ToInt(), true);
        }

        public void OnMoveMouse(InputValue value)
        {
            var mousePosition = value.Get<Vector2>();
            uiManager?.SetMousePosition(mousePosition);
        }
    }
}
