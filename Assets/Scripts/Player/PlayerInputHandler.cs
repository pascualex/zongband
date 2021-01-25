using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour {
    public Entity playerPrefab;
    public Board board;

    private Entity playerEntity;

    private void Start() {
        Entity entity = Instantiate(playerPrefab);
        board.AddEntity(entity, new Vector2Int(1, 1));
        playerEntity = entity;
    }
    
    public void OnMoveUp() {
        Move(Vector2Int.up);
    }

    public void OnMoveRight() {
        if (playerEntity == null) return;
        Move(Vector2Int.right);
    }

    public void OnMoveDown() {
        Move(Vector2Int.down);
    }

    public void OnMoveLeft() {
        Move(Vector2Int.left);
    }

    private void Move(Vector2Int delta) {
        if (playerEntity == null) return;
        if (!board.IsPositionAvailable(playerEntity, delta)) return;
        board.DisplaceEntity(playerEntity, delta);
    }
}
