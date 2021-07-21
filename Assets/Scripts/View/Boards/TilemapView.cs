using Zongband.Utils;

using RLEngine.Boards;
using RLEngine.Utils;

using UnityEngine;
using UnityEngine.Tilemaps;

using ANE = System.ArgumentNullException;

namespace Zongband.View.Boards
{
    public class TilemapView : MonoBehaviour
    {
        [SerializeField] private Tilemap? tilemap;

        public void Modify(ITileType tileType, Coords at)
        {
            if (tilemap is null) throw new ANE(nameof(tilemap));

            if (tileType.Visuals is not TileBase tilebase)
            {
                Debug.LogWarning(Warnings.VisualsType(tileType.Visuals, typeof(TileBase)));
                return;
            }

            var position = new Vector3Int(at.X, at.Y, 0);
            tilemap.SetTile(position, tilebase);
        }
    }
}
