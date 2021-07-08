using System;

using Zongband.Utils;

namespace Zongband.Games.Boards
{
    public class TerrainLayer : Layer
    {
        private readonly Terrain[][] Terrains;
        private readonly ITerrainLayerView View;

        public TerrainLayer(Size size, ITerrainType defaultType, ITerrainLayerView view)
        : base(size)
        {
            View = view;
            Terrains = new Terrain[Size.Y][];
            for (var i = 0; i < Size.Y; i++)
            {
                Terrains[i] = new Terrain[Size.X];
                for (var j = 0; j < Size.X; j++)
                {
                    Terrains[i][j] = new Terrain(defaultType);
                    View.Modify(new Tile(j, i), defaultType.Visuals);
                }
            }
        }

        public void Modify(Tile at, ITerrainType type)
        {
            if (!Size.Contains(at)) throw new ArgumentOutOfRangeException();

            Terrains[at.Y][at.X].Type = type;
            View.Modify(at, type.Visuals);
        }

        public Terrain GetTile(Tile at)
        {
            if (!Size.Contains(at)) throw new ArgumentOutOfRangeException();

            return Terrains[at.Y][at.X];
        }
    }
}
