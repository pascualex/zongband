using Zongband.Utils;

namespace  Zongband.Engine.Boards
{
    public interface IReadOnlyBoard
    {
        Size Size { get; }

        IReadOnlyTile? GetTile(Coords at);
    }
}