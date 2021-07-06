namespace Zongband.Games.Core.Boards
{
    public interface IBoardView<T>
    {
        ITerrainLayerView<T> TerrainLayerView { get; }
    }
}
