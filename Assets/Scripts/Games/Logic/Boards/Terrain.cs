#nullable enable

using Zongband.Games.Core.Boards;

namespace Zongband.Games.Logic.Boards
{
    public class Terrain<T>
    {
        public ITerrainTypeData<T> Type;

        public Terrain(ITerrainTypeData<T> defaultType)
        {
            Type = defaultType;
        }
    }
}