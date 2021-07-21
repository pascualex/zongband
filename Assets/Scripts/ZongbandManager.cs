using Zongband.Input;
using Zongband.View;
using Zongband.Content;

using RLEngine;
using RLEngine.Actions;
using RLEngine.State;
using RLEngine.Utils;

using UnityEngine;

using ANE = System.ArgumentNullException;

namespace Zongband
{
    public class ZongbandManager : MonoBehaviour
    {
        [SerializeField] private InputManager? inputManager;
        [SerializeField] private GameView? gameView;
        [SerializeField] private GameContentSO? gameContent;

        private Game? game;

        private void Start()
        {
            if (gameView is null) throw new ANE(nameof(gameView));
            if (gameContent is null) throw new ANE(nameof(gameContent));

            var state = new GameState(gameContent.BoardSize, gameContent.FloorType);
            game = new Game(gameContent);
            gameView.Represent(state);
        }

        private void Update()
        {
            if (inputManager == null) throw new ANE(nameof(inputManager));
            if (game == null) throw new ANE(nameof(game));

            inputManager.ProcessInput();
            // game.GameLoop();
            // UIManager.Refresh();
            inputManager.ClearInput();
        }
    }
}
