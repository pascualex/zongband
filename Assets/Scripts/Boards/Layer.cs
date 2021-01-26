using UnityEngine;

namespace Zongband.Boards {
    public abstract class Layer {
        public Vector2Int size { get; private set; }
        public float scale { get; private set; }
        
        public Layer(Vector2Int size, float scale)
        {
            this.size = size;
            this.scale = scale;
        }
    }
}