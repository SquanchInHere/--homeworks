namespace Spiral
{
    public static class Helper
    {
        public static int[,] RandomMatrix(int row, int col, int min, int max)
        {
            Random rng = new Random();
            int[,] m = new int[row, col];

            for (int r = 0; r < row; r++)
                for (int c = 0; c < col; c++)
                    m[r, c] = rng.Next(min, max + 1);

            return m;
        }

        public static void SortMatrix(int[,] m)
        {
            int n = m.GetLength(0), k = 0;
            int[] arr = new int[n * n];

            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    arr[k++] = m[r, c];

            Array.Sort(arr);

            int top = 0, bottom = n - 1, left = 0, right = n - 1;
            k = 0;

            while (top <= bottom && left <= right)
            {
                for (int c = left; c <= right; c++) m[top, c] = arr[k++]; top++;
                for (int r = top; r <= bottom; r++) m[r, right] = arr[k++]; right--;

                if (top <= bottom)
                {
                    for (int c = right; c >= left; c--) m[bottom, c] = arr[k++]; bottom--;
                }

                if (left <= right)
                {
                    for (int r = bottom; r >= top; r--) m[r, left] = arr[k++]; left++;
                }
            }
        }

        public static void PrintMatrix(int[,] a, int ox, int oy, int cellW, int r, int c)
        {
            int x = ox + c * cellW;
            int y = oy + r;
            Console.SetCursorPosition(x, y);
            Console.Write(a[r, c].ToString().PadLeft(cellW));
        }

        public static void DrawEmptyGrid(int n, int ox, int oy, int cellW)
        {
            for (int r = 0; r < n; r++)
            {
                Console.SetCursorPosition(ox, oy + r);
                for (int c = 0; c < n; c++)
                    Console.Write("_".PadLeft(cellW));
            }
        }
    }
}
