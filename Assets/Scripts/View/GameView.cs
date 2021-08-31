using Zongband.View.VActions;
using Zongband.Utils;
using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Utils;

using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;

using ANE = System.ArgumentNullException;

namespace Zongband.View
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private Tilemap? tilemap = null;
        [SerializeField] private Vector3 origin = Vector3.zero;
        [SerializeField] private Vector3 scale = Vector3.one;
        [SerializeField] float movementDuration = 1f;
        [SerializeField] Ease movementEase = Ease.Linear;

        private Context? ctx = null;
        private VAction? currentVAction = null;
        private readonly Queue<VAction> vActions = new();

        public bool IsCompleted => (currentVAction?.IsCompleted ?? true) && vActions.Count == 0;

        private void Awake()
        {
            DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
            DOTween.defaultUpdateType = UpdateType.Manual;

            if (tilemap is null) throw new ANE(nameof(tilemap));

            ctx = new(tilemap, origin, scale, movementDuration, movementEase);
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
                    vActions.Enqueue(new ModifyVAction(log, ctx));
                }
            }
        }

        public void EnqueueLog(Log log)
        {
            if (ctx is null) throw new ANE(nameof(ctx));

            var remaining = new Stack<Log>();
            remaining.Push(log);

            while (remaining.Count > 0)
            {
                var c = remaining.Pop();
                if (c is CombinedLog combinedLog)
                {
                    foreach (var sublog in combinedLog.Logs.Reverse())
                    {
                        remaining.Push(sublog);
                    }
                }
                else
                {
                    VAction? va = null;
                    if (c is SpawnLog spawnLog) va = new SpawnVAction(spawnLog, ctx);
                    else if (c is MoveLog moveLog) va = new MoveVAction(moveLog, ctx);
                    else if (c is DestroyLog destroyLog) va = new DestroyVAction(destroyLog, ctx);
                    else if (c is ModifyLog modifyLog) va = new ModifyVAction(modifyLog, ctx);
                    else if (c is DamageLog damageLog) va = new DamageVAction(damageLog, ctx);

                    if (va != null) vActions.Enqueue(va);
                    else Debug.LogWarning(Warnings.LogNotSupported(c));
                }
            }
        }

        public void Refresh()
        {
            currentVAction?.Process();
            while ((currentVAction?.IsCompleted ?? true) && vActions.Count > 0)
            {
                currentVAction = vActions.Dequeue();
                currentVAction.Process();
            }

            DOTween.ManualUpdate(Time.deltaTime, Time.unscaledDeltaTime);
        }
    }
}
