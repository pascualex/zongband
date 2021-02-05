using UnityEngine;
using System;

using Zongband.Game.Boards;
using Zongband.Game.Entities;

namespace Zongband.Game.Actions
{
    public class MovementAnimation : AsyncAction
    {
        private const float animationFixedSpeed = 1.0f;
        private const float animationVariableSpeed = 10.0f;

        private Entity entity;
        private Board board;
        private bool isCompleted;

        public MovementAnimation(Entity entity, Board board)
        {
            if (entity == null) throw new ArgumentNullException();
            if (board == null) throw new ArgumentNullException();

            this.entity = entity;
            this.board = board;
            isCompleted = false;
        }

        public override void CustomUpdate()
        {
            if (IsCompleted()) return;

            MoveTowardsTarget();
        }

        public override bool IsCompleted()
        {
            return isCompleted;
        }

        private void MoveTowardsTarget()
        {
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