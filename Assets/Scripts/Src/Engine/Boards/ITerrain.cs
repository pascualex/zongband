namespace  Zongband.Engine.Boards
{
    public interface ITerrain
    {
        bool BlocksGround { get; }
        bool BlocksAir { get; }
        object? Visuals { get; }
    }
}