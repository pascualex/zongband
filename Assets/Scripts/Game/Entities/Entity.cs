#nullable enable

using UnityEngine;
using System;

namespace Zongband.Game.Entities
{
    public class Entity : MonoBehaviour
    {
        public Vector2Int position = new Vector2Int(-1, -1);
        public bool removed = false;

        private GameObject? gameModel;

        public void ApplySO(EntitySO entitySO)
        {
            if (gameModel != null) Destroy(gameModel);
            gameModel = Instantiate(entitySO.gameModel);
        }
    }
}
