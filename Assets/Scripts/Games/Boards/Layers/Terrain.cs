namespace Zongband.Games.Boards
{
    public class Terrain<T>
    {
        public ITerrainType<T> Type;

        public Terrain(ITerrainType<T> defaultType)
        {
            Type = defaultType;
        }
    }
}