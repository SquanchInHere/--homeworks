using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessMyNumber
{
    public static class Input
    {
        public static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine();

                int v;
                if (s != null && int.TryParse(s.Trim(), out v))
                    return v;

                Console.WriteLine("Please enter an integer number.");
            }
        }

        public static bool YesNo(string prompt)
        {
            Console.Write(prompt);
            string s = Console.ReadLine();
            if (s == null) return false;

            s = s.Trim().ToLower();
            return s == "y" || s == "yes" || s == "1";
        }
    }
}
