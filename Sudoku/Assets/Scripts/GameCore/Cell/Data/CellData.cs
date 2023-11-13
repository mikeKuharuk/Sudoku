using GameCore.Sudoku;

namespace GameCore
{
    public class CellData
    {
        public readonly int FieldBlockIndex;
        public readonly int IndexInBlock;
        
        public readonly int XInMatrix;
        public readonly int YInMatrix;

        public int TrueValue;
        public int PlacedValue;
        public CellData(int fieldBlockIndex, int indexInBlock)
        {
            FieldBlockIndex = fieldBlockIndex;
            IndexInBlock = indexInBlock;

            var address = BlockConstants.GetMatrixIndex(fieldBlockIndex, indexInBlock);
            XInMatrix = address.xIndex;
            YInMatrix = address.yIndex;
        }
    }
}