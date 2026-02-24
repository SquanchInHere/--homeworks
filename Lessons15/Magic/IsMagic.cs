namespace Swapping
{
    internal class IsMagic
    {
        public static void Run(int[,] a)
        {
            Console.WriteLine("Matrix:");
            Helper.PrintMatrix(a);
            Console.WriteLine($"Is magic: {(IsMagicSquare(a) ? "yeap" : "noup")}");
        }


        static bool IsMagicSquare(int[,] m)
        {
            if (m == null) return false;

            int n = m.GetLength(0);
            if (n == 0 || m.GetLength(1) != n) return false;

            int target = 0;
            for (int c = 0; c < n; c++) target += m[0, c];

            for (int r = 0; r < n; r++)
            {
                int sum = 0;
                for (int c = 0; c < n; c++) sum += m[r, c];
                if (sum != target) return false;
            }

            for (int c = 0; c < n; c++)
            {
                int sum = 0;
                for (int r = 0; r < n; r++) sum += m[r, c];
                if (sum != target) return false;
            }

            int d1 = 0;
            for (int i = 0; i < n; i++) d1 += m[i, i];
            if (d1 != target) return false;

            int d2 = 0;
            for (int i = 0; i < n; i++) d2 += m[i, n - 1 - i];
            if (d2 != target) return false;

            return true;
        }
    }
}
