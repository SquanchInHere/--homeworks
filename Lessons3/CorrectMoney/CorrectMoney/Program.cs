using System;
using System.Text;

namespace CorrectMoney
{
    internal class Program
    {
        const int COEFFICIENT = 100;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            int hryvnia = SetHryvnia();
            int kopiyka = SetKopiyka();

            CalculateSum(hryvnia, kopiyka);
        }

        static int SetHryvnia()
        {
            int hryvnia = 0;

            while (true)
            {
                Console.Write("Введіть сумму в (грн): ");

                if (!int.TryParse(Console.ReadLine(), out hryvnia) && hryvnia < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Некоректне введенно значення!");
                    Console.ResetColor();
                }
                else
                    return hryvnia;
            }
        }

        static int SetKopiyka()
        {
            int kopiyka = 0;

            while (true)
            {
                Console.Write("Введіть сумму в (коп): ");

                if (!int.TryParse(Console.ReadLine(), out kopiyka) && kopiyka < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Некоректне введенно значення!");
                    Console.ResetColor();
                }
                else
                    return kopiyka;
            }
        }

        static void CalculateSum(int hryvnia , int kopiyka)
        {
            int total = hryvnia * COEFFICIENT + kopiyka;
            int resultHryvnia = total / COEFFICIENT;
            int resultKopiyka = total % COEFFICIENT;

            Console.WriteLine($"Ваша сумма дорівнює: {resultHryvnia} грн {resultKopiyka} коп.");
        }
    }
}
