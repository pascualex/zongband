#nullable enable

using UnityEngine;
using System;

using Zongband.Utils;

namespace Zongband.Game.Boards
{
    public class TerrainLayer : Layer
    {
        private Terrain[][] terrains = new Terrain[0][];

        public override void ChangeSize(Size size)
        {
            base.ChangeSize(size);

            terrains = new Terrain[Size.y][];
            for (var i = 0; i < Size.y; i++)
            {
                terrains[i] = new Terrain[Size.x];
                for (var j = 0; j < Size.x; j++)
                {
                    terrains[i][j] = new Terrain();
                }
            }
        }

        public void Modify(Tile at, TerrainSO terrainSO)
        {
            if (!Size.Contains(at)) throw new ArgumentOutOfRangeException();

            terrains[at.y][at.x].ApplySO(terrainSO);
        }

        public Terrain GetTile(Tile at)
        {
            if (!Size.Contains(at)) throw new ArgumentOutOfRangeException();

            return terrains[at.y][at.x];
        }
    }
}
