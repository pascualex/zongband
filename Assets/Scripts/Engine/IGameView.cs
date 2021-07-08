using Zongband.Engine.Boards;

namespace  Zongband.Engine
{
    public interface IGameView
    {
        IBoardView Board { get; }
    }
}
