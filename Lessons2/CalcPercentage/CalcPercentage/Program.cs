using System;
using System.Text;

namespace CalcPercentage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.Write("Введіть число знайти відсоток: ");
            double number = double.Parse(Console.ReadLine());

            Console.Write("Введіть відсоток: ");
            double percentage = double.Parse(Console.ReadLine());

            double result = (number * percentage) / 100;

            Console.WriteLine($"{percentage}% від {number} дорівнює {result}");
        }
    }
}
