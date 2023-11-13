using GameCore;
using General.Game;
using PlayborschUI.Base;
using PlayborschUI.GameHUD;
using PlayborschUI.GameMenu;
using Zenject;

namespace General.Windows.GameHUD
{
    public class GameHudWindowController : WindowController<GameHudWindowView, GameHudWindowModel>
    {
        [Inject] private IGameManager _gameManager;
        [Inject] private IGameField _gameField;
        [Inject] private IGameEventsListener _gameEvents;
        protected override void OnInit()
        {
            View.OnOpenMenu += OpenMenu;
            _gameField.NewFieldGenerated += OnNewFieldGenerated;
            
            _gameEvents.OnMovePerformed += OnMovePerformed;
            
            View.GameFieldView.Init();
            View.PaintHolder.Init(Model.Painter, View.GameFieldView, _gameField);

            _gameField.AssignFieldView(View.GameFieldView);
        }

        private void OnMovePerformed()
        {
            View.UpdateMoves(_gameEvents.TotalMoves.ToString());
            View.UpdateMistakes(_gameEvents.TotalMistakes.ToString());
        }

        private void OnNewFieldGenerated()
        {
            
        }

        private void OpenMenu()
        {
            WindowsManager.OpenWindow<GameMenuWindowController>();
        }

        protected override void OnDispose()
        {            
            View.OnOpenMenu -= OpenMenu;
            _gameField.NewFieldGenerated -= OnNewFieldGenerated;
            _gameEvents.OnMovePerformed -= OnMovePerformed;

        }
    }
}