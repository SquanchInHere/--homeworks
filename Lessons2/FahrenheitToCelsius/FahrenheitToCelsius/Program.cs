using System;
using System.Text;

namespace FahrenheitToCelsius
{
    internal class Program
    {
        const int diff = 32;
        const double factor = (9d/5d);

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Оберіть яку температуру ви будете конвертувати:");

            int choice = PrintChoice();

            ConvertTemperature(choice);
        }

        static void ConvertTemperature(int choice)
        {
            double F = 0;
            double C = 0;

            while (choice != 1 && choice != 2 && choice != 3)
            {

                Console.WriteLine("Невірний вибір. Будь ласка, виберіть 1 або 2.");
                choice = PrintChoice();
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Введіть температуру у Фаренгейтах: ");
                    F = Convert.ToDouble(Console.ReadLine());
                    C = ConvertFahrenheitToCelsius(F);
                    Console.WriteLine($"{F:F2}°F дорівнює {C:F2}°C");
                    break;
                case 2:
                    Console.Write("Введіть температуру у Цельсіях: ");
                    C = Convert.ToDouble(Console.ReadLine());
                    F = ConvertCelsiusToFahrenheit(C);
                    Console.WriteLine($"{C:F2}°C дорівнює {F:F2}°F");
                    break;
                case 3:
                    break;
            }
        }

        static double ConvertFahrenheitToCelsius(double fahrenheit)
        {
            return (fahrenheit - diff) / factor;
        }

        static double ConvertCelsiusToFahrenheit(double celsius)
        {
            return (celsius * factor) + diff;
        }

        static int PrintChoice(){
            Console.WriteLine("1. °F -> °C");
            Console.WriteLine("2. °C -> °F");
            Console.WriteLine("3. Вийти.");
            Console.Write("Оберіть (1 - 3): ");

            return int.Parse(Console.ReadLine());
        }
    }
}
