namespace Zongband.Games.Boards
{
    public interface ITerrainType<T>
    {
        bool BlocksGround { get; }
        bool BlocksAir { get; }
        T Visuals { get; }
    }
}