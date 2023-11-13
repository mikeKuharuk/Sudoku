namespace GameCore
{
    public class PaintNumber : IPaint
    {
        public int Paint { get; }

        public PaintNumber(int number)
        {
            Paint = number;
        }
    }
}