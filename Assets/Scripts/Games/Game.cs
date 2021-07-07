using Zongband.Games.Boards;

namespace Zongband.Games
{
    public class Game<T>
    {
        // public readonly PlayerController PlayerController;
        // public readonly AIController AIController;
        // public readonly Pathfinder Pathfinder;
        // public readonly DungeonGenerator DungeonGenerator;
        // public readonly TurnManager TurnManager;
        private readonly Board<T> Board;
        // public readonly Action.Context Ctx;
        // public Agent? LastPlayer { get; private set; }

        // private Action MainAction = new NullAction();

        public Game(IGameContent<T> content, IGameView<T> view)
        {
            // var defaultMovement = gameSO.DefaultMovement;
            // var testAbilitySO = gameSO.TestAbilitySO.Value();
            // PlayerController = new PlayerController(defaultMovement, testAbilitySO);
            // AIController = new AIController(defaultMovement);
            // Pathfinder = new Pathfinder();
            // DungeonGenerator = new DungeonGenerator();
            // TurnManager = new TurnManager();
            Board = new(content.BoardSize, content.DefaultTerrainType, view.Board);
            // var agentPrefab = gameSO.AgentPrefab.Value();
            // var entityPrefab = gameSO.EntityPrefab.Value();
            // Ctx = new Action.Context(TurnManager, Board, agentPrefab, entityPrefab);

            // SetupExample1(gameSO);
        }

        // private void SetupExample1(GameSO gameSO)
        // {
        //     var newAction = new ParallelAction();

        //     var playerAction = new SequentialAction();
        //     var playerAgentSO = gameSO.PlayerAgentSO.Value();
        //     var spawnPlayerAction = new SpawnAction(playerAgentSO, new Tile(3, 3), Ctx, true);
        //     playerAction.Add(spawnPlayerAction);
        //     playerAction.Add(new ControlAction(spawnPlayerAction.Agent, true));
        //     newAction.Add(playerAction);

        //     for (var i = 0; i < gameSO.EnemiesSOs.Length; i++)
        //     {
        //         var enemySO = gameSO.EnemiesSOs[i];
        //         if (enemySO == null) continue;
        //         newAction.Add(new SpawnAction(enemySO, new Tile(3 + i, 7), Ctx));
        //     }

        //     var boxEntitySO = gameSO.BoxEntitySO.Value();
        //     newAction.Add(new CreateAction(boxEntitySO, new Tile(3, 9), Ctx));

        //     MainAction = newAction;
        // }

        // private void SetupExample2(GameSO gameSO)
        // {
        //     var newAction = new ParallelAction();

        //     var dungeonSO = gameSO.DungeonSO.Value();
        //     var dungeonData = DungeonGenerator.GenerateDungeon(dungeonSO);
        //     // TODO: DungeonVisualizer.DungeonData = dungeonData;
        //     if (dungeonData == null) throw new NullReferenceException();

        //     Board.Apply(dungeonData.ToBoardData());

        //     var playerAction = new SequentialAction();
        //     var playerAgentSO = gameSO.PlayerAgentSO.Value();
        //     var playerSpawn = dungeonData.PlayerSpawn;
        //     var spawnPlayerAction = new SpawnAction(playerAgentSO, playerSpawn, Ctx, true);
        //     playerAction.Add(spawnPlayerAction);
        //     playerAction.Add(new ControlAction(spawnPlayerAction.Agent, true));
        //     newAction.Add(playerAction);

        //     if (gameSO.EnemiesSOs.Length > 0)
        //     {
        //         foreach (var spawn in dungeonData.EnemiesSpawn)
        //         {
        //             var randomIndex = Random.Range(0, gameSO.EnemiesSOs.Length);
        //             var enemySO = gameSO.EnemiesSOs[randomIndex];
        //             if (enemySO == null) continue;
        //             newAction.Add(new SpawnAction(enemySO, spawn, Ctx));
        //         }
        //     }

        //     MainAction = newAction;
        // }

        public void GameLoop()
        {
            // if (MainAction.IsCompleted) MainAction = ProcessTurns();
            // else MainAction.Execute();
        }

        // private Action ProcessTurns()
        // {
        //     var turnAction = new ParallelAction();
        //     var processed = new HashSet<Agent>();

        //     Agent? agent;
        //     while (((agent = TurnManager.GetCurrent()) != null) && !processed.Contains(agent))
        //     {
        //         if (agent.IsPlayer) LastPlayer = agent;
        //         Controller controller = agent.IsPlayer ? PlayerController : AIController;
        //         var agentAction = controller.ProduceAction(agent, Ctx);

        //         if (agentAction == null) break;

        //         agentAction.Execute();

        //         if (!agentAction.IsCompleted) turnAction.Add(agentAction);

        //         processed.Add(agent);
        //         TurnManager.Next();

        //         if (agentAction is not MoveAction and not NullAction) break;
        //     }

        //     return turnAction;
        // }
    }
}
