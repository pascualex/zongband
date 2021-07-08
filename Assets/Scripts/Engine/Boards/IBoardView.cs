using Zongband.Utils;

namespace  Zongband.Engine.Boards
{
    public interface IBoardView
    {
        void Modify(Coords at, ITerrain terrain);
    }
}
