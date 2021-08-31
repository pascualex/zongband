using UnityEngine;

namespace Zongband.View.Entities
{
    public class VEntity
    {
        public VEntity(GameObject model, Vector3 position)
        {
            Model = model;
            Position = position;
        }

        public GameObject Model { get; }
        public Vector3 Position { get; set; }
    }
}
