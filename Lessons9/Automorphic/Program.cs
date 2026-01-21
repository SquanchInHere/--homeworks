namespace Automorphic
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            PrintAutomorphicInRange(1, 10000);
        }

        private static void PrintAutomorphicInRange(int from, int to)
        {
            if (from > to)
            {
                int tmp = from; from = to; to = tmp;
            }

            for (int n = from; n <= to; n++)
            {
                if (IsAutomorphic(n))
                    Console.WriteLine(n);
            }
        }

        private static bool IsAutomorphic(int n)
        {
            if (n < 0) return false;

            long sq = (long)n * (long)n;

            long mod = 1;
            int t = n;

            if (t == 0) mod = 10;
            else
            {
                while (t > 0)
                {
                    mod *= 10;
                    t /= 10;
                }
            }

            return (sq % mod) == n;
        }
    }
}
