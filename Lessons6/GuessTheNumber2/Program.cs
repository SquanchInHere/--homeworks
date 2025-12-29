using System;
using System.Net.Sockets;

namespace GuessTheNumber2
{
    internal class Program
    {
        static int secret, attempts = 0;
        static Random rnd = new Random();
        const int min = 1;
        const int max = 500;

        static void Main(string[] args)
        {
            secret = rnd.Next(min, max + 1);

            Console.WriteLine($"Guessed the number {min} - {max} (q/exit — exit)");

            while (true)
            {
                if (!TryReadGuess(out var guess)) continue;

                if (guess is null)
                {
                    Console.WriteLine($"Exit");
                    return;
                }

                attempts++;

                if (guess < secret) Console.WriteLine("More");
                else if (guess > secret) Console.WriteLine("Less");
                else
                {
                    Console.WriteLine($"Guessed in {attempts} attempts. Number: {secret}");
                    return;
                }
            }
        }

        static bool TryReadGuess(out int? guess)
        {
            Console.Write("Attempt: ");
            var s = (Console.ReadLine() ?? "").Trim().ToLower();

            if (s == "q" || s == "exit")
            {
                guess = null;
                return true;
            }

            if (!int.TryParse(s, out var v) || v < min || v > max)
            {
                Console.WriteLine($"Enter an integer {min} - {max} or q/exit.");
                guess = 0;
                return false;
            }

            guess = v;
            return true;
        }
    }

}
