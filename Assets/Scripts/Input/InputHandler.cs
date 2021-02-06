using UnityEngine;
using UnityEngine.InputSystem;
using System;

using Zongband.Player;
using Zongband.UI;

namespace Zongband.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputHandler : MonoBehaviour
    {
        public PlayerController playerController;
        public UIManager uiManager;

        private void Awake()
        {
            if (playerController == null) throw new NullReferenceException();
            if (uiManager == null) throw new NullReferenceException();
        }

        public void OnMoveUp()
        {
            playerController.AttemptDisplacement(Vector2Int.up);
        }

        public void OnMoveRight()
        {
            playerController.AttemptDisplacement(Vector2Int.right);
        }

        public void OnMoveDown()
        {
            playerController.AttemptDisplacement(Vector2Int.down);
        }

        public void OnMoveLeft()
        {
            playerController.AttemptDisplacement(Vector2Int.left);
        }

        public void OnMoveMouse(InputValue value)
        {
            Vector2 mousePosition = value.Get<Vector2>();
            uiManager.SetMousePosition(mousePosition);
        }
    }
}
