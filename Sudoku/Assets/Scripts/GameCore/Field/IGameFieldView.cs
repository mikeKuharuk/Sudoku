namespace GameCore
{
    public interface IGameFieldView
    {
        Cell[,] ViewCells { get; }
        
        Cell LastSelectedCell { get; }

        void OnCellClicked(Cell cell);
    }
}