#nullable enable

using UnityEngine;

using Zongband.Utils;

namespace Zongband.Game.Boards
{
    public abstract class Layer
    {
        public Size Size { get; private set; } = Size.Zero;

        public virtual void ChangeSize(Size size)
        {
            Size = size;
        }
    }
}