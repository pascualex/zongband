using UnityEngine;
using UnityEngine.InputSystem;
using System;

using Zongband.Game.Core;
using Zongband.Game.Actions;
using Zongband.Player;

namespace Zongband.Core
{
    public class CoreManager : MonoBehaviour
    {
        public GameManager gameManager;
        public PlayerController playerController;

        private void Awake()
        {
            if (gameManager == null) throw new NullReferenceException();
        }

        private void Start()
        {
            gameManager.SetupExample();
        }

        private void Update()
        {
            playerController.ClearActionPack();
            InputSystem.Update();
            if (playerController.ActionPackAvailable())
            {
                ActionPack actionPack = playerController.ConsumeActionPack();
                gameManager.ProcessPlayerTurn(actionPack);
            }
        }
    }
}
