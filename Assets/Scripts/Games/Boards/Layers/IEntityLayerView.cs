using Zongband.Games.Entities;
using Zongband.Utils;

namespace Zongband.Games.Boards
{
    public interface IEntityLayerView
    {
        void Add(object entityVisuals, Tile at);
    }
}
