#nullable enable

using Zongband.Games.Core.Boards;

namespace Zongband.Games.Core
{
    public interface IGameView<T>
    {
        IBoardView<T> Board { get; }
    }
}
