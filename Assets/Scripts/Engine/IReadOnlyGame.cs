using Zongband.Engine.Boards;

namespace  Zongband.Engine
{
    public interface IReadOnlyGame
    {
        IReadOnlyBoard Board { get; }
    }
}
