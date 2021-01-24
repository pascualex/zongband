using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Board : MonoBehaviour {
    public Vector2Int size { get; } = new Vector2Int(10, 10);
    public float scale { get; } = 1.0f;

    private CellContent[][] cellContents;

    private void Awake() {
        if (size == null) throw new ArgumentNullException();
        if (size.x <= 0 || size.y <= 0) throw new ArgumentOutOfRangeException();

        cellContents = new CellContent[size.y][];
        for (int i = 0; i < size.y; i++) {
            cellContents[i] = new CellContent[size.x];
        }
    }

    public void AddCellContent(CellContent cellContent, Vector2Int at) {
        if (cellContent == null && at == null) throw new ArgumentNullException();
        if (cellContents == null) throw new NullReferenceException();
        if (at.y < 0 || at.y >= cellContents.Length) throw new IndexOutOfRangeException();
        if (at.x < 0 || at.x >= cellContents[0].Length) throw new IndexOutOfRangeException();

        cellContents[at.y][at.x] = cellContent;
        cellContent.Move(at, scale);
    }

    public void MoveCellContent(Vector2Int from, Vector2Int to) {
        if (from == null && to == null) throw new ArgumentNullException();
        if (from.y < 0 || from.y >= cellContents.Length) throw new IndexOutOfRangeException();
        if (from.x < 0 || from.x >= cellContents[0].Length) throw new IndexOutOfRangeException();
        if (to.y < 0 || to.y >= cellContents.Length) throw new IndexOutOfRangeException();
        if (to.x < 0 || to.x >= cellContents[0].Length) throw new IndexOutOfRangeException();
        if (cellContents[from.y][from.x] == null) throw new EmptyCellException();
        if (cellContents[to.y][to.x] != null) throw new NotEmptyCellException();

        cellContents[to.y][to.x] = cellContents[from.y][from.x];
        cellContents[from.y][from.x] = null;
        cellContents[to.y][to.x].Move(to, scale);
    }

    public void RemoveCellContent(Vector2Int at) {
        if (at == null) throw new ArgumentNullException();
        if (at.y < 0 || at.y >= cellContents.Length) throw new IndexOutOfRangeException();
        if (at.x < 0 || at.x >= cellContents[0].Length) throw new IndexOutOfRangeException();
        if (cellContents[at.y][at.x] == null) throw new EmptyCellException();

        cellContents[at.y][at.x].Remove();
        cellContents[at.y][at.x] = null;
    }
}
