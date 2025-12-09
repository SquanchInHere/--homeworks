using System;
using System.Text;

namespace FormatDays
{
    internal class Program
    {
        const int DAYS_IN_WEEK = 7;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.Write("Введіть кількість днів: ");

                if (int.TryParse(Console.ReadLine(), out int totalDays) && totalDays > 0)
                {
                    int weeks = totalDays / DAYS_IN_WEEK;
                    int days = totalDays % DAYS_IN_WEEK;
                    Console.WriteLine($"{totalDays} днів = {weeks} тиж. та {days} дн.");
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Некоректне введення. число має бути цілим та більше нуля і не може бути строкою!");
                    Console.ResetColor();
                }
            }
        }
    }
}
