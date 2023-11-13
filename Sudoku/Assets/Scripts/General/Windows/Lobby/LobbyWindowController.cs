using General.GameSceneManager;
using PlayborschUI.Base;
using PlayborschUI.Settings;
using Zenject;

namespace PlayborschUI.Lobby
{
    public class LobbyWindowController : WindowController<LobbyWindowView, LobbyWindowModel>
    {
        [Inject] private IGameSceneManager _gameSceneManager;
        protected override void OnInit()
        {
            View.OnOpenSettings += OpenSettings;
            View.OnPlayGame += PlayGame;
        }

        private void PlayGame()
        {
            _gameSceneManager.LoadGameScene();
        }

        private void OpenSettings()
        {
            WindowsManager.OpenWindow<SettingsWindowController>();
        }

        protected override void OnDispose()
        {            
            View.OnOpenSettings -= OpenSettings;
            View.OnPlayGame -= PlayGame;

        }
    }
}