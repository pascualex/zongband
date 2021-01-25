using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Board))]
public class BoardVisualizer3D : MonoBehaviour {
    public float sphereRadius = 1.0f;

    private void OnDrawGizmosSelected() {
        Vector2Int size;
        float scale;

        if (Application.isPlaying) {
            size = GetComponent<Board>().size;
            scale = GetComponent<Board>().scale;
        } else {
            BoardSO boardSO = GetComponent<Board>().boardData;
            if (boardSO == null) return;

            size = boardSO.size;
            scale = boardSO.scale;
        }

        Gizmos.color = Color.green;
        
        for (int i = 0; i < size.y; i++) {
            for (int j = 0; j < size.x; j++) {
                Vector3 position = transform.position;
                position += new Vector3(j + 0.5f, 0, i + 0.5f) * scale;
                Gizmos.DrawSphere(position, sphereRadius);
            }
        }
    }
}
