using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048
{
    public static class Move
    {
        public static int Left(int[,] grid)
        {
            int mergeSum = 0;

            for (int r = 0; r < Params.Size; r++)
            {
                int[] temp = new int[Params.Size];
                int t = 0;
                for (int c = 0; c < Params.Size; c++)
                    if (grid[r, c] != 0) temp[t++] = grid[r, c];

                int[] merged = new int[Params.Size];
                int m = 0;
                int i = 0;

                while (i < Params.Size && temp[i] != 0)
                {
                    int cur = temp[i];
                    if (i + 1 < Params.Size && temp[i + 1] == cur)
                    {
                        int nv = cur * 2;
                        merged[m++] = nv;
                        mergeSum += nv;
                        i += 2;
                    }
                    else
                    {
                        merged[m++] = cur;
                        i++;
                    }
                }

                for (int c = 0; c < Params.Size; c++)
                    grid[r, c] = merged[c];
            }

            return mergeSum;
        }

        public static int Right(int[,] grid)
        {
            int mergeSum = 0;

            for (int r = 0; r < Params.Size; r++)
            {
                int[] temp = new int[Params.Size];
                int t = 0;
                for (int c = Params.Size - 1; c >= 0; c--)
                    if (grid[r, c] != 0) temp[t++] = grid[r, c];

                int[] merged = new int[Params.Size];
                int m = 0;
                int i = 0;

                while (i < Params.Size && temp[i] != 0)
                {
                    int cur = temp[i];
                    if (i + 1 < Params.Size && temp[i + 1] == cur)
                    {
                        int nv = cur * 2;
                        merged[m++] = nv;
                        mergeSum += nv;
                        i += 2;
                    }
                    else
                    {
                        merged[m++] = cur;
                        i++;
                    }
                }

                for (int c = 0; c < Params.Size; c++) grid[r, c] = 0;

                int write = Params.Size - 1;
                for (int k = 0; k < Params.Size && merged[k] != 0; k++)
                    grid[r, write--] = merged[k];
            }

            return mergeSum;
        }

        public static int Up(int[,] grid)
        {
            int mergeSum = 0;

            for (int c = 0; c < Params.Size; c++)
            {
                int[] temp = new int[Params.Size];
                int t = 0;
                for (int r = 0; r < Params.Size; r++)
                    if (grid[r, c] != 0) temp[t++] = grid[r, c];

                int[] merged = new int[Params.Size];
                int m = 0;
                int i = 0;

                while (i < Params.Size && temp[i] != 0)
                {
                    int cur = temp[i];
                    if (i + 1 < Params.Size && temp[i + 1] == cur)
                    {
                        int nv = cur * 2;
                        merged[m++] = nv;
                        mergeSum += nv;
                        i += 2;
                    }
                    else
                    {
                        merged[m++] = cur;
                        i++;
                    }
                }

                for (int r = 0; r < Params.Size; r++)
                    grid[r, c] = merged[r];
            }

            return mergeSum;
        }

        public static int Down(int[,] grid)
        {
            int mergeSum = 0;

            for (int c = 0; c < Params.Size; c++)
            {
                int[] temp = new int[Params.Size];
                int t = 0;
                for (int r = Params.Size - 1; r >= 0; r--)
                    if (grid[r, c] != 0) temp[t++] = grid[r, c];

                int[] merged = new int[Params.Size];
                int m = 0;
                int i = 0;

                while (i < Params.Size && temp[i] != 0)
                {
                    int cur = temp[i];
                    if (i + 1 < Params.Size && temp[i + 1] == cur)
                    {
                        int nv = cur * 2;
                        merged[m++] = nv;
                        mergeSum += nv;
                        i += 2;
                    }
                    else
                    {
                        merged[m++] = cur;
                        i++;
                    }
                }

                for (int r = 0; r < Params.Size; r++) grid[r, c] = 0;

                int write = Params.Size - 1;
                for (int k = 0; k < Params.Size && merged[k] != 0; k++)
                    grid[write--, c] = merged[k];
            }

            return mergeSum;
        }
    }
}
