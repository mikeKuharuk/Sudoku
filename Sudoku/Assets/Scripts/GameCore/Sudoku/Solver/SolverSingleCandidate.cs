using System.Collections.Generic;

namespace GameCore.Sudoku.Solver
{
    public class SolverSingleCandidate
    {
        public BasicCell PlaceValueIfPossible(int[,] board, List<BasicCell> emptyCells)
        {
            foreach (var emptyCell in emptyCells)
            {
                var posX = emptyCell.PosX;
                var posY = emptyCell.PosY;
                if (emptyCell.PossibleValues.Count == 1)
                {
                    board[posX, posY] = emptyCell.PossibleValues[0];
                    return emptyCell;
                }
            }
            return null;
        }
    }
}
