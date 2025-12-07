using System;
using System.Threading;

namespace HomeworkBio
{
    internal class Bio
    {
        static readonly string[] bioData = {
            "Улюблений фільм: Кримінальне чтиво (1994)",
            "Улюблений мультфільм: Богиня благословляє цей чудовий світ (Kono Subarashii Sekai ni Shukufuku wo!)",
            "Улюблена пісня: Disturbed - The Sound of Silence",
            "Куплет з пісні:",
            "   And in the naked light I saw",
            "   Ten thousand people, maybe more",
            "   People talking without speaking",
            "   People hearing without listening",
            "   People writing songs that voices never share",
            "   And no one dare",
            "   Disturb the sound of silence",
            "   ------------------------------------------  ",
            "   \"Fools\" said I, 'You do not know",
            "   Silence like a cancer grows",
            "   Hear my words that I might teach you",
            "   Take my arms that I might reach you",
            "   But my words like silent raindrops fell",
            "   And echoed",
            "   In the wells of silence",
            "   ------------------------------------------  ",
            "   And the people bowed and prayed",
            "   To the neon god they made",
            "   And the sign flashed out its warning",
            "   In the words that it was forming",
            "   And the sign said",
            "   The words of the prophets",
            "   are written on the subway walls",
            "   and tenement halls",
            "   and whisper'd in the sounds of silence",
            "Співак:  Art Garfunkel",
            "Автори пісні:  Paul Simon"
        };

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            SetupConsoleColor();
            Console.WriteLine("Bio Description:");

            foreach (string line in bioData)
                printBio(line, 10);

            SetupConsole();
        }

        static void SetupConsoleColor()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }

        static void SetupConsole()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
            Console.Write("> ");
            Console.CursorVisible = true;
            Console.ReadLine();

            Console.WindowWidth = 80;
            Console.WindowHeight = 20;
            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;
        }

        static void printBio(string line, int delay = 100)
        {
            if (line.Contains("---"))
                Console.WriteLine(line);
            else
                foreach (char c in line)
                {
                    Console.Write(c);
                    Thread.Sleep(delay);
                }
                Console.WriteLine();
        }
    }
}
