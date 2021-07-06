#nullable enable

using Zongband.Games.Core.Boards;

namespace Zongband.Games.Core
{
    public interface IGame
    {
        IBoard Board { get; }
    }
}
