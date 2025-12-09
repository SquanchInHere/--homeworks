using System;
using System.Globalization;
using System.Text;

namespace RunnersSpeed
{
    internal class Program
    {
        const double SECONDS_IN_HOUR = 3600.0;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Обчислення швидкості бігу.");

            int d = SetDistanceInput();
            double t = SetTimeInput();
            double s = ConvertTimeToSeconds(t);
            PrintFormatTime(s);

//            Console.WriteLine($"{minutes} {secs}");

            Console.WriteLine($"Ви бігли зі швидкістю {CalculateSpeed(d, s):F2} км/год.");
        }

        static double SetTimeInput()
        {
            double time;

            while (true)
            {
                Console.Write("Введіть час бігу (хв.сек 1.26): ");

                if (!double.TryParse(Console.ReadLine(), out time))
                    Console.WriteLine("Некоректне значення довжини дистанції.");
                else
                {
                    return time;
                }
            }
        }

        static int SetDistanceInput()
        {
            int distance;

            while (true)
            {
                Console.Write("Введіть довжину дистанції (метри): ");

                if (!int.TryParse(Console.ReadLine(), out distance))
                    Console.WriteLine("Некоректне значення довжини дистанції.");
                else
                {
                    Console.WriteLine($"Дистанція : {distance} м.");
                    return distance;
                }
            }
        }

        static double ConvertTimeToSeconds(double time)
        {
            int minutes = (int)time;
            double seconds = (time - minutes) * 100;

            return minutes * 60 + seconds;
        }

        static double CalculateSpeed(int distance, double time)
        {
            double distanceTime = SECONDS_IN_HOUR / distance;
            return (distance / time) * distanceTime;
        }

        static void PrintFormatTime(double seconds)
        {
            int minutes = (int)(seconds / 60);
            int secs = (int)(seconds % 60);

            Console.WriteLine($"Час: {minutes} хв {secs} сек = {seconds:F2}");
        }

    }
}
