using System;
using System.Drawing;

namespace Rectangle
{
    internal class Program
    {
        const int min = 2;

        static void Main(string[] args)
        {
            if (!SetIntOrQuit($"Enter width (>= {min}) or (q) for quit: ", out int width)) return;
            if (!SetIntOrQuit($"Enter height (>= {min}) or (q) for quit: ", out int height)) return;

            if (!SetCharOrQuit(out char ch)) return;

            if (!SetColorOrQuit(out ConsoleColor color)) return;

            DrawHollowRectangle(width, height, ch, color);
        }

        //Set width height size
        static bool SetIntOrQuit(string prompt, out int value)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (isQuit(input))
            {
                value = 0;
                return Exit();
            }

            if (!int.TryParse(input, out value) && value < min)
            {
                Console.WriteLine($"Error. Enter an integer >= {min} or (q) for quit.");
                SetIntOrQuit(prompt, out value);
            }

            return true;
        }

        //Set Chart
        static bool SetCharOrQuit(out char ch)
        {
            Console.Write("Enter the symbol for the frame (1 char): ");
            string? input = Console.ReadLine();

            if (isQuit(input))
            {
                ch = '"';
                return Exit();
            }

            if (!char.TryParse(input, out ch) || input.Length != 1)
            {
                Console.WriteLine("Error. Enter exactly 1 symbol.");
                SetCharOrQuit(out ch);
            }

            return true;
        }

        //Read color list menu and try set color
        static bool SetColorOrQuit(out ConsoleColor color)
        {
            Console.WriteLine("Enter a color name or number (0..15). Enter 'q' to exit.");
            PrintColorMenu();

            while (true)
            {
                Console.Write("Color: ");
                string? input = Console.ReadLine();

                if (isQuit(input))
                {
                    color = Console.ForegroundColor;
                    return Exit();
                }


                if (TrySetStaticColor(input.Trim().ToLower(), out color))
                    return true;

                Console.WriteLine("Error. Please enter a valid color name or number from the list for quit enter (q).");
                SetColorOrQuit(out color);
            }
        }

        //Print color list from TrySetStaticColor
        static void PrintColorMenu()
        {
            for (int i = 0; i <= 15; i++)
            {
                TrySetStaticColor(i.ToString(), out ConsoleColor color);
                Console.WriteLine($"{i}: {color}");
            }
        }

        //Set color for render rectangle
        static bool TrySetStaticColor(string s, out ConsoleColor color)
        {
            color = ConsoleColor.White;

            switch (s)
            {
                case "0":
                case "darkblue":
                    color = ConsoleColor.DarkBlue;
                    return true;
                case "1":
                case "darkgreen":
                    color = ConsoleColor.DarkGreen;
                    return true;
                case "2":
                case "darkred":
                    color = ConsoleColor.DarkRed;
                    return true;
                case "3":
                case "darkyellow":
                    color = ConsoleColor.DarkYellow;
                    return true;
                case "4":
                case "darkgray":
                    color = ConsoleColor.DarkGray;
                    return true;
                case "5":
                case "darkmagenta":
                    color = ConsoleColor.DarkMagenta;
                    return true;
                case "6":
                case "darkcyan":
                    color = ConsoleColor.DarkCyan;
                    return true;
                case "7":
                case "gray":
                    color = ConsoleColor.Gray;
                    return true;
                case "8":
                case "blue":
                    color = ConsoleColor.Blue;
                    return true;
                case "9":
                case "green":
                    color = ConsoleColor.Green;
                    return true;
                case "10":
                case "red":
                    color = ConsoleColor.Red;
                    return true;
                case "11":
                case "cyan":
                    color = ConsoleColor.Cyan;
                    return true;
                case "12":
                case "magenta":
                    color = ConsoleColor.Magenta;
                    return true;
                case "13":
                case "yellow":
                    color = ConsoleColor.Yellow;
                    return true;
                case "14":
                case "white":
                    color = ConsoleColor.White;
                    return true;
                case "15":
                case "black":
                    color = ConsoleColor.Black;
                    return true;
                default:
                    return false;
            }
        }

        static void DrawHollowRectangle(int width, int height, char ch, ConsoleColor color)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            Console.WriteLine("".PadLeft(width, ch));

            for (int h = 0; h < height - 2; h++)
            {
                Console.Write(ch);
                Console.Write(" ".PadLeft(width - 2));
                Console.WriteLine(ch);
            }

            Console.WriteLine("".PadLeft(width, ch));

            Console.ForegroundColor = oldColor;
        }

        static bool isQuit(string input)
        {
            return input.Trim().ToLower().Equals("q");
        }

        static bool Exit()
        {
            Console.WriteLine("Exit!");
            return false;
        }
    }
}

