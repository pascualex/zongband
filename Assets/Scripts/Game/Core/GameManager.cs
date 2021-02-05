using UnityEngine;
using System;
using System.Collections.Generic;

using Zongband.Game.AI;
using Zongband.Game.Boards;
using Zongband.Game.Turns;
using Zongband.Game.Actions;
using Zongband.Game.Entities;
using Zongband.Utils;

namespace Zongband.Game.Core
{
    public class GameManager : MonoBehaviour, ICustomStartable, ICustomUpdatable
    {
        public Agent playerAgentPrefab;
        public Agent fastAgentPrefab;
        public Agent normalAgentPrefab;
        public Agent slowAgentPrefab;
        public Entity entityPrefab;
        public TileSO floorTile;
        public TileSO wallTile;

        public Board board;
        public TurnManager turnManager;
        public AgentAI aiController;

        public Agent playerAgent { get; private set; }

        private ActionPack playerActionPack;
        private ActionPack currentActionPack;

        public GameManager()
        {
            playerActionPack = null;
            currentActionPack = new NullActionPack();
        }

        private void Awake()
        {
            if (playerAgentPrefab == null) throw new NullReferenceException();
            if (fastAgentPrefab == null) throw new NullReferenceException();
            if (normalAgentPrefab == null) throw new NullReferenceException();
            if (slowAgentPrefab == null) throw new NullReferenceException();
            if (entityPrefab == null) throw new NullReferenceException();
            if (floorTile == null) throw new NullReferenceException();
            if (wallTile == null) throw new NullReferenceException();

            if (board == null) throw new NullReferenceException();
            if (turnManager == null) throw new NullReferenceException();
            if (aiController == null) throw new NullReferenceException();
        }

        public void CustomStart()
        {
            Entity playerEntityPrefab = playerAgentPrefab.GetEntity();
            playerAgent = Spawn(playerEntityPrefab, new Vector2Int(3, 3), true).GetAgent();
            Spawn(fastAgentPrefab.GetEntity(), new Vector2Int(3, 5));
            Spawn(normalAgentPrefab.GetEntity(), new Vector2Int(4, 5));
            Spawn(normalAgentPrefab.GetEntity(), new Vector2Int(5, 5));
            Spawn(slowAgentPrefab.GetEntity(), new Vector2Int(6, 5));
            Spawn(entityPrefab, new Vector2Int(3, 7));

            Vector2Int upRight = board.size - Vector2Int.one;
            Vector2Int downRight = new Vector2Int(board.size.x - 1, 0);
            Vector2Int downLeft = Vector2Int.zero;
            Vector2Int upLeft = new Vector2Int(0, board.size.y - 1);
            board.ModifyBoxTerrain(downLeft, upRight, floorTile);
            board.ModifyBoxTerrain(upLeft, upRight + new Vector2Int(0, -1), wallTile);
            board.ModifyBoxTerrain(upRight, downRight + new Vector2Int(-1, 0), wallTile);
            board.ModifyBoxTerrain(downRight, downLeft + new Vector2Int(0, 1), wallTile);
            board.ModifyBoxTerrain(downLeft, upLeft + new Vector2Int(1, 0), wallTile);
        }

        public void CustomUpdate()
        {
            if (!currentActionPack.IsCompleted()) UpdateCurrentActionPack();
            else if (!IsPlayerTurn() || IsPlayerActionPackAvailable()) ProcessTurns();
        }

        public bool CanSetPlayerActionPack()
        {
            return IsPlayerTurn() && currentActionPack.IsCompleted();
        }

        public void SetPlayerActionPack(ActionPack actionPack)
        {
            if (!CanSetPlayerActionPack()) throw new IsNotPlayerTurnException();

            playerActionPack = actionPack;
        }

        private bool IsPlayerTurn()
        {
            if (playerAgent == null) throw new NullReferenceException();

            return turnManager.GetCurrent() == playerAgent;
        }

        private bool IsPlayerActionPackAvailable()
        {
            return (playerActionPack != null);
        }

        private ActionPack ConsumePlayerActionPack()
        {
            if (!IsPlayerActionPackAvailable()) throw new NullReferenceException();
            ActionPack consumedPlayerActionPack = playerActionPack;
            playerActionPack = null;
            return consumedPlayerActionPack;
        }

        private void ProcessTurns()
        {
            // TODO: custom exception
            if (!currentActionPack.IsCompleted()) throw new Exception();

            SequentialActionPack nextActionPack = new SequentialActionPack();

            HashSet<Agent> processedAgents = new HashSet<Agent>();
            do
            {
                Agent agent = turnManager.GetCurrent();

                if (processedAgents.Contains(agent)) break;

                ActionPack actionPack;
                if (IsPlayerTurn()) actionPack = ConsumePlayerActionPack();
                else actionPack = aiController.GenerateActionPack(agent, board);

                while (actionPack.IsActionAvailable())
                {
                    Actions.Action action = actionPack.ConsumeAction();
                    ApplyAction((PositionAction)action);
                }

                if (!actionPack.IsCompleted())
                {
                    nextActionPack.Add(actionPack);
                    // TODO: stop if GameActions left
                }

                processedAgents.Add(agent);
                turnManager.Next();
            }
            while (!IsPlayerTurn());

            this.currentActionPack = nextActionPack;
        }

        private void UpdateCurrentActionPack()
        {
            while (currentActionPack.IsActionAvailable())
            {
                Actions.Action action = currentActionPack.ConsumeAction();
                ApplyAction((PositionAction)action);
                Debug.Log("Never called");
            }

            if (!currentActionPack.IsCompleted()) currentActionPack.CustomUpdate();
        }

        private void ApplyAction(PositionAction positionAction)
        {
            if (positionAction == null) throw new ArgumentNullException();

            if (positionAction.absolute) board.Move(positionAction.entity, positionAction.position);
            else board.Displace(positionAction.entity, positionAction.position);
        }

        private Entity Spawn(Entity entityPrefab, Vector2Int at, bool priority)
        {
            if (entityPrefab == null) throw new ArgumentNullException();
            if (!board.IsPositionValid(at)) throw new ArgumentOutOfRangeException();

            Entity entity = Instantiate(entityPrefab, turnManager.transform);
            board.Add(entity, at);

            Vector2Int position = entity.position;
            float scale = board.scale;
            Vector3 relativePosition = new Vector3(position.x + 0.5f, 0, position.y + 0.5f) * scale;
            Vector3 absolutePosition = board.transform.position + relativePosition; 
            entity.transform.position = absolutePosition;

            if (entity.IsAgent()) turnManager.Add(entity.GetAgent(), priority);

            return entity;
        }

        private Entity Spawn(Entity entityPrefab, Vector2Int at)
        {
            return Spawn(entityPrefab, at, false);
        }
    }
}
