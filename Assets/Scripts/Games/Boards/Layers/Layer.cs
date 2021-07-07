using Zongband.Utils;

namespace Zongband.Games.Boards
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