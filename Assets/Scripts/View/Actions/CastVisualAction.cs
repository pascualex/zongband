using Zongband.Utils;

using RLEngine.Core.Logs;

using UnityEngine;
using DG.Tweening;

namespace Zongband.View.Actions
{
    public partial class VisualActionExecutor
    {
        public Sequence Cast(AbilityLog log)
        {
            var sequence = DOTween.Sequence();
            return sequence;
        }
    }
}
