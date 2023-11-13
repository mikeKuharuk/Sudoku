namespace General.Game
{
    public interface IGameEventsInvoker
    {
        void MovePerformed(int totalMoves, int totalMistakes);
        void PuzzleSolved();

        void StartGame();
        void PauseGame();
        void UnPauseGame();
    }
}