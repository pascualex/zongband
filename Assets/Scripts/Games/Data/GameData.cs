#nullable enable // TODO: Remove all

using UnityEngine;
using UnityEngine.Tilemaps;

using Zongband.Games.Core;
using Zongband.Games.Core.Boards;
using Zongband.Games.Data.Boards;
using Zongband.Utils;

namespace Zongband.Games.Data
{
    [CreateAssetMenu(fileName = "Game", menuName = "ScriptableObjects/Game")]
    public class GameData : ScriptableObject, IGameData<TileBase>
    {
        public IBoardData<TileBase> Board => _Board.Value();
        public BoardData? _Board;

        // public AgentSO? PlayerAgentSO;
        // public AgentSO?[] EnemiesSOs = new AgentSO[0];
        // public EntitySO? BoxEntitySO;
        // public readonly MoveAction.Parameters DefaultMovement = new();
        // public AbilitySO? TestAbilitySO;
        // public DungeonSO? DungeonSO;
        // public BoardSO? BoardSO;
        // public UnityEngine.Tilemaps.Tilemap? TerrainTilemap;

        // public Agent? AgentPrefab;
        // public Entity? EntityPrefab;
    }
}
