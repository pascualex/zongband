#nullable enable

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
        [SerializeField] private GameManager? gameManager;
        [SerializeField] private PlayerController? playerController;
        [SerializeField] private UIManager? uiManager;

        private void Start()
        {
            gameManager?.CustomStart();
        }

        private void Update()
        {
            if (gameManager == null) return;
            if (playerController == null) return;
            if (uiManager == null) return;

            if (Debug.isDebugBuild) playerController.ClearActionpack();
            
            InputSystem.Update();

            if (gameManager.IsPlayerTurn())
            {
                var actionPack = playerController.RemoveActionPack();
                if (actionPack != null) gameManager.SetPlayerActionPack(actionPack);
            }

            gameManager.CustomUpdate();
            uiManager.CustomUpdate();
        }
    }
}
