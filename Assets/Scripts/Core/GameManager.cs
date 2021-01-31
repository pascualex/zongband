using UnityEngine;
using System;

using Zongband.Boards;
using Zongband.Turns;
using Zongband.Entities;

namespace Zongband.Core
{
    public class GameManager : MonoBehaviour
    {
        public Agent examplePlayerAgentPrefab;
        public Agent exampleAgentPrefab;
        public Entity exampleEntityPrefab;
        public TileSO floorTile;
        public TileSO wallTile;
        public Board board;
        public TurnManager turnManager;
        public AgentAI agentAI;

        private Agent playerAgent;

        private void Start()
        {
            playerAgent = Spawn(examplePlayerAgentPrefab, new Vector2Int(3, 3));
            Spawn(exampleAgentPrefab, new Vector2Int(5, 3)); ;
            Spawn(exampleEntityPrefab, new Vector2Int(5, 5));

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
                Debug.Log("Agent's turn");
                Agent agent = turnManager.GetCurrent();

                Vector2Int direction = agentAI.GenerateMovement(agent, board);
                board.Displace(agent, direction);

                turnManager.NextTurn();
            }

            Debug.Log("Player's turn");
        }

        public Entity Spawn(Entity entityPrefab, Vector2Int at)
        {
            if (entityPrefab == null) throw new ArgumentNullException();
            if (!board.IsPositionValid(at)) throw new ArgumentOutOfRangeException();

            Entity entity = Instantiate(entityPrefab);
            board.Add(entity, at);
            return entity;
        }

        public Agent Spawn(Agent agentPrefab, Vector2Int at)
        {
            if (agentPrefab == null) throw new ArgumentNullException();
            if (!board.IsPositionValid(at)) throw new ArgumentOutOfRangeException();

            Agent agent = Instantiate(agentPrefab);
            board.Add(agent, at);
            turnManager.Add(agent);
            return agent;
        }

        public void AttemptMovePlayer(Vector2Int movement)
        {
            if (!board.IsDisplacementAvailable(playerAgent, movement)) return;
            // This check is only temporal and is not thread friendly            
            if (turnManager.GetCurrent() != playerAgent) return;
            MovePlayer(movement);
        }

        public void MovePlayer(Vector2Int movement)
        {
            board.Displace(playerAgent, movement);
            turnManager.NextTurn();
            ResolveTurns();
        }
    }
}
