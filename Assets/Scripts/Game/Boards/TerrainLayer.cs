#nullable enable

using UnityEngine;
using System;

namespace Zongband.Game.Boards
{
    public class TerrainLayer : Layer
    {
        private Tile[][] tiles = new Tile[0][];

        public override void ChangeSize(Vector2Int size)
        {
            base.ChangeSize(size);

            tiles = new Tile[size.y][];
            for (var i = 0; i < size.y; i++)
            {
                tiles[i] = new Tile[size.x];
                for (var j = 0; j < size.x; j++)
                {
                    tiles[i][j] = new Tile();
                }
            }
        }

        public void Modify(Vector2Int position, TileSO tileSO)
        {
            if (!IsPositionValid(position)) throw new ArgumentOutOfRangeException();

            tiles[position.y][position.x].ApplySO(tileSO);
        }

        public Tile GetTile(Vector2Int position)
        {
            if (!IsPositionValid(position)) throw new ArgumentOutOfRangeException();

            return tiles[position.y][position.x];
        }
    }
}
