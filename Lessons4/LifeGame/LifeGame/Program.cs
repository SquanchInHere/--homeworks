using System.Text;

namespace LifeGame
{
    class Program
    {
        const int W = 5;
        const int H = 10;
        const int START_HP = 10;
        const int DMG_LONELY = 1;
        const int DMG_OVERPOP = 2;
        const int SPAWN_PROC = 20;
        const int START_CELLS = 4;

        static bool[,] alive = new bool[H, W];
        static int[,] hp = new int[H, W];

        static int playerX = 2;
        static int playerY = 5;

        static Random rnd = new Random();

        static int totalDead = 0;
        static int tickCount = 0;

        static bool gameOver = false;
        static bool win = false;

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;

            Init();

            while (!gameOver && !win)
            {
                Console.Clear();
                Help();
                PrintMatrix();
                PrintStats();
                HandleInput();
            }

            Console.Clear();
            Console.WriteLine(win ? $"Ви перемогли! пройшло циклів {tickCount}" : $"Ви загинули пройшло циклів {tickCount}");
            Console.ReadKey();
        }

        static void Init()
        {
            for (int i = 1; i <= START_CELLS; i++)
                SpawnRandom();

            alive[playerY, playerX] = true;
            hp[playerY, playerX] = START_HP;
        }

        static void SpawnRandom()
        {
            for (int i = 0; i < 100; i++)
            {
                int x = rnd.Next(W);
                int y = rnd.Next(H);

                if (!alive[y, x])
                {
                    alive[y, x] = true;
                    hp[y, x] = START_HP;
                    return;
                }
            }
        }

        static void HandleInput()
        {
            ConsoleKey key = Console.ReadKey(true).Key;

            int nx = playerX;
            int ny = playerY;

            switch (key)
            {
                case ConsoleKey.LeftArrow: nx--; break;
                case ConsoleKey.RightArrow: nx++; break;
                case ConsoleKey.UpArrow: ny--; break;
                case ConsoleKey.DownArrow: ny++; break;
                case ConsoleKey.Spacebar: break;
                default: return;
            }

            Tick(nx, ny);
        }

        static void Tick(int newPlayerX, int newPlayerY)
        {
            bool[,] nextAlive = new bool[H, W];
            int[,] nextHp = new int[H, W];

            tickCount++;

            for (int y = 0; y < H; y++)
                for (int x = 0; x < W; x++)
                {
                    if (!alive[y, x]) continue;

                    int newHp = ApplyDamage(x, y);

                    if (newHp <= 0)
                    {
                        totalDead++;
                        continue;
                    }

                    if (x == playerX && y == playerY)
                        MovePlayer(x, y, newPlayerX, newPlayerY, newHp, nextAlive, nextHp);
                    else
                        MoveNeighbors(CountNeighbors(x, y), x, y, newHp, nextAlive, nextHp);
                }

            alive = nextAlive;
            hp = nextHp;

            if (!alive[playerY, playerX])
                gameOver = true;

            if (CheckWin())
                win = true;

            if (rnd.Next(100) < SPAWN_PROC)
                SpawnRandom();
        }

        static int ApplyDamage(int x, int y)
        {
            int n = CountNeighbors(x, y);

            if (n == 0) return hp[y, x] - DMG_LONELY;
            if (n >= 3) return hp[y, x] - DMG_OVERPOP;

            return hp[y, x];
        }

        static void MoveNeighbors(int n, int x, int y, int newHp, bool[,] nextAlive, int[,] nextHp)
        {
            int tx = x;
            int ty = y;

            if (n != 2)
            {
                bool found = false;
                int bestScore = 0;

                for (int dy = -1; dy <= 1; dy++)
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        if (dx == 0 && dy == 0) continue;

                        int nx = x + dx;
                        int ny = y + dy;

                        if (!InBounds(nx, ny)) continue;
                        if (alive[ny, nx]) continue;

                        int score = CountNeighbors(nx, ny);

                        if (n >= 3)
                            score = -score;

                        if (!found || score > bestScore)
                        {
                            found = true;
                            bestScore = score;
                            tx = nx;
                            ty = ny;
                        }
                    }
            }

