using UnityEngine.Tilemaps;

using Zongband.Games.Core;
using Zongband.Games.Core.Boards;
using Zongband.Games.View.Boards;

namespace Zongband.Games.View
{
    public class GameView : IGameView<TileBase>
    {
        public IBoardView<TileBase> Board { get; }

        public GameView(Tilemap tilemap)
        {
            Board = new BoardView(tilemap);
        }
    }
}
