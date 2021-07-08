using UnityEngine.Tilemaps;

using Zongband.View.Boards;
using Zongband.Engine;
using Zongband.Engine.Boards;

namespace Zongband.View
{
    public class GameView : IGameView
    {
        public IBoardView Board { get; private set; }

        public GameView(Tilemap tilemap)
        {
            Board = new BoardView(tilemap);
        }
    }
}
