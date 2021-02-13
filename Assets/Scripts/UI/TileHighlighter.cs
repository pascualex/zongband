#nullable enable

using UnityEngine;

using Zongband.Game.Core;
using Zongband.Utils;

namespace Zongband.UI
{
    public class TileHighlighter : MonoBehaviour
    {
        public Tile MouseTile { private get; set; } = Tile.MinusOne;

        [SerializeField] private GameManager? gameManager;
        [SerializeField] private GameObject? initialCursorPrefab;
        private GameObject? cursor;

        private void Awake()
        {
            if (initialCursorPrefab != null) ChangeCursor(initialCursorPrefab);
        }

        public void Refresh()
        {
            HighlightTile();
        }

        public void ChangeCursor(GameObject cursorPrefab)
        {
            if (cursor != null) Destroy(cursor);
            cursor = Instantiate(cursorPrefab, transform);
            cursor.SetActive(false);
        }

        private void HighlightTile()
        {
            if (gameManager == null) return;
            if (cursor == null) return;

            var board = gameManager.board;
            if (board == null) return;

            var highlight = false;

            var lastPlayer = gameManager.LastPlayer;
            if (lastPlayer != null && board.IsTileAvailable(lastPlayer, MouseTile, false))
            {
                var position = MouseTile.ToWorld(board.Scale, board.transform.position);
                cursor.transform.position = position;
                highlight = true;
            }

            cursor.SetActive(highlight);
        }
    }
}