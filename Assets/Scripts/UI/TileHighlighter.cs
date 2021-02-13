#nullable enable

using UnityEngine;

using Zongband.Game.Core;
using Zongband.Utils;

namespace Zongband.UI
{
    public class TileHighlighter : MonoBehaviour
    {
        public Tile boardTile = Tile.MinusOne;

        [SerializeField] private GameObject? initialCursorPrefab;
        [SerializeField] private GameManager? gameManager;
        private GameObject? cursor;

        public void CustomUpdate()
        {
            HighlightTile();
        }

        public void Refresh()
        {
            if (initialCursorPrefab != null) ChangeCursor(initialCursorPrefab);
        }

        public void ChangeCursor(GameObject cursorPrefab)
        {
            if (cursor != null) Destroy(cursor);
            cursor = Instantiate(cursorPrefab);
            cursor.gameObject.SetActive(false);
        }

        private void HighlightTile()
        {
            if (gameManager == null) return;
            if (cursor == null) return;

            var highlight = false;

            var agent = gameManager.LastPlayer;
            if (agent == null) return;

            var board = gameManager.board;
            if (board == null) return;

            if (board.IsTileAvailable(agent, boardTile, false))
            {
                var position = new Vector3(boardTile.x, 0, boardTile.y);
                position += new Vector3(0.5f, 0, 0.5f);
                position *= board.Scale;

                cursor.transform.position = position;
                highlight = true;
            }

            cursor.gameObject.SetActive(highlight);
        }
    }
}