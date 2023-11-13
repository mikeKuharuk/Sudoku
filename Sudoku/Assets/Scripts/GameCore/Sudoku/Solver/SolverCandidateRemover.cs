using System.Collections.Generic;
using System.Linq;
using Utilities.Extensions;
using Utilities.Logger;

namespace GameCore.Sudoku.Solver
{
    public class SolverCandidateRemover
    {
        private BoardHelper _boardHelper;
        private List<BasicCell> _emptyCells;
        private Dictionary<int, List<BasicCell>> _cellByBlocks;
        private int _removedValuesCount;

        public SolverCandidateRemover()
        {
            _cellByBlocks = new Dictionary<int, List<BasicCell>>();
        }

        public void RemoveWrongPossibleValues(List<BasicCell> emptyCells)
        {
            _emptyCells = emptyCells;
            _removedValuesCount = 0;

            SortByBlocks();
            FindForLines();
            FindForRows();

            LogHandler.LogInfo($"[{nameof(SolverCandidateRemover)}] Removed {_removedValuesCount} values from puzzle");
        }

        private void SortByBlocks()
        {
            foreach (var emptyCell in _emptyCells)
            {
                var blockIndex = BlockConstants.GetBlockIndex(emptyCell.PosX, emptyCell.PosY);
                _cellByBlocks.AddSafely(blockIndex, emptyCell);
            }
        }

        private void FindForLines()
        {
            foreach (var block in _cellByBlocks)
            {
                List<int> possibleValuesInBlock = FindAllPossibleValuesInBlock(block);
                var searchingIndex = -1; // -1 is default value
                
                foreach (var searchingValue in possibleValuesInBlock)
                {
                    foreach (var cell in block.Value)
                    {
                        if (cell.PossibleValues.Contains(searchingValue))
                        {
                            if (searchingIndex == -1 || cell.PosX == searchingIndex)
                            {
                                searchingIndex = cell.PosX;
                            }
                            else
                            {
                                searchingIndex = -1;
                                break;
                            }
                        }
                    }

                    if (searchingIndex != -1)
                    {
                        for (int i = 0; i < _emptyCells.Count; i++)
                        {
                            var blockIndex = BlockConstants.GetBlockIndex(_emptyCells[i].PosX, _emptyCells[i].PosY);
                            if (_emptyCells[i].PosX == searchingIndex && _emptyCells[i].PossibleValues.Contains(searchingValue) && blockIndex != block.Key)
                            {
                                _emptyCells[i].PossibleValues.Remove(searchingValue);
                                _removedValuesCount++;
                            }
                        }
                    }
                }
            }
        }
        private void FindForRows()
        {
            foreach (var block in _cellByBlocks)
            {
                List<int> possibleValuesInBlock = FindAllPossibleValuesInBlock(block);
                var searchingIndex = -1; // -1 is default value
                
                foreach (var searchingValue in possibleValuesInBlock)
                {
                    foreach (var cell in block.Value)
                    {
                        if (cell.PossibleValues.Contains(searchingValue))
                        {
                            if (searchingIndex == -1 || cell.PosY == searchingIndex)
                            {
                                searchingIndex = cell.PosY;
                            }
                            else
                            {
                                searchingIndex = -1;
                                break;
                            }
                        }
                    }

                    if (searchingIndex != -1)
                    {
                        for (int i = 0; i < _emptyCells.Count; i++)
                        {
                            var blockIndex = BlockConstants.GetBlockIndex(_emptyCells[i].PosX, _emptyCells[i].PosY);
                            if (_emptyCells[i].PosY == searchingIndex && _emptyCells[i].PossibleValues.Contains(searchingValue) && blockIndex != block.Key)
                            {
                                _emptyCells[i].PossibleValues.Remove(searchingValue);
                                _removedValuesCount++;
                            }
                        }
                    }
                }
            }
        }

        private List<int> FindAllPossibleValuesInBlock(KeyValuePair<int, List<BasicCell>> block)
        {
            List<int> possibleValuesInBlock = new List<int>();
            foreach (var blockCell in block.Value)
            {
                possibleValuesInBlock.AddRange(blockCell.PossibleValues);
            }

            possibleValuesInBlock = possibleValuesInBlock.Distinct().ToList();
            return possibleValuesInBlock;
        }
    }
}