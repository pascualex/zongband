using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Board board;
    public Entity entityPrefab;

    private void Start() {
        Entity cellContent = Instantiate(entityPrefab);
        board.AddEntity(cellContent, new Vector2Int(5, 3));
    }
}
