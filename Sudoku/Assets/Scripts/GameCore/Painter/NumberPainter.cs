using General.Game;
using Zenject;

namespace GameCore
{
    public class NumberPainter : IPainter
    {
        [Inject] private IGameManager _gameManager;

        public IPaint SelectedPaint => _selectedPaint;

        private IPaint _selectedPaint;
        private bool _noteMode;
        public void Paint(Cell cell)
        {
            if (_selectedPaint == null || cell == null) return;

            if (!_noteMode)
            {
                _gameManager.PaintCell(cell, _selectedPaint);
            }
            else
            {
                _gameManager.PlaceNoteInCell(cell, _selectedPaint);
            }
        }

        public void SelectNewPaint(IPaint paint)
        {
            _selectedPaint = paint;
        }

        public void SetNoteMode(bool isOn)
        {
            _noteMode = isOn;
        }
    }
}