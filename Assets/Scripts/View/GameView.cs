using RLEngine.Logs;
using RLEngine.State;
using RLEngine.Boards;
using RLEngine.Utils;

using UnityEngine;
using UnityEngine.Tilemaps;

using VE = Zongband.View.Exceptions.VisualsException;
using ANE = System.ArgumentNullException;

namespace Zongband.View
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private Tilemap? tilemap;

        public void Represent(GameState state)
        {
            for (var i = 0; i < state.Board.Size.Y; i++)
            {
                for (var j = 0; j < state.Board.Size.X; j++)
                {
                    var at = new Coords(j, i);
                    var tileType = state.Board.GetTileType(at);
                    if (tileType is null) continue;
                    ModifyBoard(tileType, at);
                }
            }
        }

        public void Represent(Log log)
        {
            if (log is ModifyLog modifyLog) RepresentLog(modifyLog);
        }

        private void RepresentLog(ModifyLog log)
        {
            ModifyBoard(log.NewType, log.At);
        }

        private void ModifyBoard(ITileType tileType, Coords at)
        {
            if (tilemap is null) throw new ANE(nameof(tilemap));

            var visuals = tileType.Visuals;
            if (visuals is not TileBase tilebase) throw VE.FromObject(visuals, typeof(TileBase));

            var position = new Vector3Int(at.X, at.Y, 0);
            tilemap.SetTile(position, tilebase);
        }
    }
}
