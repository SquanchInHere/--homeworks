using System;
using System.Text;

namespace LeftDateTime
{
    internal class Program
    {
        const int ONE_HOUR_IN_SECONDS = 3600;
        const int ONE_DAY_IN_SECONDS = 24 * ONE_HOUR_IN_SECONDS;
        const int ONE_MINUTE = 60;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.Write("Введіть секунди від початку дня: ");

            if (!int.TryParse(Console.ReadLine(), out int s) && s < 0)
            {
                Console.WriteLine("Невірний формат вводу!");
                return;
            }

            int hours = s / ONE_HOUR_IN_SECONDS;
            int minutes = (s % ONE_HOUR_IN_SECONDS) / ONE_MINUTE;
            int seconds = s % ONE_MINUTE;

            Console.WriteLine($"Поточний час: {hours} год, {minutes} хв, {seconds} сек");
            
            int remaining = ONE_DAY_IN_SECONDS - s;

            int lHours = remaining / ONE_HOUR_IN_SECONDS;
            int lMinutes = (remaining % ONE_HOUR_IN_SECONDS) / ONE_MINUTE;
            int lSeconds = remaining % ONE_MINUTE;

            Console.WriteLine($"До опівночі залишилось: {lHours} год, {lMinutes} хв, {lSeconds} сек");
        }
    }
}
