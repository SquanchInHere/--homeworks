using System;
using System.Text;

namespace LifeGame
{
    class Program
    {
        const char ALIVE_CHAR = '\u2B1B';

        static int W = 60;
        static int H = 25;

        static int DELAY_MS = 150;
        const double DENSITY = 0.22;

        static bool[,] alive = new bool [H, W];
        static int[,] age = new int [H, W];

        static int tick = 0;
        static bool paused = false;
        static readonly Random rnd = new Random();

        static bool running = true;

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);

            HandlerInput();
            InitSize();
            InitRandom();

            while (running)
            {
                HandleKeys();

                if (!paused)
                {
                    Step();
                    Render();
                }

                Thread.Sleep(DELAY_MS);
            }
        }

        static void InitRandom()
        {
            tick = 0;
            Array.Clear(alive, 0, alive.Length);
            Array.Clear(age, 0, age.Length);

            for (int y = 0; y < H; y++)
                for (int x = 0; x < W; x++)
                {
                    bool a = rnd.NextDouble() < DENSITY;
                    alive[y, x] = a;
                    age[y, x] = a ? 1 : 0;
                }
        }

        static void ClearAll()
        {
            tick = 0;
            Array.Clear(alive, 0, alive.Length);
            Array.Clear(age, 0, age.Length);
        }

        static void HandleKeys()
        {
            while (Console.KeyAvailable)
            {
                var k = Console.ReadKey(true).Key;

                if (k == ConsoleKey.Escape)
                {
                    Console.ResetColor();
                    Console.Clear();
                    running = !running;
                }

                if (k == ConsoleKey.Spacebar) paused = !paused;
                if (k == ConsoleKey.N && paused) Step();
                if (k == ConsoleKey.R) InitRandom();
                if (k == ConsoleKey.C) ClearAll();
            }
        }

        static void Step()
        {
            bool[,] next = new bool[H, W];
            int[,] nextAge = new int[H, W];

            for (int y = 0; y < H; y++)
                for (int x = 0; x < W; x++)
                {
                    int n = CountNeighbors(x, y);

                    if (alive[y, x])
                    {
                        bool survives = (n == 2 || n == 3);
                        next[y, x] = survives;
                        nextAge[y, x] = survives ? age[y, x] + 1 : 0;
                    }
                    else
                    {
                        bool born = (n == 3);
                        next[y, x] = born;
                        nextAge[y, x] = born ? 1 : 0;
                    }
                }

            Console.Clear();

            alive = next;
            age = nextAge;
            tick++;
        }

        static int CountNeighbors(int x, int y)
        {
            int c = 0;

            for (int dy = -1; dy <= 1; dy++)
                for (int dx = -1; dx <= 1; dx++)
                {
                    if (dx == 0 && dy == 0) continue;

                    int nx = x + dx;
                    int ny = y + dy;

                    if (nx < 0 || nx >= W || ny < 0 || ny >= H) continue;
                    if (alive[ny, nx]) c++;
                }

            return c;
        }

        static int CountAlive()
        {
            int c = 0;
            for (int y = 0; y < H; y++)
                for (int x = 0; x < W; x++)
                    if (alive[y, x]) c++;
            return c;
        }

        static void Render()
        {
            MainMenu();

            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    if (!alive[y, x])
                    {
                        Console.Write(' ');
                        continue;
                    }

                    Console.ForegroundColor = ColorByAge(age[y, x]);
                    Console.Write(ALIVE_CHAR);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        static ConsoleColor ColorByAge(int a)
        {
            if (a <= 1) return ConsoleColor.Green;
            if (a <= 3) return ConsoleColor.Cyan;
            if (a <= 6) return ConsoleColor.Blue;
            if (a <= 10) return ConsoleColor.Magenta;
            return ConsoleColor.Yellow;
        }

        static void MainMenu()
        {
            Console.ResetColor();
            Console.WriteLine("Життя. Esc=вихід | Space=пауза | N=крок | R=Перегенерувати | C=очистити");
            Console.WriteLine($"Покоління: {tick}   Стан: {(paused ? "PAUSE" : "RUN")}   Живих: {CountAlive()}");
            Console.WriteLine();
        }

        static void HandlerInput()
        {
            Console.Write("Введіть ширину поля або залиште порожнім: ");
            if (int.TryParse(Console.ReadLine(), out int width))
                W = width;

            Console.Write("Введіть висоту поля або залиште порожнім: ");
            if (int.TryParse(Console.ReadLine(), out int height))
                H = height;

            Console.Write("Введіть швидкість кроку в ms або залиште порожнім: ");
            if (int.TryParse(Console.ReadLine(), out int ms))
                DELAY_MS = ms;
        }

        static void InitSize()
        {
            alive = new bool[H, W];
            age = new int[H, W];
            Console.Clear();
        }
    }
}