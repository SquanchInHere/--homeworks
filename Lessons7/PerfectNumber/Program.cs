using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PerfectNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (!TrySetNumber(out int value)) return;

            Console.WriteLine(IsPerfectNumber(value) ? "Number is Perfect" : "Number is not perfect!");
        }

        static bool TrySetNumber(out int value)
        {
            Console.Write("Enter the number you think is perfect or (q) for Quit: ");
            string? input = Console.ReadLine();

            if (input.Trim().ToLower().Equals("q"))
            {
                Console.WriteLine("Exit!");
                value = 0;
                return false;
            }

            if (!int.TryParse(input, out value) && value < 0)
            {
                Console.WriteLine("Invalid value!");
                TrySetNumber(out value);
                return false;
            }

            return true;
        }

        static bool IsPerfectNumber(int number)
        {
            if (number <= 1) return false;

            int sum = 0;
            for (int i = 1; i <= number / 2; i++)
                if (number % i == 0) sum += i;

            return sum == number;
        }
    }
}
