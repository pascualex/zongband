using RLEngine.Core.Games;
using RLEngine.Core.Input;
using RLEngine.Core.Utils;

using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

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
            if (Game == null) throw new ANE(nameof(Game));

            Game.Input = null;
        }

        private void OnMove(InputValue value)
        {
            if (Game == null) throw new ANE(nameof(Game));
            var vector = value.Get<Vector2>();
            var direction = new Coords((int)vector.x, (int)vector.y);
            Game.Input = new MovementInput(direction, true);
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

        private void OnAuxiliaryAction()
        {
            if (Game == null) throw new ANE(nameof(Game));
            if (Game.CurrentAgent == null) return;
            var position = Game.CurrentAgent.Position + Coords.Right;
            var entities = Game.Board.GetEntities(position);
            if (entities.Count == 0) return;
            Game.Input = new AbilityInput(Game.Content.Ability, entities.First());
        }
    }
}
