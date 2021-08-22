using RLEngine.Entities;
using RLEngine.Utils;

using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;
using System.Collections.Generic;

namespace Zongband.View.VActions
{
    public abstract class VAction
    {
        protected Context ctx;

        private bool startExecuted = false;

        protected VAction(Context ctx)
        {
            this.ctx = ctx;
        }

        public bool IsCompleted { get; private set; } = false;

        public void Process()
        {
            if (IsCompleted) return;
            if (!startExecuted)
            {
                IsCompleted = ProcessStart();
                startExecuted = true;
            }
            else
            {
                IsCompleted = ProcessUpdate();
            }
        }

        protected virtual bool ProcessStart()
        {
            return false;
        }

        protected virtual bool ProcessUpdate()
        {
            return true;
        }

        public class Context
        {
            private readonly Vector3 origin;
            private readonly Vector3 scale;

            public Context(Tilemap tilemap, Vector3 origin, Vector3 scale, float movementDuration, Ease movementEase)
            {
                Tilemap = tilemap;
                this.origin = origin;
                this.scale = scale;
                MovementDuration = movementDuration;
                MovementEase = movementEase;
            }

            public Dictionary<Entity, GameObject> Entities { get; } = new();
            public Tilemap Tilemap { get; }
            public float MovementDuration { get; }
            public Ease MovementEase { get; }

            public Vector3 CoordsToWorld(Coords coords)
            {
                var offset = new Vector3(coords.X + 0.5f, 0f, coords.Y + 0.5f);
                return origin + Vector3.Scale(offset, scale);
            }
        }
    }
}
