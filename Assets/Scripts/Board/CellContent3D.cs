using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellContent3D : MonoBehaviour, CellContent {

    public void Move(Vector2 to, float scale) {
        transform.position = new Vector3(to.x + 0.5f, 0, to.y + 0.5f) * scale;
    }

    public void Remove() {

    }
}
