namespace SumNumbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;
            int num = int.MinValue;
            do
            {
                Console.Write("Enter a number to display the result, enter 0: ");
                if (!int.TryParse(Console.ReadLine(), out int input))
                {
                    Console.WriteLine("Incorrect value entered");
                    continue;
                }

                num = input;
                sum += num;
            }
            while (num != 0);

            Console.WriteLine($"The sum of the numbers you entered is {sum}");
        }
    }
}
