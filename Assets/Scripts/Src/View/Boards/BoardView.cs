using UnityEngine;
using UnityEngine.Tilemaps;

using Zongband.Engine.Boards;
using Zongband.Utils;

namespace Zongband.View.Boards
{
    public class BoardView : IBoardView
    {
        private readonly Tilemap tilemap;

        public BoardView(Tilemap tilemap)
        {
            this.tilemap = tilemap;
        }

        public void Modify(Coords at, ITerrain terrain)
        {
            if (terrain.Visuals is not TileBase tilebase)
            {
                Debug.Log(Warnings.UnexpectedVisualsObject);
                return;
            }

            var position = new Vector3Int(at.X, at.Y, 0);
            tilemap.SetTile(position, tilebase);
        }
    }
}
