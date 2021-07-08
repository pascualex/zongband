namespace Zongband.Games.Boards
{
    public interface IBoardView
    {
        IEntityLayerView EntityLayerView { get; }
        ITerrainLayerView TerrainLayerView { get; }
    }
}
