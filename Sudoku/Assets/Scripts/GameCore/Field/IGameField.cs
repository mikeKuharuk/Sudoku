using System;

namespace GameCore
{
    public interface IGameField
    {
        IGameFieldView View { get; }
        Cell SelectedCell { get; }

        event Action NewFieldGenerated;

        void GenerateNewField();

        void AssignFieldView(IGameFieldView newView);
        bool CheckIsPuzzleSolved();
    }
}