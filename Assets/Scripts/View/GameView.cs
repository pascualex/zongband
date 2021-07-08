using UnityEngine;
using UnityEngine.Tilemaps;

using Zongband.View.Boards;
using Zongband.Games;
using Zongband.Games.Boards;

namespace Zongband.View
{
    public class GameView : IGameView
    {
        public IBoardView Board { get; }

        public GameView(Tilemap tilemap)
        {
            Board = new BoardView(tilemap);
        }
    }
}
