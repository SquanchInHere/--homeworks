using System;
using System.Net.Sockets;
using System.Text;

namespace GuessTheNumber
{
    internal class Program
    {
        const ushort MAX_NUMBER = 1000;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Random random = new Random();
            ushort secret = (ushort)random.Next(1, MAX_NUMBER);

            Console.Write($"Я загадав число від 1 до {MAX_NUMBER} вагадй його: ");

            PrintMessage(secret);
        }

        static void PrintMessage(ushort secret)
        {
            ushort guess;

            while (true)
            {
                if (!ushort.TryParse(Console.ReadLine(), out guess))
                {
                    Console.Write("Будь ласка, введіть коректне число: ");
                    continue;
                }

                if (guess > MAX_NUMBER)
                {
                    Console.WriteLine("Не читеруй проказник!!! \n\n\n");
                    break;
                }
                else if (guess < secret)
                {
                    Console.Write("Загадане число більше. Спробуй ще раз: ");
                }
                else if (guess > secret)
                {
                    Console.Write("Загадане число менше. Спробуй ще раз: ");
                }
                else
                {
                    Console.WriteLine("Вітаю! Ви вгадали число!");
                    break;
                }
            }
        }
    }
}
