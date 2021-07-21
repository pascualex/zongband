using Zongband.View.Boards;
using Zongband.View.Entities;
using Zongband.Utils;
using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Utils;

using UnityEngine;
using DG.Tweening;

using ANE = System.ArgumentNullException;

namespace Zongband.View
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private TilemapView? tilemapView;
        [SerializeField] private EntitiesView? entitiesView;

        private void Start()
        {
            DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
            DOTween.defaultUpdateType = UpdateType.Manual;
        }

        public void Represent(GameState state)
        {
            if (tilemapView is null) throw new ANE(nameof(tilemapView));

            for (var i = 0; i < state.Board.Size.Y; i++)
            {
                for (var j = 0; j < state.Board.Size.X; j++)
                {
                    var at = new Coords(j, i);
                    var tileType = state.Board.GetTileType(at);
                    if (tileType is null) continue;
                    tilemapView.Modify(tileType, at);
                }
            }
        }

        public void Represent(Log log)
        {
            if (log is CombinedLog combinedLog) RepresentLog(combinedLog);
            else if (log is SpawnLog spawnLog) RepresentLog(spawnLog);
            else if (log is MoveLog moveLog) RepresentLog(moveLog);
            else if (log is DestroyLog destroyLog) RepresentLog(destroyLog);
            else if (log is ModifyLog modifyLog) RepresentLog(modifyLog);
            else RepresentLog(log);
        }

        public void Refresh()
        {
            DOTween.ManualUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        }

        private void RepresentLog(CombinedLog log)
        {
            foreach (var subLog in log.Logs)
            {
                Represent(subLog);
            }
        }

        private void RepresentLog(SpawnLog log)
        {
            if (entitiesView is null) throw new ANE(nameof(entitiesView));

            entitiesView.Add(log.Entity, log.At);
        }

        private void RepresentLog(MoveLog log)
        {
            if (entitiesView is null) throw new ANE(nameof(entitiesView));

            entitiesView.Move(log.Entity, log.To);
        }

        private void RepresentLog(DestroyLog log)
        {
            if (entitiesView is null) throw new ANE(nameof(entitiesView));

            entitiesView.Remove(log.Entity);
        }

        private void RepresentLog(ModifyLog log)
        {
            if (tilemapView is null) throw new ANE(nameof(tilemapView));

            tilemapView.Modify(log.NewType, log.At);
        }

        private void RepresentLog(Log log)
        {
            Debug.LogWarning(Warnings.LogNotSupported(log));
        }
    }
}
