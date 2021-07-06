#nullable enable

using System;

using Zongband.Games.Core.Boards;
using Zongband.Utils;

namespace Zongband.Games.Logic.Boards
{
    public class TerrainLayer<T> : Layer
    {
        private readonly Terrain<T>[][] Terrains;
        private readonly ITerrainLayerView<T> View;

        public TerrainLayer(Size size, ITerrainTypeData<T> defaultType, ITerrainLayerView<T> view)
        : base(size)
        {
            View = view;
            Terrains = new Terrain<T>[Size.Y][];
            for (var i = 0; i < Size.Y; i++)
            {
                Terrains[i] = new Terrain<T>[Size.X];
                for (var j = 0; j < Size.X; j++)
                {
                    Terrains[i][j] = new Terrain<T>(defaultType);
                    View.Modify(new Tile(j, i), defaultType.Visuals);
                }
            }
        }

        public void Modify(Tile at, ITerrainTypeData<T> type)
        {
            if (!Size.Contains(at)) throw new ArgumentOutOfRangeException();

            Terrains[at.Y][at.X].Type = type;
            View.Modify(at, type.Visuals);
        }

        public Terrain<T> GetTile(Tile at)
        {
            if (!Size.Contains(at)) throw new ArgumentOutOfRangeException();

            return Terrains[at.Y][at.X];
        }
    }
}
