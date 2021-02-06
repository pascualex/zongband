using UnityEngine;
using System;

using Zongband.Game.Core;
using Zongband.Game.Boards;
using Zongband.Game.Entities;
using Zongband.Utils;

namespace Zongband.UI
{
    public class UIManager : MonoBehaviour, ICustomUpdatable
    {
        public TileHighlighter tileHighlighter;
        public Camera mainCamera;
        public GameManager gameManager;

        private Vector2 mousePosition;

        public UIManager()
        {
            mousePosition = new Vector2(-1, -1);
        }

        private void Awake()
        {
            if (tileHighlighter == null) throw new NullReferenceException();
            if (mainCamera == null) throw new NullReferenceException();
            if (gameManager == null) throw new NullReferenceException();
        }

        public void CustomUpdate()
        {
            tileHighlighter.CustomUpdate();
        }

        public void SetMousePosition(Vector2 mousePosition)
        {
            this.mousePosition = mousePosition;

            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            
            Vector2Int boardPosition = new Vector2Int(-1, -1);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Board board = gameManager.board;

                Vector3 worldPosition = ray.GetPoint(distance);
                Vector3 localPosition = worldPosition - board.transform.position;
                boardPosition = new Vector2Int((int)localPosition.x, (int)localPosition.z);
            }

            tileHighlighter.boardPosition = boardPosition;
        }
    }
}