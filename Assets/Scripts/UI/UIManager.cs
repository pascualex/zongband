using UnityEngine;
using System;

using Zongband.Game.Core;
using Zongband.Game.Boards;
using Zongband.Game.Entities;

namespace Zongband.UI
{
    public class UIManager : MonoBehaviour
    {
        public GameObject tileHighlighterPrefab;
        public Camera mainCamera;
        public GameManager gameManager;

        public Vector2 mousePosition { get; set; }

        private Transform tileHighlighter;

        public UIManager()
        {
            mousePosition = new Vector2(-1, -1);
        }

        private void Awake()
        {
            if (tileHighlighterPrefab == null) throw new NullReferenceException();
            if (mainCamera == null) throw new NullReferenceException();
            if (gameManager == null) throw new NullReferenceException();
        }

        private void Start()
        {
            tileHighlighter = Instantiate(tileHighlighterPrefab, transform).transform;
        }

        public void UpdateUI()
        {
            HighlightTile();
        }

        private void HighlightTile()
        {
            bool highlight = false;

            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Agent agent = gameManager.playerAgent;
                Board board = gameManager.board;

                Vector3 worldPosition = ray.GetPoint(distance);
                Vector3 localPosition = worldPosition - board.transform.position;
                Vector2Int position = new Vector2Int((int)localPosition.x, (int)localPosition.z);

                if (board.IsPositionAvailable(agent.GetEntity(), position))
                {
                    Vector3 finalLocalPosition = new Vector3(position.x, 0, position.y);
                    finalLocalPosition += new Vector3(0.5f, 0, 0.5f);
                    finalLocalPosition *= board.scale;
                    Vector3 finalWorldPosition = finalLocalPosition + board.transform.position;

                    tileHighlighter.transform.position = finalWorldPosition;
                    highlight = true;
                }
            }

            tileHighlighter.gameObject.SetActive(highlight);
        }
    }
}