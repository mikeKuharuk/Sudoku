using System;
using GameCore.Sudoku;

namespace GameCore
{
    public class GameField : IGameField
    {
        public IGameFieldView View => _view;
        public Cell SelectedCell => _view.LastSelectedCell;
        
        public event Action NewFieldGenerated;
        public event Action NewViewAssigned;

        private IGameFieldView _view;
        private Cell[,] _currentBoard => _view?.ViewCells;
        
        private readonly BoardGenerator _boardGenerator;

        public GameField()
        {
            _boardGenerator = new BoardGenerator();
        }
        
        public void GenerateNewField()
        {
            _boardGenerator.GenerateNewBoard();
            FillSudokuBoard();
            NewFieldGenerated?.Invoke();
        }

        public void AssignFieldView(IGameFieldView newView)
        {
            UnsubscribeFromView();
            _view = newView;
            SubscribeOnView();
            NewViewAssigned?.Invoke();
        }
        public bool CheckIsPuzzleSolved()
        {
            foreach (var cell in _currentBoard)
            {
                if (!cell.IsSolved)
                {
                    return false;
                }
            }

            return true;
        }

        private void FillSudokuBoard()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    _currentBoard[i,j].SetData(_boardGenerator.Board[i,j],_boardGenerator.PuzzleBoard[i,j]);
                    if (_boardGenerator.PuzzleBoard[i, j] != 0) // This means cell contains value
                    {
                        _currentBoard[i,j].SetImmutable(true);
                    }
                }
            }
        }
        private void SubscribeOnView()
        {
            if(_currentBoard == null) return;

            foreach (var cell in _currentBoard)
            {
                cell.OnCellClicked += OnCellClicked;
            }
        }
        private void UnsubscribeFromView()
        {
            if(_currentBoard == null) return;

            foreach (var cell in _currentBoard)
            {
                cell.OnCellClicked -= OnCellClicked;
            }
        }

        private void OnCellClicked(Cell cell)
        {
            _view.OnCellClicked(cell);
        }
    }
}