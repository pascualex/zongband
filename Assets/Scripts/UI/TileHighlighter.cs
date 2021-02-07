using UnityEngine;
using System;

using Zongband.Game.Core;
using Zongband.Game.Boards;
using Zongband.Game.Entities;
using Zongband.Utils;

namespace Zongband.UI
{
    public class TileHighlighter : MonoBehaviour, ICustomUpdatable
    {
        public GameObject cursorPrefab;
        public GameManager gameManager;

        public Vector2Int boardPosition { get; set; }

        private Transform cursor;

        public TileHighlighter()
        {
            boardPosition = new Vector2Int(-1, -1);
            cursor = null;
        }

        private void Awake()
        {
            if (cursorPrefab == null) throw new NullReferenceException();
            if (gameManager == null) throw new NullReferenceException();

            cursor = Instantiate(cursorPrefab, transform).transform;
        }

        public void CustomUpdate()
        {
            HighlightTile();
        }

        private void HighlightTile()
        {
            bool highlight = false;

            Agent agent = gameManager.playerAgent;
            Board board = gameManager.board;

            if (board.IsPositionAvailable(agent, boardPosition))
            {
                Vector3 finalLocalPosition = new Vector3(boardPosition.x, 0, boardPosition.y);
                finalLocalPosition += new Vector3(0.5f, 0, 0.5f);
                finalLocalPosition *= board.scale;
                Vector3 finalWorldPosition = finalLocalPosition + board.transform.position;

                cursor.transform.position = finalWorldPosition;
                highlight = true;
            }

            cursor.gameObject.SetActive(highlight);
        }
    }
}