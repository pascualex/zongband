using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Board : MonoBehaviour {
    public Vector2Int size { get; } = new Vector2Int(10, 10);
    public float scale { get; } = 1.0f;

    private Entity[][] entities;

    private void Awake() {
        if (size == null) throw new ArgumentNullException();
        if (size.x <= 0 || size.y <= 0) throw new ArgumentOutOfRangeException();

        entities = new Entity[size.y][];
        for (int i = 0; i < size.y; i++) {
            entities[i] = new Entity[size.x];
        }
    }

    public void AddEntity(Entity entity, Vector2Int at) {
        if (entities == null) throw new NullReferenceException();
        if (entity == null) throw new ArgumentNullException();
        if (!Checker.Range(at, size)) throw new ArgumentOutOfRangeException();

        entities[at.y][at.x] = entity;
        entity.Move(at, scale);
    }

    public void MoveEntity(Entity entity, Vector2Int to) {
        if (entity == null) throw new ArgumentNullException();
        if (!CheckEntityPosition(entity)) throw new NotInPositionException();

        MoveEntity(entity.position, to);
    }

    public void MoveEntity(Vector2Int from, Vector2Int to) {
        if (!Checker.Range(from, size)) throw new ArgumentOutOfRangeException();
        if (!Checker.Range(to, size)) throw new ArgumentOutOfRangeException();
        if (entities[from.y][from.x] == null) throw new EmptyCellException();
        if (entities[to.y][to.x] != null) throw new NotEmptyCellException();

        entities[to.y][to.x] = entities[from.y][from.x];
        entities[from.y][from.x] = null;
        entities[to.y][to.x].Move(to, scale);
    }

    public void DisplaceEntity(Entity entity, Vector2Int delta) {
        if (entity == null) throw new ArgumentNullException();
        if (!CheckEntityPosition(entity)) throw new NotInPositionException();

        DisplaceEntity(entity.position, delta);
    }

    public void DisplaceEntity(Vector2Int from, Vector2Int delta) {
        MoveEntity(from, from + delta);
    }

    public void RemoveEntity(Entity entity) {
        if (entity == null) throw new ArgumentNullException();
        if (!CheckEntityPosition(entity)) throw new NotInPositionException();

        RemoveEntity(entity.position);
    }

    public void RemoveEntity(Vector2Int at) {
        if (!Checker.Range(at, size)) throw new ArgumentOutOfRangeException();
        if (entities[at.y][at.x] == null) throw new EmptyCellException();

        entities[at.y][at.x].Remove();
        entities[at.y][at.x] = null;
    }

    private bool CheckEntityPosition(Entity entity) {
        if (entity == null) throw new ArgumentNullException();
        if (!Checker.Range(entity.position, size)) throw new ArgumentOutOfRangeException();
        
        return entities[entity.position.y][entity.position.x] == entity;
    } 
}
