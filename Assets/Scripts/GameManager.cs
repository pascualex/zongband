using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Board board;
    public CellContent3D cellContentPrefab;

    private void Start() {
        CellContent3D cellContent = Instantiate(cellContentPrefab);
        board.AddCellContent(cellContent, new Vector2Int(5, 3));
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            board.MoveCellContent(new Vector2Int(5, 3), Vector2Int.zero);
        }
    }
}
