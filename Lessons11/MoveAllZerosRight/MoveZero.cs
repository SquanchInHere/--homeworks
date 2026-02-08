namespace MoveAllZerosRight
{
    public static class MoveZero
    {
        public static void Run()
        {
            int[,] a = {
                { 0, 1, 0, 2, 4 },
                { 1, 3, 0, 0, 15 }
            };

            ShiftNonZeroRight(a);

            Print2D(a);
        }

        static void ShiftNonZeroRight(int[,] a)
        {
            int n = a.GetLength(0);
            int m = a.GetLength(1);

            for (int i = 0; i < n; i++)
            {
                int write = m - 1;

                for (int j = m - 1; j >= 0; j--)
                {
                    int v = a[i, j];
                    if (v != 0)
                    {
                        a[i, write] = v;
                        write--;
                    }
                }

                for (int j = write; j >= 0; j--)
                    a[i, j] = 0;
            }
        }

        static void Print2D(int[,] a)
        {
            int n = a.GetLength(0);
            int m = a.GetLength(1);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (j > 0) Console.Write(" ");
                    Console.Write(a[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
