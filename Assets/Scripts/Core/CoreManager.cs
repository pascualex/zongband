using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

using Zongband.Content;
using Zongband.View;
using Zongband.Games;
using Zongband.Input;

using ANE = System.ArgumentNullException;

namespace Zongband.Core
{
    public class CoreManager : MonoBehaviour
    {
        [SerializeField] private InputManager? InputManager;
        // [SerializeField] private UIManager? UIManager;
        [SerializeField] private Tilemap? Tilemap;
        [SerializeField] private GameContent? GameContent;

        private Game? Game;

        private void Start()
        {
            DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
            DOTween.defaultUpdateType = UpdateType.Manual;

            if (Tilemap == null) throw new ANE(nameof(Tilemap));
            if (GameContent == null) throw new ANE(nameof(GameContent));

            var gameView = new GameView(Tilemap);
            Game = new(GameContent, gameView);
        }

        private void Update()
        {
            if (InputManager == null) throw new ANE(nameof(InputManager));
            // if (UIManager == null) throw new ANE(nameof(UIManager));
            if (Game == null) throw new ANE(nameof(Game));

            InputManager.ProcessInput();
            DOTween.ManualUpdate(Time.deltaTime, Time.unscaledDeltaTime);
            Game.GameLoop();
            // UIManager.Refresh();
            InputManager.ClearInput();
        }
    }
}
