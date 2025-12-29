namespace MultipplicationTable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = 0;

            while (true) {
                Console.Write("Введіть цифру на яку ви хочете отримати таблицю множення: ");
                if (!int.TryParse(Console.ReadLine(), out n))
                {
                    Console.WriteLine("\nВведено невірне значення!");
                    continue;
                }

                for (int i = 1; i <= 9; i++)
                {
                    Console.WriteLine($"{i}x{n}={i * n}");
                }
                Console.ReadKey();
                return;
            }
        }
    }
}
