namespace Utilities.Extensions
{
    public static class ArrayExtensions
    {
        public static int[,] CreateClone(this int[,] array)
        {
            var clone = new int[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    clone[i,j] = array[i, j];
                }
            }

            return clone;
        }
    }
}