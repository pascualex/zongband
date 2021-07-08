using UnityEngine;

using Zongband.Games.Boards;
using Zongband.Utils;

namespace Zongband.View.Boards
{
    public class EntityLayerView : IEntityLayerView
    {
        private Vector3 Origin;
        private Vector3 CellSize;

        public EntityLayerView(Vector3 origin, Vector2 cellSize)
        {
            Origin = origin;
            CellSize = cellSize;
        }

        public void Add(object entityVisuals, Tile at)
        {
            if (entityVisuals is not GameObject gameObject)
            {
                Debug.Log(Warnings.UnexpectedVisualsObject);
                return;
            }

            var offset = new Vector3((at.X + 0.5f) * CellSize.x, 0f, (at.Y + 0.5f) * CellSize.y);
            var position = Origin + offset;
            GameObject.Instantiate(gameObject, position, Quaternion.identity);
        }
    }
}
