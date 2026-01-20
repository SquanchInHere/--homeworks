namespace Figures
{
    internal class Menu
    {
        public static void Print()
        {
            while (true)
            {
                Console.WriteLine("Choose one of the shapes: a, b, c, d, e (q - exit)");
                Console.Write("Choice: ");

                string s = Console.ReadLine();
                if (s == null) continue;

                s = s.Trim().ToLower();
                if (s == "q") return;
                if (s.Length == 0) continue;

                char fig = s[0];

                int w = Params.WIDTH;
                int h = Params.HEIGHT;

                Console.WriteLine();

                if (!DrawFrame.Draw(fig, w, h))
                    Console.WriteLine("Invalid shape. Available: a, b, c, d, e");

                Console.WriteLine();
            }
        }
    }
}
