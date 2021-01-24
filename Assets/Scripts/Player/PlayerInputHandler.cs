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
        if (playerEntity == null) return;

        board.DisplaceEntity(playerEntity, Vector2Int.up);
    }

    public void OnMoveRight() {
        if (playerEntity == null) return;

        board.DisplaceEntity(playerEntity, Vector2Int.right);
    }

    public void OnMoveDown() {
        if (playerEntity == null) return;

        board.DisplaceEntity(playerEntity, Vector2Int.down);
    }

    public void OnMoveLeft() {
        if (playerEntity == null) return;
        
        board.DisplaceEntity(playerEntity, Vector2Int.left);
    }
}
