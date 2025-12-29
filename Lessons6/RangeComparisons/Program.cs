using System.Text;

namespace RangeComparisons
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int step = 0;
            int a = 0, b = 0;

            while (step < 2)
            {
                Console.Write($"Enter {( step == 0 ? "Start" : "End")} Range: ");
                if (!int.TryParse(Console.ReadLine(), out int value))
                {
                    Console.WriteLine("Incorrectly entered data!");
                    continue;
                }

                if (step == 0) a = value;
                else b = value;

                step++;
            }

            (int start, int end) = a <= b ? (a, b) : (b, a);

            Console.WriteLine("\nAll even numbers in the range: ");
            for (int i = start; i <= end; i++)
                if (i % 2 == 0)  Console.WriteLine($"{start}+{end}={i}");

            Console.WriteLine("\nAll numbers that are multiples of seven: ");
            for (int i = start; i <= end; i++)
                if (i % 7 == 0) Console.WriteLine($"{start}+{end}={i}");

            Console.ReadKey();
            return;
        }
    }
}
