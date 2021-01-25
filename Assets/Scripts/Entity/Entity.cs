using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    public Vector2Int position { get; private set; } = Vector2Int.zero;

    public void Move(Vector2Int to, float scale) {
        position = to;
        EntityTransformer entityTransformer = GetComponent<EntityTransformer>();
        if (entityTransformer != null) entityTransformer.Transform(to, scale);
        else Debug.LogWarning(Warnings.missingEntityTransformer);
    }

    public void Remove() {
        // TODO
    }
}
