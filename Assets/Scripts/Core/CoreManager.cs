using UnityEngine;
using UnityEngine.InputSystem;
using System;

using Zongband.Game;

namespace Zongband.Core
{
    public class CoreManager : MonoBehaviour
    {
        public GameManager gameManager;

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
            gameManager.SetupTurn();
            InputSystem.Update();
            if (gameManager.IsReady()) gameManager.ProcessTurn();
        }
    }
}
