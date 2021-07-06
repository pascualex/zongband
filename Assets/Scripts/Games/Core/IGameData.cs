#nullable enable

using Zongband.Games.Core.Boards;
using Zongband.Utils;

namespace Zongband.Games.Core
{
    public interface IGameData<T>
    {
        IBoardData<T> Board { get; }
    }
}
