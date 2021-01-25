using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Board", menuName = "ScriptableObjects/Board")]
public class BoardSO : ScriptableObject {
    public Vector2Int size = new Vector2Int(10, 10);
    public float scale = 1.0f;

    private void OnValidate() {
        size.x = Mathf.Max(size.x, 1);
        size.x = Mathf.Max(size.x, 1);
        if (scale <= 0) scale = 1.0f;
    }
}
