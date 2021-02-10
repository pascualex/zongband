#nullable enable

using UnityEngine;

using Zongband.Input;
using Zongband.UI;
using Zongband.Game.Core;

namespace Zongband.Core
{
    public class CoreManager : MonoBehaviour
    {
        [SerializeField] private InputManager? inputManager;
        [SerializeField] private UIManager? uiManager;
        [SerializeField] private GameManager? gameManager;

        private void Start()
        {
            gameManager?.SetupExample();
        }

        private void Update()
        {
            if (inputManager == null) return;
            if (gameManager == null) return;
            if (uiManager == null) return;
            
            inputManager.ProcessInput();
            gameManager.GameLoop();
            uiManager.Refresh();
            inputManager.ClearInput();
        }
    }
}
