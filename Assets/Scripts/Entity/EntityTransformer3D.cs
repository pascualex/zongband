using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTransformer3D : EntityTransformer {

    override public void Transform(Vector2Int to, float scale) {
        transform.position = new Vector3(to.x + 0.5f, 0, to.y + 0.5f) * scale;
    }
}
