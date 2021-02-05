using UnityEngine;

using System;

namespace Zongband.Game.Entities
{
    public class Entity : MonoBehaviour
    {
        public Vector2Int position;
        public bool removed;

        public Entity()
        {
            position = Vector2Int.zero;
            removed = false;
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
    }
}
