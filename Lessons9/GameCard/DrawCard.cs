using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCard
{
    public static class DrawCard
    {
        public static void run()
        {
            Console.OutputEncoding = Encoding.UTF8;

            foreach (char suit in Params.suits)
            {
                for (int i = 0; i < Params.ranks.Length; i++)
                {
                    PrintCard(Params.ranks[i], suit);
                }
            }
        }

        private static void PrintCard(string rank, char suit)
        {
            string left = rank + suit;
            string right = suit + rank;

            Console.Write(Params.ULC); Repeat(Params.HB, Params.size); Console.WriteLine(Params.URC);

            Console.Write(Params.VB); PrintLeft(left, Params.size); Console.WriteLine(Params.VB);
            Console.Write(Params.VB); Repeat(Params.space, Params.size); Console.WriteLine(Params.VB);
            Console.Write(Params.VB); PrintCenter(suit, Params.size); Console.WriteLine(Params.VB);
            Console.Write(Params.VB); Repeat(Params.space, Params.size); Console.WriteLine(Params.VB);
            Console.Write(Params.VB); PrintRight(right, Params.size); Console.WriteLine(Params.VB);

            Console.Write(Params.DLC); Repeat(Params.HB, Params.size); Console.WriteLine(Params.DRC);
        }

        private static void Repeat(char ch, int count)
        {
            int i = 0;
            while (i < count)
            {
                Console.Write(ch);
                i++;
            }
        }

        private static void PrintLeft(string text, int width)
        {
            Console.Write(text);
            int spaces = width - text.Length;
            if (spaces < 0) spaces = 0;
            Repeat(Params.space, spaces);
        }

        private static void PrintRight(string text, int width)
        {
            int spaces = width - text.Length;
            if (spaces < 0) spaces = 0;
            Repeat(Params.space, spaces);
            Console.Write(text);
        }

        private static void PrintCenter(char suit, int width)
        {
            int left = (width - 1) / 2;
            int right = width - 1 - left;
            Repeat(Params.space, left);
            Console.Write(suit);
            Repeat(Params.space, right);
        }
    }
}
