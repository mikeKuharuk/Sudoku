using System;
using System.Collections.Generic;
using System.Linq;
using GameCore.Sudoku.Solver;
using Utilities.Extensions;
using Utilities.Logger;
using Random = UnityEngine.Random;

namespace GameCore.Sudoku
{
    public class BoardPuzzleCreator
    {
        private int[,] _puzzle;
        private int _triesValue = 0;
        private List<BasicCell> _removedPlaces = new List<BasicCell>();
        public int[,] CreatePuzzle(int[,] generatedBoard, int removedValuesCount)
        {
            _puzzle = generatedBoard.CreateClone();
            var boardSize = generatedBoard.GetLength(0) * generatedBoard.GetLength(1);
            var limit = boardSize;
            
            var boardSolver = new BoardSolver();
            
            for (int i = 0; i < removedValuesCount; i++)
            {
                while (limit > 0)
                {
                    if (_removedPlaces.Count + i == boardSize)
                    {
                        break;
                    }

                    var puzzleVariant = TryRemoveValueFromBoard(_puzzle);
                    if(boardSolver.TryToSolveBoard(puzzleVariant))
                    {
                        _puzzle = puzzleVariant;
                        _triesValue++;
                        _removedPlaces.Clear();
                        limit = boardSize-i;
                        break;
                    }

                    limit--;
                    _triesValue++;
                }

                if (_removedPlaces.Count + i == boardSize)
                {
                    LogHandler.LogInfo($"[{nameof(BoardPuzzleCreator)}] Failed to create a puzzle after {_triesValue} tries. " +
                                       $"Removed {i} values from board, places that cant be removed: {_removedPlaces.Count}", LogType.SudokuSolver);
                    return null;
                }
            }
            
            LogHandler.LogInfo($"[{nameof(BoardPuzzleCreator)}] Successfully created solvable puzzle after {_triesValue} tries", LogType.SudokuSolver);
            return _puzzle;
        }

        private int[,] TryRemoveValueFromBoard(int[,] puzzle)
        {
            var puzzleVariant = puzzle.CreateClone();
            while (true)
            {
                var xIndex = Random.Range(0, 9);
                var yIndex = Random.Range(0, 9);
                
                var isUsedAlready = _removedPlaces.Any(removedPlace => removedPlace.PosX == xIndex && removedPlace.PosY == yIndex);
                if (puzzleVariant[xIndex, yIndex] == 0 || isUsedAlready) continue;
                
                puzzleVariant[xIndex, yIndex] = 0;
                _removedPlaces.Add(new BasicCell(xIndex, yIndex));
                
                return puzzleVariant;

            }
        }
    }
}