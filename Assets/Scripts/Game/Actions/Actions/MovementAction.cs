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
        private const float animationVariableSpeed = 10.0f;

        private readonly Entity entity;
        private readonly Board board;
        private readonly bool instant;
        private bool isCompleted;

        public MovementAction(Entity entity, Board board, Vector2Int delta)
        : this(entity, board, delta, false)
        {

        }

        public MovementAction(Entity entity, Board board, Vector2Int position, bool absolute)
        : this(entity, board, position, absolute, false)
        {

        }

        public MovementAction(Entity entity, Board board, Vector2Int position, bool absolute,
                              bool instant)
        {
            this.entity = entity;
            this.board = board;
            this.instant = instant;
            isCompleted = false;

            gameAction = new MovementGameAction(entity, position, absolute);
        }

        public override void CustomStart()
        {
            if (instant) MoveToTarget();
        }

        public override void CustomUpdate()
        {
            //MoveTowardsTarget();
        }

        protected override bool IsAnimationCompleted()
        {
            return isCompleted;
        }
        
        private void MoveToTarget()
        {
            if (isCompleted) return;

            // TODO: entity.transform.position = GetTargetPosition();
            isCompleted = true;
        }

        /*private void MoveTowardsTarget()
        {
            if (isCompleted) return;

            var targetPosition = GetTargetPosition();

            var transform = entity.transform;
            var remainingDistance = Vector3.Distance(transform.position, targetPosition);
            var variableDistance = remainingDistance * animationVariableSpeed;
            var distance = (variableDistance + animationFixedSpeed) * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, distance);

            isCompleted = transform.position == targetPosition;
        }

        private Vector3 GetTargetPosition()
        {
            var position = entity.position;
            var scale = board.scale;

            var relativePosition = new Vector3(position.x + 0.5f, 0, position.y + 0.5f) * scale;
            var absolutePosition = board.transform.position + relativePosition;

            return absolutePosition;
        }*/
    }
}