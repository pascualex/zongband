#nullable enable

namespace Zongband.Games.Core.Boards
{
    public interface ITerrainTypeData<T>
    {
        bool BlocksGround { get; }
        bool BlocksAir { get; }
        T Visuals { get; }
    }
}