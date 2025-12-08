using System;
using System.Text;

namespace GuessTheNumberPC
{
    internal class Program
    {
        const int MIN_NUMBER = 1;
        const int MAX_NUMBER = 10;
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int numberToGuess;
            int prevGuess = 0;

            while (true)
            {
                numberToGuess = GetNewRandom(prevGuess);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Ти загадав мені число від {MIN_NUMBER} до {MAX_NUMBER} це число {numberToGuess} ?");

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Введи 'yes' якщо я вгадав або 'no' якщо ні: ");

                string userResponse = Console.ReadLine();

                switch (userResponse)
                {
                    case "yes":
                        Console.WriteLine("Ура! Я вгадав твоє число!");
                        return;
                    case "no":
                        prevGuess = MoreOrLess(numberToGuess);
                        break;
                    default:
                        PrintErrorMessage();
                        continue;
                }
            }
        }

        static int MoreOrLess(int numberToGuess)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Чи моє число більше за твоє? (введи 'yes' або 'no'): ");

            string hintResponse = Console.ReadLine();

            switch (hintResponse)
            {
                case "yes":
                    numberToGuess -= 1;
                    break;
                case "no":
                    numberToGuess += 1;
                    break;
                default:
                    PrintErrorMessage();
                    break;
            }

            return numberToGuess;
        }

        static void PrintErrorMessage()
        {
            Console.WriteLine("Будь ласка, введи 'так' або 'ні'.");
        }

        static int GetNewRandom(int prev)
        {
            Random random = new Random();

            int value;
            do
            {
                value = random.Next(MIN_NUMBER, MAX_NUMBER + 1);
            }
            while (value == prev);

            return value;
        }
    }
}
