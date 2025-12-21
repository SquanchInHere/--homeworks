using System.Globalization;
using System.Text;

namespace BrickSize
{
    internal class Program
    {
        const double a = 25;
        const double b = 12;
        const double c = 6.5;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine($"Дано дійсні позитивні числа a = {a}, b = {b}, c = {c}.");
            Console.WriteLine("\nВведіть розмір (x) та (y) в см. щоб з'ясувати чи пройде цеглина !");
            double x = ReadPositiveDouble("x");
            double y = ReadPositiveDouble("y");

            bool fits = Fits(a, b, c, x, y);

            Console.WriteLine(fits ? "Цеглина пройде у отвір." : "Цеглина не пройде у отвір.");
        }

        static double ReadPositiveDouble(string prompt)
        {
            while (true)
            {
                Console.Write($"Введіть {prompt}: ");

                if (!double.TryParse(Console.ReadLine(), out double size) && size < 0)
                    Console.WriteLine("Помилка вводу. Введіть дійсне додатне число.");

                return size;
            }
        }

        static bool Fits(double a, double b, double c, double x, double y)
        {
            return FitRect(a, b, x, y) || FitRect(a, c, x, y) || FitRect(b, c, x, y);
        }

        static bool FitRect(double p, double q, double x, double y)
        {
            return (p <= x && q <= y) || (p <= y && q <= x);
        }
    }
}
