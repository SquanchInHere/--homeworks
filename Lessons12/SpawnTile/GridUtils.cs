using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpawnTile
{
    public static class GridUtils
    {
        static readonly Random rng = new Random();
        public static void Run(int[,] grid)
        {
            Print(SpawnTile(grid));
        }
        private static int[,] SpawnTile(int[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);

            var zeros = new List<(int r, int c)>();
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (grid[r, c] == 0)
                        zeros.Add((r, c));
                }
            }

            (int zr, int zc) = zeros[rng.Next(zeros.Count)];

            int roll = rng.Next(100);
            grid[zr, zc] = (roll < 90) ? 2 : 4;

            return grid;
        }

        private static void Print(int[,] g)
        {
            for (int r = 0; r < g.GetLength(0); r++)
            {
                for (int c = 0; c < g.GetLength(1); c++)
                    Console.Write($"{g[r, c],4}");
                Console.WriteLine();
            }
        }
    }
}
