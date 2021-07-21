using RLEngine;
using RLEngine.Input;
using RLEngine.Utils;

using UnityEngine;
using UnityEngine.InputSystem;

using PlayerInput = UnityEngine.InputSystem.PlayerInput;
using ANE = System.ArgumentNullException;

namespace Zongband.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour
    {
        public Game? Game { get; set; }

        public void ProcessInput()
        {
            InputSystem.Update();
        }

        public void ClearInput()
        {
            // TODO
        }

        private void OnMove(InputValue value)
        {
            if (Game == null) throw new ANE(nameof(Game));
            var vector = value.Get<Vector2>();
            var direction = new Coords((int)vector.x, (int)vector.y);
            Game.Input = new MoveInput(direction, true);
        }

        private void OnSkipTurn(InputValue value)
        {
            // if (PlayerController == null) throw new ANE(nameof(PlayerController));
            // var skipTurn = value.Get<float>() >= 0.5f;
            // PlayerController.SkipTurn = skipTurn;
        }

        private void OnMouseMove(InputValue value)
        {
            // if (UIManager == null) throw new ANE(nameof(UIManager));

            // var mousePosition = value.Get<Vector2>();
            // UIManager.SetMousePosition(mousePosition);
        }

        private void OnMouseLeftClick()
        {
            // if (UIManager == null) throw new ANE(nameof(UIManager));
            // if (PlayerController == null) throw new ANE(nameof(PlayerController));

            // // UIManager.HandleMouseLeftClick();
            // var mouseTile = UIManager.MouseTile;
            // PlayerController.PlayerAction = new PlayerAction(mouseTile, false, true);
        }

        private void OnMouseRightClick()
        {
            // if (UIManager == null) throw new ANE(nameof(UIManager));

            // UIManager.HandleCtrlMouseLeftClick();
        }

        private void OnCtrlMouseLeftClick()
        {
            // if (UIManager == null) throw new ANE(nameof(UIManager));

            // UIManager.HandleCtrlMouseLeftClick();
        }
    }
}
