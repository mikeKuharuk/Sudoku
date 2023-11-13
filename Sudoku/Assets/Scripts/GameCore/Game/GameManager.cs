using System.Diagnostics;
using System.Timers;
using GameCore;
using Zenject;

namespace General.Game
{
    public class GameManager : IGameManager
    {
        [Inject] private IGameField _gameField;
        [Inject] private IGameEventsInvoker _gameEvents;

        private int _totalMoves;
        private int _totalMistakes;
        public void Init()
        {
            
        }

        public void GenerateNewField()
        {
            _gameField.GenerateNewField();
            _gameEvents.StartGame();
        }

        public void PaintCell(Cell cell, IPaint paint)
        {
            cell.PaintCell(paint);
           
           _totalMoves++;
           if (!cell.IsSolved)
           {
               _totalMistakes++;
           }
           
           _gameEvents.MovePerformed(_totalMoves, _totalMistakes);

           if (_gameField.CheckIsPuzzleSolved())
           {
               _gameEvents.PuzzleSolved();
           }
        }

        public void PlaceNoteInCell(Cell cell, IPaint paint)
        {
            cell.PlaceNoteInCell(paint);
        }
    }
}