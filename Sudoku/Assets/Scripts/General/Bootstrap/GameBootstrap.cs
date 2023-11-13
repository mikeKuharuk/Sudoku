using General.Game;
using General.Windows.GameHUD;
using PlayborschUI.GameHUD;
using PlayborschUI.WindowsManager;
using UnityEngine;
using Zenject;

namespace General.Bootstrap
{
    public class GameBootstrap : MonoBehaviour
    {
        [Inject] private IWindowsManager _windowsManager;
        [Inject] private IGameManager _gameManager;
        public void Start()
        {
            _gameManager.Init();
            _windowsManager.OpenWindow<GameHudWindowController>();
            _gameManager.GenerateNewField();
        }
    }
}