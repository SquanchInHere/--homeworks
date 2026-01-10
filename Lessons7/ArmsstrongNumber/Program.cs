namespace ArmsstrongNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int num = SetNumber();

            if (num < 0)
            {
                Console.WriteLine("Exit!");
                return;
            }

            Console.WriteLine(IsArmstrong(num) ? "Armstrong number" : "Not an Armstrong number");
        }

        //Set number or Quit
        static int SetNumber()
        {
            Console.Write("Enter a non-negative integer or (q) for Quit: ");
            string? input = Console.ReadLine();

            if (input.Trim().ToLower().Equals("q"))
            {
                return -1;
            }

            if (!int.TryParse(input, out int n) || n < 0)
            {
                Console.WriteLine("Invalid input.");
                SetNumber();
            }

            return n;
        }

        static bool IsArmstrong(int n)
        {
            if (n < 0) return false;
            if (n == 0) return true;

            int k = CountDigits(n);

            int sum = 0;
            int t = n;
            while (t > 0)
            {
                int digit = t % 10;
                sum += PowInt(digit, k);
                t /= 10;
            }

            return sum == n;
        }

        static int CountDigits(int n)
        {
            int c = 0;
            while (n > 0) { c++; n /= 10; }
            return c;
        }

        static int PowInt(int a, int p)
        {
            int r = 1;
            for (int i = 0; i < p; i++) r *= a;
            return r;
        }
    }
}
