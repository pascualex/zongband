#nullable enable

using UnityEngine;

using Zongband.Game.Boards;
using Zongband.Game.Turns;
using Zongband.Game.Actions;
using Zongband.Game.Entities;
using Zongband.Utils;

namespace Zongband.Game.Core
{
    public class GameManager : MonoBehaviour, ICustomStartable, ICustomUpdatable
    {
        [SerializeField] private AgentSO? playerAgentSO;
        [SerializeField] private AgentSO? fastAgentSO;
        [SerializeField] private AgentSO? normalAgentSO;
        [SerializeField] private AgentSO? slowAgentSO;
        [SerializeField] private EntitySO? boxEntitySO;
        [SerializeField] private TileSO? floorTile;
        [SerializeField] private TileSO? wallTile;
        [SerializeField] private BoardSO? boardSO;

        public readonly Board? board;
        public readonly TurnManager? turnManager;
        [SerializeField] private ActionProducer? actionProducer;
        [SerializeField] private ActionConsumer? actionConsumer;

        public Agent? PlayerAgent { get; set; } // TODO: remove

        public void CustomStart()
        {
            if (playerAgentSO == null) return;
            if (fastAgentSO == null) return;
            if (normalAgentSO == null) return;
            if (slowAgentSO == null) return;
            if (boxEntitySO == null) return;
            if (floorTile == null) return;
            if (wallTile == null) return;
            if (boardSO == null) return;

            if (board == null) return;
            if (actionProducer == null) return;
            if (actionConsumer == null) return;

            var actionPack = new ParallelActionPack();

            var playerActionPack = new SequentialActionPack();
            var position = new Vector2Int(3, 3);
            var spawnPlayer = new SpawnAction(playerAgentSO, board, position, true);
            playerActionPack.Add(spawnPlayer);
            playerActionPack.Add(new MakePlayerAction(spawnPlayer));
            actionPack.Add(playerActionPack);

            actionPack.Add(new SpawnAction(fastAgentSO, board, new Vector2Int(3, 5)));
            actionPack.Add(new SpawnAction(normalAgentSO, board, new Vector2Int(4, 5)));
            actionPack.Add(new SpawnAction(normalAgentSO, board, new Vector2Int(5, 5)));
            actionPack.Add(new SpawnAction(slowAgentSO, board, new Vector2Int(6, 3)));
            actionPack.Add(new SpawnAction(boxEntitySO, board, new Vector2Int(3, 7)));

            actionConsumer.TryToConsumeActionPack(actionPack);
            if (!actionPack.IsCompleted()) actionConsumer.ConsumeTurnActionPack(actionPack);

            var upRight = board.Size - Vector2Int.one;
            var downRight = new Vector2Int(board.Size.x - 1, 0);
            var downLeft = Vector2Int.zero;
            var upLeft = new Vector2Int(0, board.Size.y - 1);
            board.ModifyBoxTerrain(downLeft, upRight, floorTile);
            board.ModifyBoxTerrain(upLeft, upRight + new Vector2Int(0, -1), wallTile);
            board.ModifyBoxTerrain(upRight, downRight + new Vector2Int(-1, 0), wallTile);
            board.ModifyBoxTerrain(downRight, downLeft + new Vector2Int(0, 1), wallTile);
            board.ModifyBoxTerrain(downLeft, upLeft + new Vector2Int(1, 0), wallTile);
        }

        public void CustomUpdate()
        {
            if (actionConsumer == null) return;
            if (actionProducer == null) return;

            if (!actionConsumer.IsCompleted())
            {
                actionConsumer.CustomUpdate();
            }
            else if (actionProducer.CanProduceTurnActionPack())
            {
                var turnActionPack = actionProducer.ProduceTurnActionPack();
                actionConsumer.ConsumeTurnActionPack(turnActionPack);
            }
        }

        public bool IsPlayerTurn()
        {
            if (actionConsumer == null) return false;
            if (actionProducer == null) return false;
            return actionProducer.IsPlayerTurn() && actionConsumer.IsCompleted();
        }

        public void SetPlayerActionPack(ActionPack actionPack)
        {
            if (!IsPlayerTurn()) throw new IsNotPlayerTurnException();

            actionProducer?.SetPlayerActionPack(actionPack);
        }
    }
}
