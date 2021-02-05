using UnityEngine;
using UnityEngine.InputSystem;
using System;

using Zongband.Game.Core;
using Zongband.Game.Actions;
using Zongband.Player;
using Zongband.UI;

namespace Zongband.Core
{
    public class CoreManager : MonoBehaviour
    {
        public GameManager gameManager;
        public PlayerController playerController;
        public UIManager uiManager;

        private void Awake()
        {
            if (gameManager == null) throw new NullReferenceException();
            if (playerController == null) throw new NullReferenceException();
            if (uiManager == null) throw new NullReferenceException();
        }

        private void Start()
        {
            gameManager.CustomStart();
            uiManager.CustomStart();
        }

        private void Update()
        {
            #if !UNITY_EDITOR && !DEVELOPMENT_BUILD
            playerController.ClearActionPack();
            #endif
            
            InputSystem.Update();

            if (gameManager.CanSetPlayerActionPack() && playerController.ActionPackAvailable())
            {
                gameManager.SetPlayerActionPack(playerController.ConsumeActionPack());
            }

            gameManager.CustomUpdate();
            uiManager.CustomUpdate();
        }
    }
}
