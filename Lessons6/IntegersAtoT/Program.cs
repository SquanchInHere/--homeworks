namespace IntegersAtoT
{
    internal class Program
    {
        const int CONTROL_NUM = 20;

        static void Main()
        {
            Console.Write("Enter A (1..20): ");
            int a = int.Parse(Console.ReadLine()!);

            long p = 1;
            for (int i = a; i <= CONTROL_NUM; i++)
                p *= i;

            Console.WriteLine($"Income from {a} to 20 = {p}");
        }
    }
}
