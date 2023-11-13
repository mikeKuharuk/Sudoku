using System.Collections.Generic;
using Utilities.Logger;

namespace GameCore.Sudoku.Solver
{
    public class SolverNakedPairsRemover
    {
        private BoardHelper _boardHelper;
        private List<BasicCell> _emptyCells;
        private Dictionary<int, List<BasicCell>> _cellByBlocks;
        private int _removedValuesCount;

        public SolverNakedPairsRemover()
        {
            _cellByBlocks = new Dictionary<int, List<BasicCell>>();
        }

        public void RemoveWrongPossibleValues(List<BasicCell> emptyCells)
        {
            _emptyCells = emptyCells;
            _removedValuesCount = 0;
            
            LogHandler.LogInfo($"[{nameof(SolverNakedPairsRemover)}] Removed {_removedValuesCount} values from puzzle");
        }
    }
}