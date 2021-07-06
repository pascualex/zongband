using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

using Zongband.Games.Logic;
using Zongband.Games.View;
using Zongband.Games.View.Boards;
using Zongband.Games.Data;
using Zongband.Input;
using Zongband.UI;

using ANE = System.ArgumentNullException;

namespace Zongband.Core
{
    public class CoreManager : MonoBehaviour
    {
        [SerializeField] private InputManager? InputManager;
        // [SerializeField] private UIManager? UIManager;
        [SerializeField] private Tilemap? Tilemap;
        [SerializeField] private GameData? GameData;

        private Game<TileBase>? Game;

        private void Start()
        {
            DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
            DOTween.defaultUpdateType = UpdateType.Manual;

            if (Tilemap == null) throw new ANE(nameof(Tilemap));
            if (GameData == null) throw new ANE(nameof(GameData));

            var gameView = new GameView(Tilemap);
            Game = new(GameData, gameView);
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
