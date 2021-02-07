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

        private Entity entity;
        private Board board;
        private bool instant;
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
            if (entity == null) throw new ArgumentNullException();
            if (board == null) throw new ArgumentNullException();

            this.entity = entity;
            this.board = board;
            this.instant = instant;
            isCompleted = false;

            this.gameAction = new MovementGameAction(entity, position, absolute);
        }

        public override void CustomStart()
        {
            if (instant) MoveToTarget();
        }

        public override void CustomUpdate()
        {
            MoveTowardsTarget();
        }

        protected override bool IsAnimationCompleted()
        {
            return isCompleted;
        }
        
        private void MoveToTarget()
        {
            if (isCompleted) return;

            entity.transform.position = GetTargetPosition();
            isCompleted = true;
        }

        private void MoveTowardsTarget()
        {
            if (isCompleted) return;

            Vector3 targetPosition = GetTargetPosition();

            Transform transform = entity.transform;
            float remainingDistance = Vector3.Distance(transform.position, targetPosition);
            float variableDistance = remainingDistance * animationVariableSpeed;
            float distance = (variableDistance + animationFixedSpeed) * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, distance);

            isCompleted = (transform.position == targetPosition);
        }

        private Vector3 GetTargetPosition()
        {
            Vector2Int position = entity.position;
            float scale = board.scale;

            Vector3 relativePosition = new Vector3(position.x + 0.5f, 0, position.y + 0.5f) * scale;
            Vector3 absolutePosition = board.transform.position + relativePosition;

            return absolutePosition;
        }
    }
}