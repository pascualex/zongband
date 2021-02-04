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
            gameManager.SetupExample();
        }

        private void Update()
        {
            #if !UNITY_EDITOR && !DEVELOPMENT_BUILD
            playerController.ClearActionPack();
            #endif
            
            InputSystem.Update();
            if (gameManager.ReadyForNewTurn())
            {
                if (gameManager.IsPlayerTurn())
                {
                    if (playerController.ActionPackAvailable())
                    {
                        ActionPack actionPack = playerController.ConsumeActionPack();
                        gameManager.ProcessPlayerTurn(actionPack);     
                    }
                }
                else
                {
                    gameManager.ProcessAITurns();
                }
            }
            uiManager.UpdateUI();
        }
    }
}
