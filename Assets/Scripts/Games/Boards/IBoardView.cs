namespace Zongband.Games.Boards
{
    public interface IBoardView<T>
    {
        ITerrainLayerView<T> TerrainLayerView { get; }
    }
}
