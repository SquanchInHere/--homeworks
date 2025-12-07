using System;

namespace Lesson2FifteenGame3_12_2025
{
    class FifteenGame
    {
        static int[,] matrix = new int[4, 4];
        static int emptyX = 3, emptyY = 3;

        static void Main()
        {
            InitMatrix();
            ShuffleMatrix();

            while (true)
            {
                Console.Clear();
                PrintMatrix();

                if (Finish())
                {
                    Console.WriteLine("\nCongratulations, you win.");
                    break;
                }

                InitHandleInput();
            }
        }

        static void InitMatrix()
        {
            int v = 1;

            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 4; y++)
                    matrix[x, y] = v++;

            matrix[3, 3] = 0;
        }

        private static void ShuffleMatrix()
        {
            Random rnd = new Random();

            for (int i = 0; i < 200; i++)
            {
                int dir = rnd.Next(4);
                switch (dir)
                {
                    case 0:
                        Move(-1, 0);
                        break;
                    case 1:
                        Move(1, 0);
                        break;
                    case 2:
                        Move(0, -1);
                        break;
                    case 3:
                        Move(0, 1);
                        break;
                }
            }
        }

        static void PrintMatrix()
        {
            Console.WriteLine("Game Fifteen\n");

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    Console.Write(((matrix[x, y] != 0) ? $"{matrix[x, y],3:d}" : "   "));
                }

                Console.WriteLine();
            }
        }

        static void HandleInput(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    Move(1, 0);
                    break;

                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    Move(-1, 0);
                    break;

                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    Move(0, 1);
                    break;

                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    Move(0, -1);
                    break;
            }
        }

        static void Move(int x, int y)
        {
            int nx = emptyX + x;
            int ny = emptyY + y;

            if (nx < 0 || nx > 3 || ny < 0 || ny > 3)
                return;

            matrix[emptyX, emptyY] = matrix[nx, ny];
            matrix[nx, ny] = 0;

            emptyX = nx;
            emptyY = ny;
        }

        static void InitHandleInput()
        {
            var key = Console.ReadKey(true).Key;

            HandleInput(key);
        }

        static bool Finish()
        {
            int v = 1;

            for (int x = 0; x < 4; x++)
                for (int y = 0; y < 4; y++)
                {
                    if (x == 3 && y == 3) return matrix[x, y] == 0;
                    if (matrix[x, y] != v++) return false;
                }

            return true;
        }
    }

}

