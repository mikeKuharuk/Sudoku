using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace GameCore
{
    public class PaintHolder : MonoBehaviour
    {
        [SerializeField] private PaintButton _paintButtonPrefab;
        [SerializeField] private Transform _paintRoot;
        [SerializeField] private Toggle _noteModeToggle;

        private List<PaintButton> _paintButtons = new List<PaintButton>();

        private IPainter _painter;
        private IGameField _gameField;
        private IGameFieldView _gameFieldView;

        public void Init(IPainter painter, IGameFieldView gameFieldView, IGameField gameField)
        {
            _painter = painter;
            _gameFieldView = gameFieldView;
            _gameField = gameField;
            _noteModeToggle.onValueChanged.AddListener(OnNoteModeChanged);
            
            InstantiatePaintButtons();
            InitPaintButtons();
        }

        private void InstantiatePaintButtons()
        {
            for (int i = 0; i < TemporaryProjectConstants.PaintCount; i++)
            {
                var paint = Instantiate(_paintButtonPrefab, _paintRoot);
                _paintButtons.Add(paint);
            }
        }

        private void InitPaintButtons()
        {
            if (_paintButtons.Count == 0)
            {
                Debug.LogWarning($"Warning! Paint buttons was not set!");
                return;
            }

            for (var index = 0; index < _paintButtons.Count; index++)
            {
                var paintButton = _paintButtons[index];
                var paintNumber = index;
                paintButton.Init(paintNumber);
                paintButton.OnPaintClicked += OnPaintClicked;
            }
        }

        private void OnPaintClicked(PaintButton paintButton)
        {
            _painter.SelectNewPaint(paintButton.Paint);
            _painter.Paint(_gameField.SelectedCell);
        }

        private void OnNoteModeChanged(bool isOn)
        {
            _painter.SetNoteMode(isOn);
        }

        private void OnDestroy()
        {
            _noteModeToggle.onValueChanged.RemoveListener(OnNoteModeChanged);
            foreach (var paintButton in _paintButtons)
            {
                paintButton.OnPaintClicked -= OnPaintClicked;
            }
        }
    }
}