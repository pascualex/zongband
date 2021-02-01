using UnityEngine;
using UnityEngine.InputSystem;
using System;

using Zongband.Controllers;

namespace Zongband.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputHandler : MonoBehaviour
    {
        public PlayerController playerController;

        private void Awake()
        {
            if (playerController == null) throw new NullReferenceException();
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
    }
}
