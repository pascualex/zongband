using UnityEngine;

namespace Zongband.Entities
{
    public class EntityTransformer3D : EntityTransformer
    {
        public override void Transform(Vector2Int to, float scale)
        {
            transform.position = new Vector3(to.x + 0.5f, 0, to.y + 0.5f) * scale;
        }
    }
}
