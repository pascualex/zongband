using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent()]
public abstract class EntityTransformer : MonoBehaviour {

    abstract public void Transform(Vector2Int to, float scale);
}
