using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcSpeed
{
    internal class Calc
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Програма для розрахунку швидкості руху");

            Console.Write("Введіть відстань (км): ");
            double S = double.Parse(Console.ReadLine());

            Console.Write("Введіть час (хв): ");
            double t = double.Parse(Console.ReadLine());

            double V = S / (t / 60);

            Console.WriteLine($"Швидкість: {V} км/год");
        }
    }
}
