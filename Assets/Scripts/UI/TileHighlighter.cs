#nullable enable

using UnityEngine;

using Zongband.Game.Core;
using Zongband.Utils;

using ANE = System.ArgumentNullException;

namespace Zongband.UI
{
    public class TileHighlighter : MonoBehaviour
    {
        public Tile MouseTile { private get; set; } = Tile.MinusOne;

        [SerializeField] private GameManager? GameManager;
        [SerializeField] private TileHighlighterCursor? InitialCursorPrefab;
        private TileHighlighterCursor? Cursor;

        private void Awake()
        {
            if (InitialCursorPrefab != null) ChangeCursor(InitialCursorPrefab);
        }

        public void Refresh()
        {
            HighlightTile();
        }

        public void ChangeCursor(TileHighlighterCursor cursorPrefab)
        {
            if (Cursor != null) Destroy(Cursor);
            Cursor = Instantiate(cursorPrefab, transform);
            Cursor.SetNone();
        }

        private void HighlightTile()
        {
            if (GameManager == null) throw new ANE(nameof(GameManager));
            if (GameManager.Board == null) throw new ANE(nameof(GameManager.Board));
            if (Cursor == null) throw new ANE(nameof(Cursor));

            var board = GameManager.Board;
            var lastPlayer = GameManager.LastPlayer;
            if (lastPlayer != null && board.IsTileAvailable(lastPlayer, MouseTile, false))
            {
                var position = MouseTile.ToWorld(board.Scale, board.transform.position);
                Cursor.transform.position = position;
                var distance = MouseTile.GetDistance(lastPlayer.Tile);
                if (distance > 1) Cursor.SetNormal();
                else Cursor.SetWarning();
            }
            else Cursor.SetNone();
        }
    }
}