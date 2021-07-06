#nullable enable

using UnityEngine;

using Zongband.Games.Core;

using ANE = System.ArgumentNullException;

namespace Zongband.UI
{
    public class CameraController : MonoBehaviour
    {
        public Vector3 CameraOffset = new Vector3(0f, 10f, 0f);
        public float FixedSpeed = 1f;
        public float VariableSpeed = 15f;

        [SerializeField] private Camera? MainCamera;

        public void Refresh(IGame game)
        {
            MoveCamera(game);
        }

        public void MoveCamera(IGame game)
        {
            if (MainCamera == null) throw new ANE(nameof(MainCamera));
            if (game == null) throw new ANE(nameof(game));
            if (game.Board == null) throw new ANE(nameof(game.Board));

            return;
            // var lastPlayer = game.LastPlayer;
            // if (lastPlayer == null) return;

            // var board = game.Board;
            // var playerPosition = lastPlayer.Tile.ToWorld(board.Scale, board.Position);
            // var targetPosition = playerPosition + CameraOffset;

            // var transform = MainCamera.transform;
            // var remainingDistance = Vector3.Distance(transform.position, targetPosition);
            // var variableDistance = remainingDistance * VariableSpeed;
            // var distance = (variableDistance + FixedSpeed) * Time.deltaTime;
            // transform.position = Vector3.MoveTowards(transform.position, targetPosition, distance);
        }
    }
}