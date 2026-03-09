using System;
using System.Collections.Generic;
using System.Text;

namespace Dictionary.src
{
    public static class PaginationHelper
    {
        public static void RunPages(int totalPages, Action<int> showPage)
        {
            if (totalPages <= 0)
            {
                Console.WriteLine("Nothing to display.");
                return;
            }

            int currentPage = 0;

            while (true)
            {
                Console.Clear();

                showPage(currentPage);

                Console.WriteLine();
                Console.WriteLine("N - next page");
                Console.WriteLine("P - previous page");
                Console.WriteLine("Q - exit");

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.N)
                {
                    if (currentPage < totalPages - 1)
                    {
                        currentPage++;
                    }
                }
                else if (key == ConsoleKey.P)
                {
                    if (currentPage > 0)
                    {
                        currentPage--;
                    }
                }
                else if (key == ConsoleKey.Q)
                {
                    Console.Clear();
                    break;
                }
            }
        }
    }
}
