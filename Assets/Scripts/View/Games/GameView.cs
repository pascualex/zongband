using Zongband.View.Actions;
using Zongband.View.Assets;
using Zongband.Utils;

using RLEngine.Core.Games;
using RLEngine.Core.Logs;
using RLEngine.Core.Utils;

using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;
using System;

using ANE = System.ArgumentNullException;
using NRE = System.NullReferenceException;

namespace Zongband.View.Games
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private Transform? entitiesParent;
        [SerializeField] private Tilemap? tilemap;

        private VisualActionExecutor? visualActionExecutor;
        private Sequence? currentSequence = null;

        public bool IsReady => !currentSequence?.IsPlaying() ?? true;

        private void Awake()
        {
            if (entitiesParent == null) throw new ANE(nameof(entitiesParent));
            if (tilemap == null) throw new ANE(nameof(tilemap));

            visualActionExecutor = new(entitiesParent, tilemap);
            DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
            DOTween.defaultUpdateType = UpdateType.Manual;
        }

        public void Represent(Game game)
        {
            if (visualActionExecutor == null) throw new ANE(nameof(visualActionExecutor));

            visualActionExecutor.Clear();

            for (var i = 0; i < game.Board.Size.Y; i++)
            {
                for (var j = 0; j < game.Board.Size.X; j++)
                {
                    var coords = new Coords(j, i);
                    var tileType = game.Board.GetTileType(coords);
                    if (tileType == null) throw new NRE();
                    visualActionExecutor.Modify(coords, tileType);
                    foreach (var entity in game.Board.GetEntities(coords))
                    {
                        visualActionExecutor.Spawn(entity, coords);
                    }
                }
            }
        }

        public void Represent(ILog log)
        {
            if (visualActionExecutor == null) throw new ANE(nameof(visualActionExecutor));
            if (!IsReady) throw new InvalidOperationException();

            currentSequence = visualActionExecutor.Execute(log);
            if (currentSequence == null) Debug.LogWarning(Warnings.LogNotSupported(log));
        }

        public void ManualUpdate()
        {
            if (currentSequence == null) return;
            DOTween.ManualUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        }
    }
}
