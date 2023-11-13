using System;
using System.Collections.Generic;
using System.Linq;
using Utilities.Extensions;
using Utilities.Logger;

namespace GameCore.Sudoku.Solver
{
    public class BoardSolver
    {
        private int[,] _targetBoard;
        private List<BasicCell> _emptyCells;

        private SolverSingleCandidate _singleCandidate;
        private SolverCandidateRemover _candidateRemover;
        
        private BoardHelper _boardHelper;
        private int _triesCounter;

        public BoardSolver()
        {
            _singleCandidate = new SolverSingleCandidate();
            _candidateRemover = new SolverCandidateRemover();
        }
        public bool TryToSolveBoard(int[,] board)
        {
            _triesCounter = 0;
            _targetBoard = board.CreateClone();
            _boardHelper = new BoardHelper(board);

            FindEmptyCells();
            
            FindPossibleValues();
            _candidateRemover.RemoveWrongPossibleValues(_emptyCells);

            var isTherePlaceableValues = IsTherePlaceableValues();
            if (!isTherePlaceableValues)
            {
                LogHandler.LogInfo($"[{nameof(BoardSolver)}] Can`t solve puzzle! Rolling back", LogType.SudokuSolver);
                return false;
            }

            while (isTherePlaceableValues)
            {
                while (isTherePlaceableValues)
                {
                    var placedCell = _singleCandidate.PlaceValueIfPossible(_targetBoard, _emptyCells);
                    if (placedCell != null)
                    {
                        _emptyCells.Remove(placedCell);
                    }
                    else
                    {
                        isTherePlaceableValues = false;
                    }
                }
            
                FindPossibleValues();
                _candidateRemover.RemoveWrongPossibleValues(_emptyCells);
                isTherePlaceableValues = IsTherePlaceableValues();
                
                _triesCounter++;
                if (_triesCounter >= FlowConstant.Low)
                {
                    throw new Exception("Something wrong here!!");
                }
            }

            if (_emptyCells.Count == 0)
            {
                LogHandler.LogInfo($"[{nameof(BoardSolver)}] Can`t solve puzzle! Rolling back", LogType.SudokuSolver);
                return true;
            }

            return false;
        }

        private void FindPossibleValues()
        {
            foreach (var emptyCell in _emptyCells)
            {
                emptyCell.PossibleValues = new List<int>();
                _boardHelper.FindPossibleValues(emptyCell);
            }
        }

        private void FindEmptyCells()
        {
            _emptyCells = new List<BasicCell>();
            for (int i = 0; i < _targetBoard.GetLength(0); i++)
            {
                for (int j = 0; j < _targetBoard.GetLength(1); j++)
                {
                    if (_targetBoard[i, j] == 0)
                    {
                        var cell = new BasicCell
                        {
                            PosX = i,
                            PosY = j
                        };

                        _emptyCells.Add(cell);
                    }
                }
            }
        }

        private bool IsTherePlaceableValues()
        {
            return _emptyCells.Any(cell => cell.PossibleValues.Count == 1);
        }
    }
}