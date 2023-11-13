using System.Collections.Generic;

namespace GameCore.Sudoku
{
    public static class BlockConstants
    {
        private static readonly List<int> BlockOneRow = new List<int>() {0,1,2};
        private static readonly List<int> BlockOneColumn = new List<int>() {0,1,2};

        private static readonly List<int> BlockTwoRow = new List<int>() {0,1,2};
        private static readonly List<int> BlockTwoColumn = new List<int>() {3,4,5};

        private static readonly List<int> BlockThreeRow = new List<int>() {0,1,2};
        private static readonly List<int> BlockThreeColumn = new List<int>() {6,7,8};

        private static readonly List<int> BlockFourRow = new List<int>() {3,4,5};
        private static readonly List<int> BlockFourColumn = new List<int>() {0,1,2};

        private static readonly List<int> BlockFiveRow = new List<int>() {3,4,5};
        private static readonly List<int> BlockFiveColumn = new List<int>() {3,4,5};

        private static readonly List<int> BlockSixRow = new List<int>() {3,4,5};
        private static readonly List<int> BlockSixColumn = new List<int>() {6,7,8};

        private static readonly List<int> BlockSevenRow = new List<int>() {6,7,8};
        private static readonly List<int> BlockSevenColumn = new List<int>() {0,1,2};

        private static readonly List<int> BlockEightRow = new List<int>() {6,7,8};
        private static readonly List<int> BlockEightColumn = new List<int>() {3,4,5};

        private static readonly List<int> BlockNineRow = new List<int>() {6,7,8};
        private static readonly List<int> BlockNineColumn = new List<int>() {6,7,8};

        public static readonly int[,] DefaultSudoku =
        {
            { 5,6,3, 8,7,9, 2,1,4 },
            { 7,1,9, 4,2,3, 6,5,8 },
            { 2,8,4, 5,6,1, 3,9,7 },
            
            { 4,2,6, 1,5,7, 9,8,3,},
            { 1,9,5, 6,3,8, 4,7,2,},
            { 8,3,7, 2,9,4, 1,6,5,},
            
            { 9,4,8, 3,1,5, 7,2,6,},
            { 6,5,1, 7,4,2, 8,3,9,},
            { 3,7,2, 9,8,6, 5,4,1,},
        };
        
        private class Block
        {
            public List<int> Row;
            public List<int> Column;

            public Block(List<int> row, List<int> column)
            {
                Row = row;
                Column = column;
            }
        }

        private static readonly List<Block> Blocks = new List<Block>()
        {
            new Block(BlockOneRow, BlockOneColumn),
            new Block(BlockTwoRow, BlockTwoColumn),
            new Block(BlockThreeRow, BlockThreeColumn),
            new Block(BlockFourRow, BlockFourColumn),
            new Block(BlockFiveRow, BlockFiveColumn),
            new Block(BlockSixRow, BlockSixColumn),
            new Block(BlockSevenRow, BlockSevenColumn),
            new Block(BlockEightRow, BlockEightColumn),
            new Block(BlockNineRow, BlockNineColumn),
        };

        public static int GetBlockIndex(int row, int column)
        {
            foreach (var block in Blocks)
            {
                if (block.Row.Contains(row) && block.Column.Contains(column))
                {
                    var indexOfBlock = Blocks.IndexOf(block);
                    return indexOfBlock;
                }
            }

            return -1;
        }

        public static (int xIndex, int yIndex) GetMatrixIndex(int blockIndex, int indexInBlock)
        {
            var block = Blocks[blockIndex];

            var xIndex = GetXByIndexInBlock(indexInBlock);
            var yIndex = GetYByIndexInBlock(indexInBlock);

            var xIndexInMatrix = block.Row[xIndex];
            var yIndexInMatrix = block.Column[yIndex];

            return (xIndexInMatrix, yIndexInMatrix);
        }

        private static int GetYByIndexInBlock(int indexInBlock)
        {
            var yIndex = 0;
            if (indexInBlock == 0 || indexInBlock == 3 || indexInBlock == 6)
            {
                yIndex = 0;
            } else if (indexInBlock == 1 || indexInBlock == 4 || indexInBlock == 7)
            {
                yIndex = 1;
            }
            else
            {
                yIndex = 2;
            }

            return yIndex;
        }

        private static int GetXByIndexInBlock(int indexInBlock)
        {
            var xIndex = 0;
            if (indexInBlock <= 2)
            {
                xIndex = 0;
            }
            else if(indexInBlock <= 5)
            {
                xIndex = 1;
            }
            else
            {
                xIndex = 2;
            }

            return xIndex;
        }

    }
}