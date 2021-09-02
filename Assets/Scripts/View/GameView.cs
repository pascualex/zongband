using Zongband.View.VActions;
using Zongband.Utils;
using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Utils;

using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;
using System.Collections.Generic;

using ANE = System.ArgumentNullException;

namespace Zongband.View
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private Transform? entitiesParent = null;
        [SerializeField] private Tilemap? tilemap = null;
        [SerializeField] private Vector3 origin = Vector3.zero;
        [SerializeField] private Vector3 scale = Vector3.one;
        [SerializeField] float movementDuration = 1f;
        [SerializeField] Ease movementEase = Ease.Linear;

        private Context? ctx = null;
        private readonly Queue<VAction> mainVActions = new();

        public bool IsCompleted => mainVActions.Count == 0;

        private void Awake()
        {
            DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
            DOTween.defaultUpdateType = UpdateType.Manual;

            if (entitiesParent is null) throw new ANE(nameof(entitiesParent));
            if (tilemap is null) throw new ANE(nameof(tilemap));

            var parent = entitiesParent.transform;
            ctx = new(parent, tilemap, origin, scale, movementDuration, movementEase);
        }

        public void EnqueueState(GameState state)
        {
            if (ctx is null) throw new ANE(nameof(ctx));

            for (var i = 0; i < state.Board.Size.Y; i++)
            {
                for (var j = 0; j < state.Board.Size.X; j++)
                {
                    var at = new Coords(j, i);
                    var tileType = state.Board.GetTileType(at);
                    if (tileType is null) continue;
                    var log = new ModifyLog(tileType, tileType, at);
                    var newMainAction = new SequentialVAction();
                    newMainAction.Add(new ModifyVAction(log, ctx));
                    mainVActions.Enqueue(newMainAction);
                }
            }
        }

        public void EnqueueLog(Log log)
        {
            if (ctx is null) throw new ANE(nameof(ctx));

            if (log is CombinedLog combinedLog)
            {
                EnqueueCombinedLog(combinedLog);
                return;
            }

            var newMainAction = GetActionFromLog(log);
            if (newMainAction is not null) mainVActions.Enqueue(newMainAction);
            else Debug.LogWarning(Warnings.LogNotSupported(log));
        }

        private void EnqueueCombinedLog(CombinedLog combinedLog)
        {
            if (ctx is null) throw new ANE(nameof(ctx));

            var remainingPairs = new Stack<(CombinedVAction, IEnumerable<Log>)>();
            var newMainAction = CombinedVAction.FromIsParallel(combinedLog.IsParallel);
            remainingPairs.Push((newMainAction, combinedLog.Logs));

            while (remainingPairs.Count > 0)
            {
                var (combinedAction, logs) = remainingPairs.Pop();
                foreach (var log in logs)
                {
                    if (log is CombinedLog cl)
                    {
                        var newCombinedAction = CombinedVAction.FromIsParallel(cl.IsParallel);
                        combinedAction.Add(newCombinedAction);
                        remainingPairs.Push((newCombinedAction, cl.Logs));
                    }
                    else
                    {
                        var vAction = GetActionFromLog(log);
                        if (vAction is not null) combinedAction.Add(vAction);
                        else Debug.LogWarning(Warnings.LogNotSupported(log));
                    }
                }
            }

            mainVActions.Enqueue(newMainAction);
        }

        private VAction? GetActionFromLog(Log log)
        {
            if (ctx is null) throw new ANE(nameof(ctx));

            if      (log is   SpawnLog   spawnLog) return new   SpawnVAction(  spawnLog, ctx);
            else if (log is    MoveLog    moveLog) return new    MoveVAction(   moveLog, ctx);
            else if (log is DestroyLog destroyLog) return new DestroyVAction(destroyLog, ctx);
            else if (log is  ModifyLog  modifyLog) return new  ModifyVAction( modifyLog, ctx);
            else if (log is  DamageLog  damageLog) return new  DamageVAction( damageLog, ctx);

            return null;
        }

        public void Refresh()
        {
            while (mainVActions.Count > 0)
            {
                mainVActions.Peek().Process();
                if (!mainVActions.Peek().IsCompleted) break;
                mainVActions.Dequeue();
            }
            DOTween.ManualUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        }
    }
}
