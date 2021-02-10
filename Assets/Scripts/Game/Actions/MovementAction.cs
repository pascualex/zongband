#nullable enable

using UnityEngine;
using System;

using Zongband.Game.Boards;
using Zongband.Game.Entities;

namespace Zongband.Game.Actions
{
    public class MovementAction : Action
    {
        private const float animationFixedSpeed = 1.0f;
        private const float animationVariableSpeed = 15.0f;

        private readonly Entity entity;
        private readonly Board board;
        private readonly Vector2Int position;
        private readonly bool relative;
        private readonly bool instant;

        public MovementAction(Entity entity, Board board, Vector2Int position)
        : this(entity, board, position, false) { }

        public MovementAction(Entity entity, Board board, Vector2Int position, bool relative)
        : this(entity, board, position, relative, false) { }

        public MovementAction(Entity entity, Board board, Vector2Int position, bool relative,
                              bool instant)
        {
            this.entity = entity;
            this.board = board;
            this.position = position;
            this.relative = relative;
            this.instant = instant;
        }

        protected override bool ProcessStart()
        {
            if (!MoveInBoard()) return true;

            if (instant)
            {
                MoveToTargetInWorld();
                return true;
            }
            
            return false;
        }

        protected override bool ProcessUpdate()
        {
            return MoveTowardsTarget();
        }

        public bool MoveInBoard()
        {
            if (!board.IsPositionAvailable(entity, position, relative)) return false;
            board.Move(entity, position, relative);
            return true;
        }

        private void MoveToTargetInWorld()
        {
            entity.transform.position = GetTargetPosition();
        }

        private bool MoveTowardsTarget()
        {
            var targetPosition = GetTargetPosition();

            var transform = entity.transform;
            var remainingDistance = Vector3.Distance(transform.position, targetPosition);
            var variableDistance = remainingDistance * animationVariableSpeed;
            var distance = (variableDistance + animationFixedSpeed) * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, distance);

            return transform.position == targetPosition;
        }

        private Vector3 GetTargetPosition()
        {
            var position = entity.position;
            var scale = board.Scale;

            var relativePosition = new Vector3(position.x + 0.5f, 0, position.y + 0.5f) * scale;
            var absolutePosition = board.transform.position + relativePosition;

            return absolutePosition;
        }
    }
}