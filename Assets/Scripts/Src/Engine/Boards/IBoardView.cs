using Zongband.Engine.Entities;
using Zongband.Utils;

namespace  Zongband.Engine.Boards
{
    public interface IBoardView
    {
        void Add(Entity entity, Coords at);
        void Move(Entity entity, Coords to);
        void Remove(Entity entity);
        void Modify(Coords at, ITerrain terrain);
    }
}
