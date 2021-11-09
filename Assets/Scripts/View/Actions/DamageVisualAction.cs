using RLEngine.Core.Logs;

using DG.Tweening;

namespace Zongband.View.Actions
{
    public partial class VisualActionExecutor
    {
        public Sequence Damage(DamageLog log)
        {
            return DOTween.Sequence();
        }
    }
}
