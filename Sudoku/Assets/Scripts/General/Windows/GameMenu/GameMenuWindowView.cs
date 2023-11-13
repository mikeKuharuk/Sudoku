using System;
using PlayborschUI.Base;
using PlayborschUI.WindowsManager;
using UnityEngine;
using UnityEngine.UI;

namespace PlayborschUI.GameMenu
{
    public class GameMenuWindowView : WindowView<GameMenuWindowModel>
    {
        [SerializeField] private Button _goToLobbyButton;
        public override CanvasLayers Layer => CanvasLayers.Window;
        public event Action OnGoToLobby;

        protected override void InitView()
        {
            _goToLobbyButton.onClick.AddListener(OnGoToLobbyButton);
        }

        private void OnGoToLobbyButton()
        {
            OnGoToLobby?.Invoke();
        }

        protected override void OnDestroying()
        {
            _goToLobbyButton.onClick.RemoveListener(OnGoToLobbyButton);
        }
    }
}