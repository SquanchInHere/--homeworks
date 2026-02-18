namespace SubtractRow
{
    public static class Matrix
    {
        public static void Run(int[,] a)
        {
            Console.WriteLine("Original:");
            Print(a);

            SubtractRowMins(a);
            Console.WriteLine("\nAfter subtracting row minimums:");
            Print(a);

            var bestZero = FindZeroWithMaxWeight(a);

            if (bestZero.found)
                Console.WriteLine($"\nBest zero: row={bestZero.row}, col={bestZero.col}, weight={bestZero.weight}");
            else
                Console.WriteLine("\nNo zeros in matrix.");
        }

        public static int[,] CreateRandomMatrix(int minValue, int maxValue)
        {
            int[,] m = new int[Params.rows, Params.cols];
            for (int r = 0; r < Params.rows; r++)
                for (int c = 0; c < Params.cols; c++)
                    m[r, c] = Params.rng.Next(minValue, maxValue);
            return m;
        }

        private static void Print(int[,] m)
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

        private static void SubtractRowMins(int[,] m)
        {
            int rows = m.GetLength(0);
            int cols = m.GetLength(1);

            for (int r = 0; r < rows; r++)
            {
                int min = m[r, 0];
                for (int c = 1; c < cols; c++)
                    if (m[r, c] < min) min = m[r, c];

                for (int c = 0; c < cols; c++)
                    m[r, c] -= min;
            }
        }

        private static (bool found, int row, int col, int weight) FindZeroWithMaxWeight(int[,] m)
        {
            int rows = m.GetLength(0);
            int cols = m.GetLength(1);

            bool found = false;
            int bestR = -1, bestC = -1, bestW = int.MinValue;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (m[r, c] != 0) continue;

                    int rowMin = MinInRowExcluding(m, r, c);
                    int colMin = MinInColExcluding(m, c, r);

                    int weight = rowMin + colMin;

                    if (!found || weight > bestW)
                    {
                        found = true;
                        bestW = weight;
                        bestR = r;
                        bestC = c;
                    }
                }
            }

            return (found, bestR, bestC, bestW);
        }

        private static int MinInRowExcluding(int[,] m, int row, int excludeCol)
        {
            int cols = m.GetLength(1);
            bool hasAny = false;
            int min = int.MaxValue;

            for (int c = 0; c < cols; c++)
            {
                if (c == excludeCol) continue;
                int v = m[row, c];
                if (!hasAny || v < min)
                {
                    min = v;
                    hasAny = true;
                }
            }

            return min;
        }

        private static int MinInColExcluding(int[,] m, int col, int excludeRow)
        {
            int rows = m.GetLength(0);
            bool hasAny = false;
            int min = int.MaxValue;

            for (int r = 0; r < rows; r++)
            {
                if (r == excludeRow) continue;
                int v = m[r, col];
                if (!hasAny || v < min)
                {
                    min = v;
                    hasAny = true;
                }
            }

            return min;
        }
    }
}
