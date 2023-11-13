using System;
using System.Diagnostics;

namespace General.Game
{
    public class GameEventsManager : IGameEventsInvoker, IGameEventsListener
    {
        public int TotalMoves { get; private set; }
        public int TotalMistakes { get; private set; }
        public TimeSpan ElapsedGameTime => _watch.Elapsed;

        public event Action OnMovePerformed;
        public event Action OnPuzzleSolved;
        public event Action OnGameStarted;
        public event Action OnGamePaused;
        public event Action OnGameUnpaused;

        private readonly Stopwatch _watch = new Stopwatch();
        

        public void MovePerformed(int totalMoves, int totalMistakes)
        {
            TotalMoves = totalMoves;
            TotalMistakes = totalMistakes;
            
            OnMovePerformed?.Invoke();
        }

        public void PuzzleSolved()
        {
            _watch.Stop();
            OnPuzzleSolved?.Invoke();
        }

        public void StartGame()
        {
            _watch.Reset();
            _watch.Start();
            OnGameStarted?.Invoke();
        }

        public void PauseGame()
        {
            _watch.Stop();
            OnGamePaused?.Invoke();
        }

        public void UnPauseGame()
        {
            _watch.Start();
            OnGameUnpaused?.Invoke();
        }
    }
}