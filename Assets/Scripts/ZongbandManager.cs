using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

using Zongband.Content;
using Zongband.View;
using Zongband.Engine;
using Zongband.Input;

using ANE = System.ArgumentNullException;

namespace Zongband
{
    public class ZongbandManager : MonoBehaviour
    {
        [SerializeField] private InputManager? inputManager;
        // [SerializeField] private UIManager? UIManager;
        [SerializeField] private Tilemap? tilemap;
        [SerializeField] private GameContent? gameContent;

        private Game? game;

        private void Start()
        {
            DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
            DOTween.defaultUpdateType = UpdateType.Manual;

            if (tilemap == null) throw new ANE(nameof(tilemap));
            if (gameContent == null) throw new ANE(nameof(gameContent));

            var gameView = new GameView(tilemap);
            game = new(gameContent, gameView);
        }

        private void Update()
        {
            if (inputManager == null) throw new ANE(nameof(inputManager));
            // if (UIManager == null) throw new ANE(nameof(UIManager));
            if (game == null) throw new ANE(nameof(game));

            inputManager.ProcessInput();
            DOTween.ManualUpdate(Time.deltaTime, Time.unscaledDeltaTime);
            game.GameLoop();
            // UIManager.Refresh();
            inputManager.ClearInput();
        }
    }
}
