using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour {
    public Vector2Int position { get; private set; } = Vector2Int.zero;

    public void Move(Vector2Int to, float scale) {
        position = to;
        MoveInWorld(to, scale);
    }

    public void Remove() {
        // TODO
    }

    abstract public void MoveInWorld(Vector2Int to, float scale);
}
