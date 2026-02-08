namespace ColumnSums
{
    public static class TwoDArr
    {
        public static void Run()
        {
            int[,] B = new int[Params.Row, Params.Column];

            FillRandom(B, minValue: 0, maxValueExclusive: 10);
            PrintWithSums(B);
        }

        private static void FillRandom(int[,] a, int minValue, int maxValueExclusive)
        {
            Random rnd = new Random();
            int rows = a.GetLength(0);
            int cols = a.GetLength(1);

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    a[i, j] = rnd.Next(minValue, maxValueExclusive);
        }

        private static void PrintWithSums(int[,] a)
        {
            int rows = a.GetLength(0);
            int cols = a.GetLength(1);

            int[] rowSums = new int[rows];
            int[] colSums = new int[cols];
            int total = 0;

            for (int i = 0; i < rows; i++)
            {
                int rsum = 0;
                for (int j = 0; j < cols; j++)
                {
                    int v = a[i, j];
                    rsum += v;
                    colSums[j] += v;
                }
                rowSums[i] = rsum;
                total += rsum;
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                    Console.Write($"{a[i, j],Params.CellW}");

                Console.Write(" |");
                Console.WriteLine($"{rowSums[i],Params.SumW}");
            }

            Console.WriteLine(new string('-', Params.CellW * cols + 1 + Params.SumW));

            for (int j = 0; j < cols; j++)
                Console.Write($"{colSums[j],Params.CellW}");

            Console.Write(" |");
            Console.WriteLine($"{total,Params.SumW}");
        }
    }
}
