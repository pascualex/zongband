using Zongband.Games.Boards;

namespace Zongband.Games
{
    public interface IGameView<T>
    {
        IBoardView<T> Board { get; }
    }
}
