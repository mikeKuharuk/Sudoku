using System.Collections.Generic;

namespace GameCore.Sudoku
{
    public class BoardHelper
    {
        private int[,] _board;
        private Dictionary<int, List<int>> _rememberedBadPlacements = new Dictionary<int, List<int>>(); // Key is index of column, Value is unapropriate number for this place

        public BoardHelper(int[,] board)
        {
            _board = board;
        }

        public void ClearRememberedBadPlaces()
        {
            _rememberedBadPlacements.Clear();
        }
        
        public void RememberThisIsBadPlace(int column, int value)
        {
            if (!_rememberedBadPlacements.ContainsKey(column))
            {
                _rememberedBadPlacements.Add(column, new List<int>());
            }
            
            _rememberedBadPlacements[column].Add(value);
        }
        
        public List<int> GetPossibleValuesForPlace(int column, List<int> values)
        {
            var result = new List<int>(values);

            if (_rememberedBadPlacements.ContainsKey(column))
            {
                foreach (var unapropriateNumber in _rememberedBadPlacements[column])
                {
                    result.Remove(unapropriateNumber);
                }
            }
            
            return result;
        }

        public bool CheckIsRowContainValue(int row, int randomValue)
        {
            for (int column = 0; column < 9; column++)
            {
                if (_board[row, column] == randomValue)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckIsColumnContainValue(int column, int randomValue)
        {
            for (int row = 0; row < 9; row++)
            {
                if (_board[row, column] == randomValue)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckIsBlockContainValue(int targetBlockIndex, int randomValue)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    var blockIndex = BlockConstants.GetBlockIndex(row,column);
                    if (blockIndex == targetBlockIndex && _board[row, column] == randomValue)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        
        public void FindPossibleValues(BasicCell cell)
        {
            for (int i = 0; i < 9; i++)
            {
                var blockIndex = BlockConstants.GetBlockIndex(cell.PosX, cell.PosY);
                if (CheckIsBlockContainValue(blockIndex, i))
                {
                    continue;
                }

                if (CheckIsRowContainValue(cell.PosX, i))
                {
                    continue;
                }

                if (CheckIsColumnContainValue(cell.PosY, i))
                {
                    continue;
                }

                if (!cell.PossibleValues.Contains(i))
                {
                    cell.PossibleValues.Add(i);
                }
            }
        }
    }
}