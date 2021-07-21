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
            if (inputManager is null) throw new ANE(nameof(inputManager));
            if (gameView is null) throw new ANE(nameof(gameView));
            if (gameContent is null) throw new ANE(nameof(gameContent));

            var state = new GameState(gameContent.BoardSize, gameContent.FloorType);
            game = new Game(gameContent);
            inputManager.Game = game;
            gameView.Represent(state);

            var log = game.SetupExample();
            gameView.Represent(log);
        }

        private void Update()
        {
            if (inputManager == null) throw new ANE(nameof(inputManager));
            if (gameView is null) throw new ANE(nameof(gameView));
            if (game == null) throw new ANE(nameof(game));

            inputManager.ProcessInput();
            var log = game.ProcessTurns();
            if (log is not null) gameView.Represent(log);
            gameView.Refresh();
            inputManager.ClearInput();
        }
    }
}
