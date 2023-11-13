using System;
using PlayborschUI.Base;
using PlayborschUI.WindowsManager;
using UnityEngine;
using UnityEngine.UI;

namespace PlayborschUI.Lobby
{
    public class LobbyWindowView : WindowView<LobbyWindowModel>
    {       
        [SerializeField] private Button _openSettingsButton;
        [SerializeField] private Button _playButton;
        public override CanvasLayers Layer => CanvasLayers.Window;
        public event Action OnOpenSettings;
        public event Action OnPlayGame;

        protected override void InitView()
        {
            _openSettingsButton.onClick.AddListener(OpenSettings);
            _playButton.onClick.AddListener(PlayGame);
        }

        protected override void OnDestroying()
        {
            _openSettingsButton.onClick.RemoveListener(OpenSettings);
            _playButton.onClick.RemoveListener(PlayGame);

        }
        private void PlayGame()
        {
            OnPlayGame?.Invoke();
        }
        private void OpenSettings()
        {
            OnOpenSettings?.Invoke();
        }
    }
}