using UnityEngine;
using UnityEngine.Events;

using Zongband.Boards;
using Zongband.Actions;
using Zongband.Entities;

namespace Zongband.Player
{
    public class PlayerAgentController : MonoBehaviour
    {
        public bool isPlayerTurn { get; private set; }
        public bool isActionPackReady { get; private set; }
        public Agent agent { get; private set; }
        public Board board { get; private set; }

        private ActionPack actionPack;
        private Object locker;

        public PlayerAgentController()
        {
            isPlayerTurn = false;
            isActionPackReady = false;
            locker = new Object();
        }

        public void StartTurn(Agent agent, Board board)
        {
            lock (locker)
            {
                if (isPlayerTurn) throw new PlayerTurnException();

                isPlayerTurn = true;
                isActionPackReady = false;
                this.agent = agent;
                this.board = board;
            }
        }

        public ActionPack EndTurn()
        {
            lock (locker)
            {
                if (!isPlayerTurn) throw new PlayerTurnException();
                if (!isActionPackReady) throw new ActionPackReadyException();

                isPlayerTurn = false;
                return actionPack;
            }
        }

        public void AttemptDisplacement(Vector2Int delta)
        {
            lock (locker)
            {
                if (!isPlayerTurn || isActionPackReady) return;

                if (!board.IsDisplacementAvailable(agent.GetEntity(), delta)) return;

                ActionPack actionPack = new ActionPack();
                MovementAction movementAction = new MovementAction(agent.GetEntity(), delta);
                actionPack.AddMovementAction(movementAction);

                SetActionPack(actionPack);
            }
        }

        private void SetActionPack(ActionPack actionPack)
        {
            if (!isPlayerTurn) throw new PlayerTurnException();
            if (isActionPackReady) throw new ActionPackReadyException();

            this.actionPack = actionPack;
            isActionPackReady = true;
        }
    }
}
