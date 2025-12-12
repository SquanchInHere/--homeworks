using System;
using System.Text;

namespace Parallelepiped
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Обчислення об'єму пралелепіпеда.");
            Console.WriteLine("Введіть вихідні дані:");
            double length = SetLength();
            double width = SetWidth();
            double height = SetHeight();

            double volume = width * height * length;
            Console.WriteLine($"Об'єм: {volume:F2} см³");
        }

        static double SetWidth()
        {
            double width;

            while (true)
            {
                Console.Write("Ширина: ");
                if (double.TryParse(Console.ReadLine(), out width) && width > 0)
                {
                    Console.WriteLine($"Ширина (см) -> {width};");
                    return width;
                }
                else
                {
                    Console.WriteLine("Некоректне значення Ширина");
                }
            }
        }

        static double SetHeight()
        {
            double height;
            while (true)
            {
                Console.Write("Висота: ");

                if (double.TryParse(Console.ReadLine(), out height) && height > 0)
                {
                    Console.WriteLine($"Висота (см) -> {height};");
                    return height;
                }
                else
                {
                    Console.WriteLine("Некоректне значення Висота");
                }
            }
        }

        static double SetLength()
        {
            double length;
            while (true)
            {
                Console.Write("Довжина: ");

                if (double.TryParse(Console.ReadLine(), out length) && length > 0)
                {
                    Console.WriteLine($"Довжина (см) -> {length};");
                    return length;
                }
                else
                {
                    Console.WriteLine("Некоректне значення Довжина");
                }
            }
        }
    }
}
