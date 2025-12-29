namespace IntegeRange
{
    internal class Program
    {
        const int divide = 10;

        static void Main(string[] args)
        {
            int a = 0, b = 0, step = 0;
            while (step < 2)
            {
                Console.Write($"Enter Range {( step == 0 ? "A" : "B" )}: ");
                if (!int.TryParse(Console.ReadLine(), out int value))
                {
                    Console.WriteLine($"Incorrect value {(step == 0 ? "A" : "B")} entered!");
                    continue;
                }

                if (step == 0) a = value;
                else b = value;

                step++;
            }

            (int start, int end) = a <= b ? (a, b) : (b, a);

            int count = 0;
            int mask;
            bool ok;

            for (int i = start; i <= end; i++)
            {
                ok = true;
                mask = 0;
                int n = (i < 0 ? -i : i);

                do
                {
                    int bit = 1 << (n % divide);

                    if ((mask & bit) != 0) { ok = false; break; }

                    mask |= bit;
                    n /= divide;
                } while (n > 0);

                if (ok) count++;
            }

            Console.WriteLine($"Number of numbers in a range with different digits: {count}");
            return;
        }
    }
}
