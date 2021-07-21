using Zongband.Utils;

using RLEngine.Entities;
using RLEngine.Utils;

using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

namespace Zongband.View.Entities
{
    public class EntitiesView : MonoBehaviour
    {
        [SerializeField] private Vector3 origin = Vector3.zero;
        [SerializeField] private Vector3 scale = Vector3.one;
        [SerializeField] private float movementDuration = 1f;
        [SerializeField] private Ease movementEase = Ease.Linear;

        private readonly Dictionary<Entity, GameObject> entities = new();

        public void Add(Entity entity, Coords at)
        {
            if (entities.ContainsKey(entity))
            {
                Debug.LogWarning(Warnings.EntityAlreadyPresent(entity));
                return;
            }

            if (entity.Visuals is not GameObject prefab)
            {
                Debug.LogWarning(Warnings.VisualsType(entity.Visuals, typeof(GameObject)));
                return;
            }

            var position = CoordsToWorld(at);
            var model = GameObject.Instantiate(prefab, position, Quaternion.identity);
            entities.Add(entity, model);
        }

        public void Move(Entity entity, Coords to)
        {
            if (!entities.TryGetValue(entity, out var model))
            {
                Debug.LogWarning(Warnings.EntityNotPresent(entity));
                return;
            }

            var oldPosition = model.transform.position;
            var newPosition = CoordsToWorld(to);
            model.transform.DOMove(newPosition, movementDuration).SetEase(movementEase);

            var direction = newPosition - oldPosition;
            model.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }

        public void Remove(Entity entity)
        {
            if (!entities.TryGetValue(entity, out var model))
            {
                Debug.LogWarning(Warnings.EntityNotPresent(entity));
                return;
            }

            GameObject.Destroy(model);
            entities.Remove(entity);
        }

        private Vector3 CoordsToWorld(Coords coords)
        {
            var offset = new Vector3(coords.X + 0.5f, 0f, coords.Y + 0.5f);
            return origin + Vector3.Scale(offset, scale);
        }
    }
}
