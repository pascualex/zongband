using Zongband.Utils;

using RLEngine.Core.Logs;
using RLEngine.Core.Boards;
using RLEngine.Core.Utils;

using DG.Tweening;

namespace Zongband.View.Actions
{
    public partial class VisualActionExecutor
    {
        public Sequence Modify(ModificationLog log)
        {
            var sequence = DOTween.Sequence();

            Modify(log.At, log.NewType);

            return sequence;
        }

        public void Modify(Coords at, TileType tileType)
        {
            var tileBase = assetLoaders.TileType.Get(tileType);
            tilemap.SetTile(at.ToCell(), tileBase);
        }
    }
}
