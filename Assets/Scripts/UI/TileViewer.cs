using UnityEngine;

using System;

namespace Zongband.UI
{
    public class TileViewer : MonoBehaviour
    {
        public Camera mainCamera;

        private void Awake()
        {
            if (mainCamera == null) throw new NullReferenceException();
        }

        public void DebugMouseProjectionPosition(Vector2 mousePosition)
        {
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 position = ray.GetPoint(distance);
                Debug.Log("Mouse at " + position);
            }
        }
    }
}