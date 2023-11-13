namespace GameCore
{
    public interface IPainter
    {
        IPaint SelectedPaint { get; }
        void Paint(Cell cell);
        void SelectNewPaint(IPaint paint);

        void SetNoteMode(bool isOn);
    }
}