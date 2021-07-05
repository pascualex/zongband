#nullable enable

using UnityEngine;

using Zongband.Game.Core;

using ANE = System.ArgumentNullException;

namespace Zongband.UI
{
    public class PlayerHighlighter : MonoBehaviour
    {
        [SerializeField] private GameManager? GameManager;
        [SerializeField] private GameObject? InitialCursorPrefab;

        private GameObject? Cursor;

        public void Awake()
        {
            if (InitialCursorPrefab != null) ChangeCursor(InitialCursorPrefab);
        }

        public void Refresh()
        {
            HighlightPlayer();
        }

        public void ChangeCursor(GameObject cursorPrefab)
        {
            if (Cursor != null) Destroy(Cursor);
            Cursor = Instantiate(cursorPrefab, transform);
            Cursor.gameObject.SetActive(false);
        }

        private void HighlightPlayer()
        {
            if (GameManager == null) throw new ANE(nameof(GameManager));
            if (Cursor == null) throw new ANE(nameof(Cursor));

            var highlight = false;
            var lastPlayer = GameManager.LastPlayer;
            if (lastPlayer != null && lastPlayer.IsAlive && lastPlayer.IsPlayer)
            {
                var gameModelContainer = lastPlayer.GameModelContainer;
                if (gameModelContainer == null) throw new ANE(nameof(gameModelContainer));

                Cursor.transform.position = gameModelContainer.transform.position;
                highlight = true;
            }

            Cursor.gameObject.SetActive(highlight);
        }
    }
}
