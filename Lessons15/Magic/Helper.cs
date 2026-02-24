namespace Swapping
{
    public static class Helper
    {
        public static int[,] RandomMatrix(int row, int col, int min, int max)
        {
            Random rng = new Random();
            int[,] m = new int[row, col];

            for (int r = 0; r < row; r++)
                for (int c = 0; c < col; c++)
                    m[r, c] = rng.Next(min, max + 1);

            return m;
        }

        public static void PrintMatrix(int[,] m)
        {
            int rows = m.GetLength(0);
            int cols = m.GetLength(1);

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                    Console.Write($"{m[r, c],4}");
                Console.WriteLine();
            }
        }
    }
}
