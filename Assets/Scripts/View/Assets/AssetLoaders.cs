using Zongband.Utils;

using RLEngine.Core.Boards;
using RLEngine.Core.Entities;
using RLEngine.Core.Utils;

using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

namespace Zongband.View.Assets
{
    public class AssetLoaders
    {
        public AssetLoader<TileType, TileBase> TileType { get; } = new();
        public AssetLoader<EntityType, GameObject> EntityType { get; } = new();
    }
}
