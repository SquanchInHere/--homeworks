namespace NumberInRange
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = 0, b = 0, step = 0;

            while (step < 2)
            {
                Console.Write("Enter value of the range: ");
                if (!int.TryParse(Console.ReadLine(), out int value))
                {
                    Console.WriteLine("Invalid first value!");
                    continue;
                }

                if (step == 0) a = value;
                else b = value;

                step++;
            }

            int start = Math.Min(a, b);
            int end = Math.Max(a, b);

            int number = EnterNumber(start, end);

            Console.WriteLine($"Number {number} is in range {start} - {end}.");
            Console.ReadKey();

        }

        static int EnterNumber(int start, int end)
        {
            Console.Write($"Enter a number in range {start} - {end}: ");

            if (!int.TryParse(Console.ReadLine(), out int value))
            {
                Console.WriteLine("Invalid value!");
                EnterNumber(start, end);
            }

            if (value < start || value > end)
            {
                Console.WriteLine("Number is out of range. Try again.");
                EnterNumber(start, end);
            }

            return value;
        }
    }
}
