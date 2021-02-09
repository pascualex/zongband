#nullable enable

using UnityEngine;
using UnityEngine.Tilemaps;

namespace Zongband.Game.Boards
{
    public class Tile
    {
        public bool BlocksGround { get; private set; } = false;
        public bool BlocksAir { get; private set; } = false;
        public TileBase? TileBase { get; private set; }

        public void ApplySO(TileSO tileSO)
        {
            BlocksGround = tileSO.blocksGround;
            BlocksAir = tileSO.blocksGround;
            TileBase = tileSO.tileBase;
        }
    }
}