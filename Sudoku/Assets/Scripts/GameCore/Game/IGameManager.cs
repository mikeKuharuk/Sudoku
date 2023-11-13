using GameCore;
using Unity.Plastic.Antlr3.Runtime.Misc;

namespace General.Game
{
    public interface IGameManager
    {
        void Init();
        void GenerateNewField();
        void PaintCell(Cell cell, IPaint paint);
        void PlaceNoteInCell(Cell cell, IPaint paint);
    }
}