            if (!nextAlive[ty, tx])
            {
                nextAlive[ty, tx] = true;
                nextHp[ty, tx] = newHp;
            }
            else
            {
                nextAlive[y, x] = true;
                nextHp[y, x] = newHp;
            }
        }

        static void MovePlayer(int x, int y, int newPlayerX, int newPlayerY, int newHp, bool[,] nextAlive, int[,] nextHp)
        {
            int tx = x;
            int ty = y;

            if (InBounds(newPlayerX, newPlayerY) && !alive[newPlayerY, newPlayerX])
            {
                tx = newPlayerX;
                ty = newPlayerY;
                playerX = tx;
                playerY = ty;
            }

            nextAlive[ty, tx] = true;
            nextHp[ty, tx] = newHp;
        }


        static int CountNeighbors(int x, int y)
        {
            int count = 0;

            for (int dy = -1; dy <= 1; dy++)
                for (int dx = -1; dx <= 1; dx++)
                {
                    if (dx == 0 && dy == 0) continue;

                    int nx = x + dx;
                    int ny = y + dy;

                    if (InBounds(nx, ny) && alive[ny, nx])
                        count++;
                }

            return count;
        }

        static bool InBounds(int x, int y)
        {
            return x >= 0 && x < W && y >= 0 && y < H;
        }

        static int CountAlive()
        {
            int c = 0;
            for (int y = 0; y < H; y++)
                for (int x = 0; x < W; x++)
                    if (alive[y, x]) c++;
            return c;
        }

        static void PrintMatrix()
        {
            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    if (!alive[y, x])
                    {
                        Console.Write("\u2B1C");
                        continue;
                    }

                    bool isPlayer = (x == playerX && y == playerY);
                    Console.ForegroundColor = GetColorByHp(hp[y, x], isPlayer);
                    Console.Write("\u2B1B");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        static ConsoleColor GetColorByHp(int hpValue, bool isPlayer)
        {
            int half = START_HP / 2;
            int quarter = START_HP / 4;

            if (isPlayer)
                return GetPlayerColor(hpValue);

            if (hpValue <= quarter)
                return ConsoleColor.Red;

            if (hpValue <= half)
                return ConsoleColor.Yellow;

            return ConsoleColor.Blue;
        }

        static void PrintStats()
        {
            Console.WriteLine();
            Console.WriteLine("┌──────── СТАТИСТИКА ────────┐");
            Console.WriteLine($"│ Живі      : {CountAlive(),14} │");
            Console.WriteLine($"│ Загинуло  : {totalDead,14} │");
            Console.WriteLine($"│ Циклів    : {tickCount,14} │");
            Console.WriteLine("└────────────────────────────┘");
        }

        static void Help()
        {
            Console.WriteLine("Гра в життя");
            Console.WriteLine("Управління:");
            Console.WriteLine("\u2191 — вгору");
            Console.WriteLine("\u2193 — вниз");
            Console.WriteLine("\u2190 — вліво");
            Console.WriteLine("\u2192 — вправо");
            Console.WriteLine("Space — крок без руху");

            Console.Write("Це ви ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\u2B1B \n\n");
            Console.ResetColor();
        }

        static bool CheckWin()
        {
            if (CountAlive() != 2)
                return false;

            int neighbors = CountNeighbors(playerX, playerY);
            return neighbors == 1;
        }

        static ConsoleColor GetPlayerColor(int hp)
        {
            if (hp > 7) return ConsoleColor.Green;
            if (hp > 4) return ConsoleColor.Cyan;
            if (hp > 2) return ConsoleColor.Yellow;
            return ConsoleColor.Magenta;
        }
    }
}
