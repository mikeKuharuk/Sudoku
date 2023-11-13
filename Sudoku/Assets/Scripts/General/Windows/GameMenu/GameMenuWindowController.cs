using General.GameSceneManager;
using PlayborschUI.Base;
using Zenject;

namespace PlayborschUI.GameMenu
{
    public class GameMenuWindowController : WindowController<GameMenuWindowView, GameMenuWindowModel>
    {
        [Inject] private IGameSceneManager _gameSceneManager;
        protected override void OnInit()
        {
            View.OnGoToLobby += LoadLobby;
        }

        private void LoadLobby()
        {
            _gameSceneManager.LoadLobbyScene();
        }

        protected override void OnDispose()
        {
            View.OnGoToLobby -= LoadLobby;
        }
    }
}