using System;

namespace General.Game
{
    public interface IGameEventsListener
    {
        int TotalMoves { get; }
        int TotalMistakes { get; }
        
        public TimeSpan ElapsedGameTime { get; }
        
        event Action OnMovePerformed;
        event Action OnPuzzleSolved;
        
        // Extract this block somewhere else
        event Action OnGameStarted;
        event Action OnGamePaused;
        event Action OnGameUnpaused;
        //
    }
}