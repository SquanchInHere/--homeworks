namespace TornArr
{
    public static class ShuffleArr
    {

        public static void Run()
        {
            int[][] arr = Create2DArrayByRules();

            Console.WriteLine("\nResult:");
            PrintJaggedArray(arr);
        }

        private static int[][] Create2DArrayByRules()
        {
            int rows = ReadInt("Enter number of rows: ", min: 1);

            int[][] a = new int[rows][];

            for (int i = 0; i < rows; i++)
            {
                int cols = ReadInt($"Enter number of elements in row {i + 1}: ", min: 1);
                a[i] = new int[cols];

                int first = ReadInt($"Enter the first element of row {i + 1}: ");
                a[i][0] = first;

                for (int j = 1; j < cols; j++)
                {
                    long calc = (long)a[i][j - 1] * 2;
                    a[i][j] = (calc > Params.Limit) ? first : (int)calc;
                }
            }

            return a;
        }

        private static int ReadInt(string prompt, int? min = null)
        {
            while (true)
            {
                Console.Write(prompt);
                string? s = Console.ReadLine();

                if (int.TryParse(s, out int value))
                {
                    if (min.HasValue && value < min.Value)
                    {
                        Console.WriteLine($"Value must be >= {min.Value}");
                        continue;
                    }
                    return value;
                }

                Console.WriteLine("Invalid integer. Try again.");
            }
        }

        private static void PrintJaggedArray(int[][] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a[i].Length; j++)
                {
                    Console.Write($"{a[i][j],6}");
                }
                Console.WriteLine();
            }
        }

    }
}
