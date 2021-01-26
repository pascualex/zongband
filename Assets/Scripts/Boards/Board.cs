using UnityEngine;

using Zongband.Entities;
using Zongband.Utils;

namespace Zongband.Boards {
    public class Board : MonoBehaviour {
        public BoardSO boardData;

        public Vector2Int size { get; private set; }
        public float scale { get; private set; }

        private EntityLayer<Agent> agentLayer;
        private EntityLayer<Entity> entityLayer;
        
        private void Awake() {
            if (boardData == null) throw new ScriptableObjectMissingException();

            size = boardData.size;
            scale = boardData.scale;

            agentLayer = new EntityLayer<Agent>(size, scale);
            entityLayer = new EntityLayer<Entity>(size, scale);
        }

        public void Add(Agent agent, Vector2Int at)
        {
            if (!IsPositionAvailable(agent, at)) throw new NotEmptyCellException();

            agentLayer.Add(agent, at);
        }

        public void Add(Entity entity, Vector2Int at)
        {
            if (!IsPositionAvailable(entity, at)) throw new NotEmptyCellException();

            entityLayer.Add(entity, at);
        }

        public void Move(Agent agent, Vector2Int to)
        {
            if (!IsPositionAvailable(agent, to)) throw new NotEmptyCellException();

            agentLayer.Move(agent, to);
        }

        public void Move(Entity entity, Vector2Int to)
        {
            if (!IsPositionAvailable(entity, to)) throw new NotEmptyCellException();
            
            entityLayer.Move(entity, to);
        }

        public void Displace(Agent agent, Vector2Int delta)
        {
            if (!IsDisplacementAvailable(agent, delta)) throw new NotEmptyCellException();

            agentLayer.Displace(agent, delta);
        }

        public void Displace(Entity entity, Vector2Int delta)
        {
            if (!IsDisplacementAvailable(entity, delta)) throw new NotEmptyCellException();

            entityLayer.Displace(entity, delta);
        }

        public void Remove(Agent agent)
        {
            agentLayer.Remove(agent);
        }

        public void Remove(Entity entity)
        {
            entityLayer.Remove(entity);
        }

        public bool IsPositionValid(Vector2Int position)
        {
            return Checker.Range(position, size);
        }

        public bool IsPositionEmpty(Vector2Int position)
        {
            if (!agentLayer.IsPositionEmpty(position)) return false;
            if (!entityLayer.IsPositionEmpty(position)) return false;
            return true;
        }

        public bool IsPositionAvailable(Agent agent, Vector2Int position)
        {
            if (!IsPositionValid(position)) return false;
            if (!agentLayer.IsPositionEmpty(position)) return false;
            /* Add here special interactions in the future */
            if (!entityLayer.IsPositionEmpty(position)) return false;
            return true;
        }

        public bool IsPositionAvailable(Entity entity, Vector2Int position)
        {
            if (!IsPositionValid(position)) return false;
            if (!entityLayer.IsPositionEmpty(position)) return false;
            /* Add here special interactions in the future */
            if (!agentLayer.IsPositionEmpty(position)) return false;
            return true;
        }

        public bool IsDisplacementAvailable(Agent agent, Vector2Int delta)
        {
            if (!agentLayer.CheckEntityPosition(agent)) throw new NotInPositionException();
            
            return IsPositionAvailable(agent, agent.position + delta);
        }

        public bool IsDisplacementAvailable(Entity entity, Vector2Int delta)
        {
            if (!entityLayer.CheckEntityPosition(entity)) throw new NotInPositionException();

            return IsPositionAvailable(entity, entity.position + delta);
        }
    }
}