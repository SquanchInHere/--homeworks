namespace Swapping
{
    public static class Swap
    {
        public static void Run(int[,] a)
        {
            Console.WriteLine("Original:");
            Helper.PrintMatrix(a);

            var (maxVal, maxR, maxC) = FindMax(a);
            Console.WriteLine($"\nMax = {maxVal} at [{maxR},{maxC}]");

            MoveMaxToTopLeft(a);
            Console.WriteLine("\nAfter moving max to top-left:");
            Helper.PrintMatrix(a);
        }

        private static (int maxVal, int r, int c) FindMax(int[,] m)
        {
            int rows = m.GetLength(0);
            int cols = m.GetLength(1);

            int maxVal = m[0, 0];
            int maxR = 0, maxC = 0;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (m[r, c] > maxVal)
                    {
                        maxVal = m[r, c];
                        maxR = r;
                        maxC = c;
                    }
                }
            }
            return (maxVal, maxR, maxC);
        }

        private static void MoveMaxToTopLeft(int[,] m)
        {
            var (_, r, c) = FindMax(m);

            if (r != 0) SwapRows(m, r, 0);
            if (c != 0) SwapCols(m, c, 0);
        }

        private static void SwapRows(int[,] m, int r1, int r2)
        {
            int cols = m.GetLength(1);
            for (int c = 0; c < cols; c++)
            {
                int tmp = m[r1, c];
                m[r1, c] = m[r2, c];
                m[r2, c] = tmp;
            }
        }

        private static void SwapCols(int[,] m, int c1, int c2)
        {
            int rows = m.GetLength(0);
            for (int r = 0; r < rows; r++)
            {
                int tmp = m[r, c1];
                m[r, c1] = m[r, c2];
                m[r, c2] = tmp;
            }
        }
    }
}
