namespace Spiral
{
    public static class Spiral
    {

        public static void Run(int[,] m)
        {             
            Helper.SortMatrix(m);
            AnimateSpiralDraw(m, Params.delayMs);
        }
        private static void AnimateSpiralDraw(int[,] m, int delayMs)
        {
            int n = m.GetLength(0);

            (int r, int c)[] path = BuildSpiralPath(n);

            Console.CursorVisible = false;

            int ox = 0;
            int oy = Console.CursorTop;

            Console.Clear();
            Helper.DrawEmptyGrid(n, ox, oy, Params.cellW);

            for (int i = 0; i < path.Length; i++)
            {
                int r = path[i].r;
                int c = path[i].c;

                Helper.PrintMatrix(m, ox, oy, Params.cellW, r, c);

                if (delayMs > 0) Thread.Sleep(delayMs);
            }

            Console.SetCursorPosition(0, oy + n);
            Console.CursorVisible = true;
        }

        private static (int r, int c)[] BuildSpiralPath(int n)
        {
            var path = new (int r, int c)[n * n];
            int k = 0;

            int top = 0, bottom = n - 1, left = 0, right = n - 1;

            while (top <= bottom && left <= right)
            {
                for (int c = left; c <= right; c++) path[k++] = (top, c);
                top++;

                for (int r = top; r <= bottom; r++) path[k++] = (r, right);
                right--;

                if (top <= bottom)
                {
                    for (int c = right; c >= left; c--) path[k++] = (bottom, c);
                    bottom--;
                }

                if (left <= right)
                {
                    for (int r = bottom; r >= top; r--) path[k++] = (r, left);
                    left++;
                }
            }

            return path;
        }
    }
}
