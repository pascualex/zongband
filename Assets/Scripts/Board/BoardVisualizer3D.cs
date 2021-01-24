using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Board))]
public class BoardVisualizer3D : MonoBehaviour {
    public float sphereRadius = 1.0f;

    private void OnDrawGizmosSelected() {
        Vector2 size = GetComponent<Board>().size;
        float scale = GetComponent<Board>().scale;
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
