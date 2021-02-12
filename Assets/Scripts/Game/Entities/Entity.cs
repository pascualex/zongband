#nullable enable

using UnityEngine;

using Zongband.Utils;

namespace Zongband.Game.Entities
{
    public class Entity : MonoBehaviour
    {
        public Location location = Location.MinusOne;
        public bool removed = false;

        private GameObject? gameModel;

        public void ApplySO(EntitySO entitySO)
        {
            name = entitySO.name;

            if (gameModel != null) Destroy(gameModel);
            
            // TODO: spawn something debug by default
            if (entitySO.gameModel == null) return;
            gameModel = Instantiate(entitySO.gameModel, transform);
            gameModel.name = "GameModel";
        }
    }
}
