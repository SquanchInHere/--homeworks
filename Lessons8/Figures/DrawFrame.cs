namespace Figures
{
    public static class DrawFrame
    {
        private static void PrintChar(char ch, int count)
        {
            int i = 0;
            while (i < count)
            {
                Console.Write(ch);
                i++;
            }
        }
        
        public static bool Draw(char fig, int innerW, int innerH)
        {
            Console.Clear();
            if (!IsValidFigure(fig))
                return false;

            Console.Write(Params.ULC);
            PrintChar(Params.HB, innerW);
            Console.WriteLine(Params.URC);

            for (int y = 0; y < innerH; y++)
            {
                Console.Write(Params.VB);
                for (int x = 0; x < innerW; x++)
                {
                    Console.Write(IsFilled(fig, x, y, innerW, innerH) ? Params.bg : Params.space);
                }
                Console.WriteLine(Params.VB);
            }

            Console.Write(Params.DLC);
            PrintChar(Params.HB, innerW);
            Console.WriteLine(Params.DRC);

            return true;
        }

        private static bool IsValidFigure(char fig)
        {
            return fig == 'a' || fig == 'b' || fig == 'c' || fig == 'd' || fig == 'e';
        }

        private static bool IsFilled(char fig, int x, int y, int w, int h)
        {
            int xr;

            switch (fig)
            {
                case 'a':
                    return y * w <= x * h;

                case 'b':
                    return y * w >= x * h;

                case 'c':
                    xr = (w - 1 - x);
                    return (y * w <= x * h) && (y * w <= xr * h);

                case 'd':
                    xr = (w - 1 - x);
                    return (y * w >= x * h) && (y * w >= xr * h);

                case 'e':
                    xr = (w - 1 - x);
                    bool top = (y * w <= x * h) && (y * w <= xr * h);
                    bool bottom = (y * w >= x * h) && (y * w >= xr * h);
                    return top || bottom;

                default:
                    return false;
            }
        }
    }
}
