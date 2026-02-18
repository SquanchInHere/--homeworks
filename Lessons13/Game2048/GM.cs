namespace Game2048
{
    public static class GM
    {
        public static void Run()
        {
            int bestScore = 0;

            while (true)
            {
                int[,] grid = new int[Params.Size, Params.Size];
                int score = 0;
                bool reached2048 = false;

                SpawnTile(grid);
                SpawnTile(grid);

                while (true)
                {
                    Console.Clear();
                    Draw(grid, score, bestScore, reached2048);

                    if (!HasAnyMoves(grid))
                    {
                        Console.WriteLine("\nGAME OVER!");
                        bestScore = Math.Max(bestScore, score);
                        Console.WriteLine($"Score: {score} | Best: {bestScore}");
                        Console.WriteLine("N - New Game | Esc - exit");
                        var endKey = Console.ReadKey(true).Key;
                        if (endKey == ConsoleKey.N) break;
                        if (endKey == ConsoleKey.Escape) return;
                        continue;
                    }

                    var key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.Escape) return;

                    if (key == ConsoleKey.N)
                    {
                        bestScore = Math.Max(bestScore, score);
                        break;
                    }

                    int[,] before = (int[,])grid.Clone();

                    int gained = 0;
                    switch (key)
                    {
                        case ConsoleKey.LeftArrow:
                            gained = Move.Left(grid);
                            break;
                        case ConsoleKey.RightArrow:
                            gained = Move.Right(grid);
                            break;
                        case ConsoleKey.UpArrow:
                            gained = Move.Up(grid);
                            break;
                        case ConsoleKey.DownArrow:
                            gained = Move.Down(grid);
                            break;
                        default:
                            continue;
                    }

                    if (!Changed(before, grid))
                    {
                        continue;
                    }

                    score += gained;
                    if (score > bestScore) bestScore = score;

                    if (!reached2048 && HasTile(grid, 2048))
                        reached2048 = true;

                    SpawnTile(grid);
                }
            }
        }

        private static void Draw(int[,] g, int score, int best, bool reached2048)
        {
            Console.WriteLine("2048 (Console Game)");
            Console.WriteLine("Press Arrow for move | N: New Game | Esc: exit\n");
            Console.WriteLine($"Score: {score}   Best: {best}");
            if (reached2048) Console.WriteLine("2048 achieved! You can continue playing.\n");
            else Console.WriteLine();

            string line = "+------+------+------+------+";
            Console.WriteLine(line);
            for (int r = 0; r < Params.Size; r++)
            {
                Console.Write("|");
                for (int c = 0; c < Params.Size; c++)
                {
                    int v = g[r, c];
                    string s = (v == 0) ? "" : v.ToString();
                    Console.Write($"{s,6}|");
                }
                Console.WriteLine();
                Console.WriteLine(line);
            }
        }

        private static bool Changed(int[,] a, int[,] b)
        {
            for (int r = 0; r < Params.Size; r++)
                for (int c = 0; c < Params.Size; c++)
                    if (a[r, c] != b[r, c]) return true;
            return false;
        }

        private static bool HasTile(int[,] g, int target)
        {
            for (int r = 0; r < Params.Size; r++)
                for (int c = 0; c < Params.Size; c++)
                    if (g[r, c] == target) return true;
            return false;
        }

        private static bool SpawnTile(int[,] grid)
        {
            var zeros = new List<(int r, int c)>();
            for (int r = 0; r < Params.Size; r++)
                for (int c = 0; c < Params.Size; c++)
                    if (grid[r, c] == 0) zeros.Add((r, c));

            if (zeros.Count == 0) return false;

            var (zr, zc) = zeros[Params.rnd.Next(zeros.Count)];
            grid[zr, zc] = (Params.rnd.Next(10) == 0) ? 4 : 2;
            return true;
        }

        private static bool HasAnyMoves(int[,] g)
        {
            for (int r = 0; r < Params.Size; r++)
                for (int c = 0; c < Params.Size; c++)
                    if (g[r, c] == 0) return true;

            for (int r = 0; r < Params.Size; r++)
            {
                for (int c = 0; c < Params.Size; c++)
                {
                    int v = g[r, c];
                    if (r + 1 < Params.Size && g[r + 1, c] == v) return true;
                    if (c + 1 < Params.Size && g[r, c + 1] == v) return true;
                }
            }
            return false;
        }
    }
}
