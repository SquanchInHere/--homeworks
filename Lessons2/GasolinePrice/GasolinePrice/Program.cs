using System;
using System.Text;

namespace GasolinePrice
{
    internal class Program
    {
        static readonly string[] gasoline = {
            "ДП",
            "А-95",
            "А-98",
        };

        static double[] prices = new double[gasoline.Length];
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("Введіть витрату бензину на 100км: ");
            double consumption = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введіть відстань у км: ");
            double distance = Convert.ToDouble(Console.ReadLine());

            initPrices();

            printHeader();
            printRow(consumption, distance);
            printFooter();
        }

        static void initPrices()
        {
            for (int i = 0; i < gasoline.Length; i++)
            {
                Console.Write($"Введіть ціну за літр {gasoline[i]}: ");
                string input = Console.ReadLine();
                double price;

                while (!double.TryParse(input, out price))
                {
                    Console.WriteLine("Некорректне значення значення повинно бути записано через \", наприклад 20,2\"");
                    Console.Write($"Введіть ціну за літр {gasoline[i]}: ");
                    input = Console.ReadLine();
                }

                prices[i] = price;
            }
        }

        static void printHeader()
        {
            Console.Clear();
            Console.WriteLine("+--------------+----------------------------+------------+----------+----------------------+");
            Console.WriteLine("| Тип палива   | Витрати пального на 100 км | Ціна за 1л | Відстань | Вартість поїздки     |");
            Console.WriteLine("+--------------+----------------------------+------------+----------+----------------------+");
        }

        static void printFooter()
        {
            Console.WriteLine("+--------------+----------------------------+------------+----------+----------------------+");
        }

        static void printRow(double consumption, double distance)
        {
            for (int i = 0; i < gasoline.Length; i++)
            {
                double totalCost = (consumption / 100) * distance * prices[i];

                Console.WriteLine($"| {gasoline[i],-12} | {consumption,-22:F2} грн | {prices[i],-10:F2} | {distance,-8} | {totalCost,-16:F2} грн |");
            }
        }
    }
}
