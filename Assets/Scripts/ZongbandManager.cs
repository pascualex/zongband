using Zongband.Input;
using Zongband.View.Games;

using RLEngine.Yaml.Serialization;
using RLEngine.Core.Games;
using RLEngine.Core.Logs;

using UnityEngine;
using System.Collections.Generic;
using System.IO;

using ANE = System.ArgumentNullException;

namespace Zongband
{
    public class ZongbandManager : MonoBehaviour
    {
        [SerializeField] private InputManager? inputManager;
        [SerializeField] private GameView? gameView;

        private Game? game;

        private void Awake()
        {
            if (inputManager == null) throw new ANE(nameof(inputManager));
            if (gameView == null) throw new ANE(nameof(gameView));

            var contentPath = Path.Combine(Application.dataPath, "Content");
            var gameContent = YamlDeserializer.Deserialize(contentPath);
            game = new Game(gameContent);
            inputManager.Game = game;
        }

        private void Start()
        {
            if (gameView == null) throw new ANE(nameof(gameView));
            if (game == null) throw new ANE(nameof(game));

            game.SetupExample();
            gameView.Represent(game);
        }

        private void Update()
        {
            if (inputManager == null) throw new ANE(nameof(inputManager));
            if (gameView == null) throw new ANE(nameof(gameView));
            if (game == null) throw new ANE(nameof(game));

            inputManager.ProcessInput();
            if (gameView.IsReady)
            {
                var log = game.ProcessStep();
                if (log != null) gameView.Represent(log);
            }
            gameView.ManualUpdate();
            inputManager.ClearInput();
        }
    }
}
