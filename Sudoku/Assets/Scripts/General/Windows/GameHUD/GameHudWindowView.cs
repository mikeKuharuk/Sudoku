using GameCore;
using PlayborschUI.Base;
using PlayborschUI.GameHUD;
using PlayborschUI.WindowsManager;
using TMPro;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace General.Windows.GameHUD
{
    public class GameHudWindowView : WindowView<GameHudWindowModel>
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private TMP_Text _movesText;
        [SerializeField] private TMP_Text _mistakesText;
        [SerializeField] private PaintHolder _paintHolder;
        [SerializeField] private GameFieldView _gameField;


        public event Action OnOpenMenu;
        public GameFieldView GameFieldView => _gameField;
        public PaintHolder PaintHolder => _paintHolder;
        public override CanvasLayers Layer => CanvasLayers.Hud;
        protected override void InitView()
        {
            _menuButton.onClick.AddListener(OpenMenu);
        }

        public void Update()
        {
            _timerText.text = Model.GameEvents.ElapsedGameTime.ToString(@"mm\:ss");
        }

        public void UpdateMoves(string movesAmount)
        {
            _movesText.text = movesAmount;
        }

        public void UpdateMistakes(string mistakesAmount)
        {
            _mistakesText.text = mistakesAmount;
        }

        protected override void OnDestroying()
        {
            _menuButton.onClick.RemoveListener(OpenMenu);
        }

        private void OpenMenu()
        {
            OnOpenMenu?.Invoke();
        }
    }
}