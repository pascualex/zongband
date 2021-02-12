#nullable enable

using UnityEngine;
using System;

using Zongband.Utils;

namespace Zongband.Game.Boards
{
    public class TerrainLayer : Layer
    {
        private Tile[][] tiles = new Tile[0][];

        public override void ChangeSize(Size size)
        {
            base.ChangeSize(size);

            tiles = new Tile[Size.y][];
            for (var i = 0; i < Size.y; i++)
            {
                tiles[i] = new Tile[Size.x];
                for (var j = 0; j < Size.x; j++)
                {
                    tiles[i][j] = new Tile();
                }
            }
        }

        public void Modify(Location at, TileSO tileSO)
        {
            if (!Size.Contains(at)) throw new ArgumentOutOfRangeException();

            tiles[at.y][at.x].ApplySO(tileSO);
        }

        public Tile GetTile(Location at)
        {
            if (!Size.Contains(at)) throw new ArgumentOutOfRangeException();

            return tiles[at.y][at.x];
        }
    }
}
