#nullable enable

using Zongband.Utils;

namespace Zongband.Games.Logic.Boards
{
    public abstract class Layer
    {
        public readonly Size Size;

        public Layer(Size size)
        {
            Size = size;
        }
    }
}