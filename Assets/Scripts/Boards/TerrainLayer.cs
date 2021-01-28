using UnityEngine;
using System;

using Zongband.Entities;
using Zongband.Utils;

namespace Zongband.Boards
{
    public class TerrainLayer : Layer
    {
        private bool[][] walls;

        public TerrainLayer(Vector2Int size, float scale) : base(size, scale)
        {
            walls = new bool[size.y][];
            for (int i = 0; i < size.y; i++)
            {
                walls[i] = new bool[size.x];
            }
        }

        public void Modify(Vector2Int position, bool isWall)
        {
            if (!IsPositionValid(position)) throw new ArgumentOutOfRangeException();

            walls[position.y][position.x] = isWall;
        }

        public override bool IsPositionEmpty(Vector2Int position)
        {
            if (!IsPositionValid(position)) throw new ArgumentOutOfRangeException();

            return !walls[position.y][position.x];
        }
    }
}
