using Zongband.Games.Boards;

namespace Zongband.Games
{
    public interface IGameView
    {
        IBoardView Board { get; }
    }
}
