using RLEngine.Core.Logs;

using DG.Tweening;

namespace Zongband.View.Actions
{
    public partial class VisualActionExecutor
    {
        public Sequence Heal(HealingLog log)
        {
            return DOTween.Sequence();
        }
    }
}
