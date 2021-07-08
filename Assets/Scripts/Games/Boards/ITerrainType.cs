namespace Zongband.Games.Boards
{
    public interface ITerrainType
    {
        bool BlocksGround { get; }
        bool BlocksAir { get; }
        object Visuals { get; }
    }
}