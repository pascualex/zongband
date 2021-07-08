namespace Zongband.Games.Boards
{
    public class Terrain
    {
        public ITerrainType Type;

        public Terrain(ITerrainType defaultType)
        {
            Type = defaultType;
        }
    }
}