using UnityEngine;
using System;

using Zongband.Game.Entities;
using Zongband.Utils;

namespace Zongband.Game.Boards
{
    public class TerrainLayer : Layer
    {
        private TileSO[][] tiles;

        public TerrainLayer(Vector2Int size, float scale) : base(size, scale)
        {
            TileSO defaultTile = ScriptableObject.CreateInstance<TileSO>();

            tiles = new TileSO[size.y][];
            for (int i = 0; i < size.y; i++)
            {
                tiles[i] = new TileSO[size.x];
                for (int j = 0; j < size.x; j++)
                {
                    tiles[i][j] = defaultTile;
                }
            }
        }

        public void Modify(Vector2Int position, TileSO tile)
        {
            if (!IsPositionValid(position)) throw new ArgumentOutOfRangeException();

            tiles[position.y][position.x] = tile;
        }

        public TileSO GetTile(Vector2Int position)
        {
            if (!IsPositionValid(position)) throw new ArgumentOutOfRangeException();

            return tiles[position.y][position.x];
        }
    }
}
