using UnityEngine;
using System;

using Zongband.Boards;
using Zongband.Turns;
using Zongband.AI;
using Zongband.Actions;
using Zongband.Entities;

namespace Zongband.Core
{
    public class GameManager : MonoBehaviour
    {
        public Agent playerPrefab;
        public Agent agentPrefab;
        public Entity entityPrefab;
        public TileSO floorTile;
        public TileSO wallTile;
        public Board board;
        public TurnManager turnManager;
        public AgentAI agentAI;

        private Agent playerAgent;

        private void Start()
        {
            playerAgent = Spawn(playerPrefab.GetEntity(), new Vector2Int(3, 3)).GetAgent();
            Spawn(agentPrefab.GetEntity(), new Vector2Int(5, 3)); ;
            Spawn(entityPrefab, new Vector2Int(5, 5));

            Vector2Int upRight = board.size - Vector2Int.one;
            Vector2Int downRight = new Vector2Int(board.size.x - 1, 0);
            Vector2Int downLeft = Vector2Int.zero;
            Vector2Int upLeft = new Vector2Int(0, board.size.y - 1);
            board.ModifyBoxTerrain(downLeft, upRight, floorTile);
            board.ModifyBoxTerrain(upLeft, upRight + new Vector2Int(0, -1), wallTile);
            board.ModifyBoxTerrain(upRight, downRight + new Vector2Int(-1, 0), wallTile);
            board.ModifyBoxTerrain(downRight, downLeft + new Vector2Int(0, 1), wallTile);
            board.ModifyBoxTerrain(downLeft, upLeft + new Vector2Int(1, 0), wallTile);

            ResolveTurns();
        }

        public void ResolveTurns()
        {
            if (playerAgent == null) throw new NullReferenceException();

            while (turnManager.GetCurrent() != playerAgent)
            {
                Agent agent = turnManager.GetCurrent();

                ActionPack actionPack = agentAI.GenerateActionPack(agent, board);
                ConsumeActionPack(actionPack);

                turnManager.NextTurn();
            }
        }

        public void ConsumeActionPack(ActionPack actionPack)
        {
            if (actionPack == null) throw new ArgumentNullException(); 

            foreach (MovementAction movementAction in actionPack.GetMovementActions()) {
                ConsumeAction(movementAction);
            }
        }

        public void ConsumeAction(MovementAction movementAction)
        {
            if (movementAction == null) throw new ArgumentNullException();

            if (movementAction.absolute) board.Move(movementAction.entity, movementAction.movement);
            else board.Displace(movementAction.entity, movementAction.movement);
        }

        public Entity Spawn(Entity entityPrefab, Vector2Int at)
        {
            if (entityPrefab == null) throw new ArgumentNullException();
            if (!board.IsPositionValid(at)) throw new ArgumentOutOfRangeException();

            Entity entity = Instantiate(entityPrefab);
            board.Add(entity, at);
            if (entity.IsAgent()) turnManager.Add(entity.GetAgent());
            return entity;
        }

        public void AttemptMovePlayer(Vector2Int movement)
        {
            if (!board.IsDisplacementAvailable(playerAgent.GetEntity(), movement)) return;
            // This check is only temporal and is not thread friendly            
            if (turnManager.GetCurrent() != playerAgent) return;
            MovePlayer(movement);
        }

        public void MovePlayer(Vector2Int movement)
        {
            board.Displace(playerAgent.GetEntity(), movement);
            turnManager.NextTurn();
            ResolveTurns();
        }
    }
}
