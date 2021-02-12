#nullable enable

using UnityEngine;
using System.Collections.Generic;

using Zongband.Game.Controllers;
using Zongband.Game.Actions;
using Zongband.Game.Turns;
using Zongband.Game.Boards;
using Zongband.Game.Entities;
using Zongband.Utils;

namespace Zongband.Game.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private BoardSO? boardSO;
        [SerializeField] private TileSO? floorTileSO;
        [SerializeField] private TileSO? wallTileSO;
        [SerializeField] private AgentSO? playerAgentSO;
        [SerializeField] private AgentSO? fastAgentSO;
        [SerializeField] private AgentSO? normalAgentSO;
        [SerializeField] private AgentSO? slowAgentSO;
        [SerializeField] private EntitySO? boxEntitySO;

        public PlayerController? playerController;
        public AIController? aiController;
        public TurnManager? turnManager;
        public Board? board;

        public Agent? LastPlayer { get; private set; }

        private Action currentAction = new NullAction();

        public void SetupExample()
        {
            if (boardSO == null) return;
            if (floorTileSO == null) return;
            if (wallTileSO == null) return;
            if (playerAgentSO == null) return;
            if (fastAgentSO == null) return;
            if (normalAgentSO == null) return;
            if (slowAgentSO == null) return;
            if (boxEntitySO == null) return;

            if (playerController == null) return;
            if (aiController == null) return;
            if (turnManager == null) return;
            if (board == null) return;

            var newAction = new ParallelAction();

            var playerAction = new SequentialAction();
            // TODO: fix verbose
            var spawnPlayerAction = new SpawnAction(playerAgentSO, board, turnManager, new Location(3, 3), true);
            playerAction.Add(spawnPlayerAction);
            playerAction.Add(new MakePlayerAction(spawnPlayerAction));
            newAction.Add(playerAction);

            newAction.Add(new SpawnAction(fastAgentSO, board, turnManager, new Location(3, 5)));
            newAction.Add(new SpawnAction(normalAgentSO, board, turnManager, new Location(4, 5)));
            newAction.Add(new SpawnAction(normalAgentSO, board, turnManager, new Location(5, 5)));
            newAction.Add(new SpawnAction(slowAgentSO, board, turnManager, new Location(6, 3)));
            newAction.Add(new SpawnAction(boxEntitySO, board, turnManager, new Location(3, 7)));

            currentAction = newAction;

            // TODO: maybe terrain modification should also be done through actions
            var upRight = new Location(board.Size.x - 1, board.Size.y - 1);
            var downRight = new Location(board.Size.x - 1, 0);
            var downLeft = Location.Zero;
            var upLeft = new Location(0, board.Size.y - 1);
            board.ModifyBoxTerrain(downLeft, upRight, floorTileSO);
            board.ModifyBoxTerrain(upLeft, upRight + new Location(0, -1), wallTileSO);
            board.ModifyBoxTerrain(upRight, downRight + new Location(-1, 0), wallTileSO);
            board.ModifyBoxTerrain(downRight, downLeft + new Location(0, 1), wallTileSO);
            board.ModifyBoxTerrain(downLeft, upLeft + new Location(1, 0), wallTileSO);
        }

        public void GameLoop()
        {
            if (currentAction.IsCompleted) currentAction = ProcessTurns();
            else currentAction.Process();
        }

        public Action ProcessTurns()
        {
            if (playerController == null) return new NullAction();
            if (aiController == null) return new NullAction();
            if (turnManager == null) return new NullAction();
            if (board == null) return new NullAction();

            var turnAction = new ParallelAction();
            var processedAgents = new HashSet<Agent>();
            Agent? agent;
            while (((agent = turnManager.GetCurrent()) != null) && !processedAgents.Contains(agent))
            {
                Action? agentAction;
                if (agent.isPlayer)
                {
                    LastPlayer = agent;
                    agentAction = playerController.ProduceAction(agent, board);
                }
                else agentAction = aiController.ProduceAction(agent, board);

                if (agentAction == null) break;

                agentAction.Process();

                if (!agentAction.IsCompleted) turnAction.Add(agentAction);

                processedAgents.Add(agent);
                turnManager.Next();

                // TODO: if (actionPack.AreGameActionsLeft()) break;
            }

            return turnAction;
        }
    }
}
