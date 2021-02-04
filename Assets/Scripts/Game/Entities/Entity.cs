using UnityEngine;

using System;

namespace Zongband.Game.Entities
{
    public class Entity : MonoBehaviour
    {
        public static float animationFixedSpeed = 1.0f;
        public static float animationVariableSpeed = 10.0f;

        public Vector2Int position { get; private set; }
        public bool isInPosition { get; private set; }

        private Vector3 worldPosition;

        private void Awake()
        {
            position = Vector2Int.zero;
            worldPosition = transform.position;
            isInPosition = true;
        }

        private void Update()
        {
            if (!isInPosition) MoveTowardsTarget();
        }

        public void Move(Vector2Int to, float scale, bool instant)
        {
            position = to;
            worldPosition = new Vector3(to.x + 0.5f, 0, to.y + 0.5f) * scale;
            if (instant) transform.position = worldPosition;
            isInPosition = instant;
        }

        public void OnRemove()
        {
            Destroy(this);
        }

        public bool IsAgent()
        {
            return GetComponent<Agent>() != null;
        }

        public Agent GetAgent()
        {
            if (!IsAgent()) throw new NullReferenceException();
            return GetComponent<Agent>();
        }

        private void MoveTowardsTarget()
        {
            float remainingDistance = Vector3.Distance(transform.position, worldPosition);
            float variableDistance = remainingDistance * animationVariableSpeed;
            float distance = (variableDistance + animationFixedSpeed) * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, worldPosition, distance);
            
            isInPosition = (transform.position == worldPosition);
        }
    }
}
