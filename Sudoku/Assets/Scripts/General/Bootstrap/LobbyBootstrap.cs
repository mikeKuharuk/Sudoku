using PlayborschUI.Lobby;
using PlayborschUI.WindowsManager;
using UnityEngine;
using Utilities.Logger;
using Zenject;

namespace General.Bootstrap
{
    public class LobbyBootstrap : MonoBehaviour
    {
        [Inject] private IWindowsManager _windowsManager;

        public void Start()
        {
            LogHandler.LogInfo($"[{nameof(LobbyBootstrap)}] Initializing Lobby scene");
            
            _windowsManager.OpenWindow<LobbyWindowController>();
        }
    }
}
