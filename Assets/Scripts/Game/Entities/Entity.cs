#nullable enable

using UnityEngine;

using Zongband.Utils;

namespace Zongband.Game.Entities
{
    public class Entity : MonoBehaviour
    {
        public GameObject? defaultGameModel;
        public Tile tile = Tile.MinusOne;
        public bool removed = false;

        private GameObject? gameModel;

        private void Awake()
        {
            if (defaultGameModel == null) return;

            gameModel = Instantiate(defaultGameModel, transform);
            gameModel.name = "GameModel";
        }

        public void ApplySO(EntitySO entitySO)
        {
            name = entitySO.name;

            if (gameModel != null) Destroy(gameModel);

            if (entitySO.gameModel != null) gameModel = Instantiate(entitySO.gameModel, transform);
            else if (defaultGameModel != null) gameModel = Instantiate(defaultGameModel, transform);

            if (gameModel != null) gameModel.name = "GameModel";
        }
    }
}
