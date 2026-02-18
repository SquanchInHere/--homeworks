using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G2048
{
    public static class Move
    {
        public static void Run(int[,] grid, MoveDir dir)
        {
            int sum = 0;
            string position = "left";
            bool isLeft = dir == MoveDir.Left;
            Console.WriteLine($"Move {position}!");
            Console.WriteLine("There was:");
            PrintGrid(grid);

            switch (dir)
            {
                case MoveDir.Left:
                    sum = MoveLeft(grid);
                    break;
                case MoveDir.Right:
                    sum = MoveRight(grid);
                    position = "right";
                    break;
                case MoveDir.Up:
                    sum = MoveUp(grid);
                    position = "up";
                    break;
                case MoveDir.Down:
                    sum = MoveDown(grid);
                    position = "down";
                    break;
            }

            Console.WriteLine($"\nIt became (after shifting to the {position}):");
            PrintGrid(grid);

            Console.WriteLine($"\nSum of joins: {sum}");
        }

        //Move to the left
        private static int MoveLeft(int[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            int mergeSum = 0;

            for (int r = 0; r < rows; r++)
            {
                int[] temp = new int[cols];
                int t = 0;
                for (int c = 0; c < cols; c++)
                {
                    int v = grid[r, c];
                    if (v != 0) temp[t++] = v;
                }

                int[] merged = new int[cols];
                int m = 0;
                int i = 0;
                while (i < cols && temp[i] != 0)
                {
                    int cur = temp[i];

                    if (i + 1 < cols && temp[i + 1] == cur)
                    {
                        int newVal = cur * 2;
                        merged[m++] = newVal;
                        mergeSum += newVal;
                        i += 2;
                    }
                    else
                    {
                        merged[m++] = cur;
                        i += 1;
                    }
                }

                for (int c = 0; c < cols; c++)
                    grid[r, c] = merged[c];
            }

            return mergeSum;
        }

        //Move to the right
        private static int MoveRight(int[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            int mergeSum = 0;

            for (int r = 0; r < rows; r++)
            {
                int[] temp = new int[cols];
                int t = 0;
                for (int c = cols - 1; c >= 0; c--)
                {
                    int v = grid[r, c];
                    if (v != 0) temp[t++] = v;
                }

                int[] merged = new int[cols];
                int m = 0;
                int i = 0;

                while (i < cols && temp[i] != 0)
                {
                    int cur = temp[i];

                    if (i + 1 < cols && temp[i + 1] == cur)
                    {
                        int newVal = cur * 2;
                        merged[m++] = newVal;
                        mergeSum += newVal;
                        i += 2;
                    }
                    else
                    {
                        merged[m++] = cur;
                        i += 1;
                    }
                }

                for (int c = 0; c < cols; c++)
                    grid[r, c] = 0;

                int writeCol = cols - 1;
                for (int k = 0; k < cols; k++)
                {
                    if (merged[k] == 0) break;
                    grid[r, writeCol--] = merged[k];
                }
            }

            return mergeSum;
        }

        //Move to the Up
        public static int MoveUp(int[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            int mergeSum = 0;

            for (int c = 0; c < cols; c++)
            {
                int[] temp = new int[rows];
                int t = 0;
                for (int r = 0; r < rows; r++)
                {
                    int v = grid[r, c];
                    if (v != 0) temp[t++] = v;
                }

                int[] merged = new int[rows];
                int m = 0;
                int i = 0;
                while (i < rows && temp[i] != 0)
                {
                    int cur = temp[i];

                    if (i + 1 < rows && temp[i + 1] == cur)
                    {
                        int newVal = cur * 2;
                        merged[m++] = newVal;
                        mergeSum += newVal;
                        i += 2;
                    }
                    else
                    {
                        merged[m++] = cur;
                        i += 1;
                    }
                }

                for (int r = 0; r < rows; r++)
                    grid[r, c] = merged[r];
            }

            return mergeSum;
        }

        //Move to the down
        public static int MoveDown(int[,] grid)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            int mergeSum = 0;

            for (int c = 0; c < cols; c++)
            {
                int[] temp = new int[rows];
                int t = 0;
                for (int r = rows - 1; r >= 0; r--)
                {
                    int v = grid[r, c];
                    if (v != 0) temp[t++] = v;
                }

                int[] merged = new int[rows];
                int m = 0;
                int i = 0;
                while (i < rows && temp[i] != 0)
                {
                    int cur = temp[i];

                    if (i + 1 < rows && temp[i + 1] == cur)
                    {
                        int newVal = cur * 2;
                        merged[m++] = newVal;
                        mergeSum += newVal;
                        i += 2;
                    }
                    else
                    {
                        merged[m++] = cur;
                        i += 1;
                    }
                }

                for (int r = 0; r < rows; r++)
                    grid[r, c] = 0;

                int writeRow = rows - 1;
                for (int k = 0; k < rows && merged[k] != 0; k++)
                    grid[writeRow--, c] = merged[k];
            }

            return mergeSum;
        }

        private static void PrintGrid(int[,] g)
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
