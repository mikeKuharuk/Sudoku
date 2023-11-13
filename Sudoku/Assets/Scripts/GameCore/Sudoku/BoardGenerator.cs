using System.Collections.Generic;
using Utilities.Logger;
using Random = UnityEngine.Random;

namespace GameCore.Sudoku
{
    public class BoardGenerator
    {
        public int[,] Board => _board;
        public int[,] PuzzleBoard => _puzzleBoard;
        
        private static readonly List<int> StandardValues = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private static readonly int TempCountForRemoval = 50;

        private int[,] _board;
        private int[,] _puzzleBoard;
        private BoardHelper _boardHelper;
        private int _triesToGenerateBoard = 0;
        public void GenerateNewBoard()
        {
            StartNewGeneration();
            LogHandler.LogInfo($"[{nameof(BoardGenerator)} Successfully created new board after {_triesToGenerateBoard} attempts" , LogType.SudokuSolver);
        }

        private void StartNewGeneration()
        {
            if(_triesToGenerateBoard >= FlowConstant.Mid) return;
            
            _triesToGenerateBoard++;
            LogHandler.LogInfo($"[{nameof(BoardGenerator)} Creating new board, attempt: {_triesToGenerateBoard}", LogType.SudokuSolver);

            _board = BlockConstants.DefaultSudoku;

            var puzzleCreator = new BoardPuzzleCreator();
            _puzzleBoard = puzzleCreator.CreatePuzzle(_board, TempCountForRemoval);
            if (_puzzleBoard == null)
            {
                StartNewGeneration();
            }
        }

        private bool GenerateFromScratch()
        {
            _board = new int[9, 9];
            _boardHelper = new BoardHelper(_board);
            _boardHelper.ClearRememberedBadPlaces();

            for (var row = 0; row < 9; row++)
            {
                var valuesToInsert = new List<int>(StandardValues);
                var addingOrder = new List<int>();
                _boardHelper.ClearRememberedBadPlaces();
                var tries = 0;

                for (var column = 0; column < 9; column++)
                {
                    var isValuePlaced = false;

                    while (!isValuePlaced)
                    {
                        var possiblyAppropriateValues = _boardHelper.GetPossibleValuesForPlace(column, valuesToInsert);

                        if (possiblyAppropriateValues.Count == 0)
                        {
                            for (var i = 0; i < 9; i++)
                            {
                                _board[row, i] = 0;
                                column = 0;
                            }

                            valuesToInsert.AddRange(addingOrder);
                            addingOrder.Clear();
                            continue;
                        }

                        var randomValueIndex = Random.Range(0, possiblyAppropriateValues.Count);
                        var randomValue = possiblyAppropriateValues[randomValueIndex];

                        var blockIndex = BlockConstants.GetBlockIndex(row, column);
                        if (!_boardHelper.CheckIsRowContainValue(row, randomValue) &&
                            !_boardHelper.CheckIsColumnContainValue(column, randomValue) &&
                            !_boardHelper.CheckIsBlockContainValue(blockIndex, randomValue))
                        {
                            _board[row, column] = randomValue;
                            isValuePlaced = true;
                            valuesToInsert.Remove(randomValue);
                            addingOrder.Add(randomValue);
                        }
                        else
                        {
                            _boardHelper.RememberThisIsBadPlace(column, randomValue);
                        }

                        tries++;
                        if (tries == 5000) // Hardcode to prevent dead process in tray
                        {
                            StartNewGeneration();
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}