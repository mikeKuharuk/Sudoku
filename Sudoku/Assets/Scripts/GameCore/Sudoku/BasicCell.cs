using System.Collections.Generic;

namespace GameCore.Sudoku
{
    public class BasicCell
    {
        public int PosX;
        public int PosY;
        public List<int> PossibleValues = new List<int>();

        public BasicCell()
        {
        }

        public BasicCell(int posX, int posY)
        {
            PosX = posX;
            PosY = posY;
        }
    }
}