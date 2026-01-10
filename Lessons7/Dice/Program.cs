using System.Text;

namespace Dice
{
    internal class Program
    {
        const int width = 7;
        const int height = 5;
        const char dot = 'o';

        const char cornerLeftUp = '┌';
        const char cornerRightUp = '┐';
        const char cornerLeftDown = '└';
        const char cornerRightDown = '┘';
        const char HorisontalBorder = '─';
        const char VerticalBorder = '│';

        const int pX1 = 2;
        const int pY1 = 5;
        const int pX2 = 12;
        const int pY2 = 5;

        static readonly int statusY = pY1 + height + 2;

        static readonly ConsoleColor playerColor = ConsoleColor.Green;
        static readonly ConsoleColor pcColor = ConsoleColor.Red;

        static Random rnd = new Random();

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Console.WriteLine("Dice: Enter - roll (your turn), q - exit\n");

            WriteLineAt(0, pY1 - 2, "PLAYER (green) vs PC (red)");

            var old = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            DrawFrame(pX1, pY1);
            DrawFrame(pX2, pY2);
            Console.ForegroundColor = old;

            DrawDots(6, pX1, pY1, ConsoleColor.White);
            DrawDots(6, pX2, pY2, ConsoleColor.White);

            int playerTotal = 0;
            int pcTotal = 0;
            int round = 1;

            while (round <= 6)
            {
                WriteLineAt(0, statusY, $"Round {(round + 1) / 2}/3");

                if (round % 2 == 0)
                {
                    WriteLineAt(0, statusY + 1, "PC turn wait");
                    pcTotal += PcTurn();
                    Thread.Sleep(500);
                }
                else
                {
                    string input = ReadInputAt(0, statusY + 1, "Player turn Enter (q) to exit: ");

                    if (input.Equals("q", StringComparison.OrdinalIgnoreCase))
                        break;

                    if (input.Length != 0)
                    {
                        WriteLineAt(0, statusY + 1, "Only Enter to roll or q to exit");
                        continue;
                    }

                    playerTotal += PlayerTurn();
                }
                WriteLineAt(0, statusY + 4, $"Total: Player {playerTotal}  :  {pcTotal} PC");
                round++;
            }

            WriteLineAt(0, statusY + 5, $"Player {playerTotal}  :  {pcTotal} PC");

            string winner = playerTotal > pcTotal ? "PLAYER WINS!" : playerTotal < pcTotal ? "PC WINS!" : "DRAW!";

            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteLineAt(0, statusY + 7, winner);
            WriteLineAt(0, statusY + 8, "  ");
            Console.ResetColor();

            Console.CursorVisible = true;
        }

        static int PcTurn()
        {
            Thread.Sleep(400);

            int D1 = AnimateDots(pX1, pY1, pcColor);
            int D2 = AnimateDots(pX2, pY2, pcColor);

            WriteLineAt(0, statusY + 3, $"PC rolled {D1} and {D2}");
            return D1 + D2;
        }

        static int PlayerTurn()
        {
            int D1 = AnimateDots(pX1, pY1, playerColor);
            int D2 = AnimateDots(pX2, pY2, playerColor);

            WriteLineAt(0, statusY + 2, $"Player rolled: {D1} and {D2}");
            return D1 + D2;
        }

        //UI helpers
        static void WriteLineAt(int x, int y, string text)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(text);

            int rest = Console.WindowWidth - x - text.Length - 1;
            if (rest > 0) Console.Write(new string(' ', rest));
        }

        //Read input
        static string ReadInputAt(int x, int y, string prompt)
        {
            WriteLineAt(x, y, prompt);
            Console.SetCursorPosition(x + prompt.Length, y);
            return (Console.ReadLine() ?? "").Trim().ToLower();
        }

        //Dice drawing
        static void DrawFrame(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(cornerLeftUp);
            Console.Write("".PadLeft(width, HorisontalBorder));
            Console.Write(cornerRightUp);

            for (int row = 0; row < height - 2; row++)
            {
                Console.SetCursorPosition(x, y + 1 + row);
                Console.Write(VerticalBorder);
                Console.Write("".PadLeft(width, ' '));
                Console.Write(VerticalBorder);
            }

            Console.SetCursorPosition(x, y + height - 1);
            Console.Write(cornerLeftDown);
            Console.Write("".PadLeft(width, HorisontalBorder));
            Console.Write(cornerRightDown);
        }

        static void DrawDots(int value, int x, int y, ConsoleColor color)
        {
            var old = Console.ForegroundColor;
            Console.ForegroundColor = color;

            for (int r = 0; r < 3; r++)
            {
                for (int c = 1; c <= 5; c += 2)
                {
                    bool isPip = HasPip(value, r, c);
                    Console.SetCursorPosition(x + 1 + c, y + 1 + r);
                    Console.Write(isPip ? dot : ' ');
                }
            }

            Console.ForegroundColor = old;
        }

        //Animated shuffle dots in dice
        static int AnimateDots(int x, int y, ConsoleColor color)
        {
            int value = 1;
            for (int i = 0; i < 12; i++)
            {
                value = rnd.Next(1, 7);
                DrawDots(value, x, y, color);
                Thread.Sleep(40 + i * 8);
            }
            return value;
        }

        //Pips logic
        static bool HasPip(int v, int r, int c)
        {
            if (v < 1 || v > 6) return false;
            if (r < 0 || r > 2) return false;
            if (c != 1 && c != 3 && c != 5) return false;

            int gridCol = (c - 1) / 2;
            int bit = r * 3 + gridCol;

            int mask = DiceMask(v);
            return (mask & (1 << bit)) != 0;
        }

        static int DiceMask(int v)
        {
            int m = 0;

            if (v >= 2) m |= (1 << 0) | (1 << 8); // TL + BR
            if (v >= 4) m |= (1 << 2) | (1 << 6); // TR + BL
            if ((v & 1) == 1) m |= (1 << 4);      // center for 1,3,5
            if (v == 6) m |= (1 << 3) | (1 << 5); // middle left/right

            return m;
        }
    }
}
