using CatalogLib.Src.Services;

namespace CatalogLib.Src
{
    public static class Menu
    {
        public static void Run(CatalogConsoleService consoleService)
        {
            while (true)
            {
                ShowMenu();
                Console.Write("Choose an option: ");
                string? choice = Console.ReadLine();

                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        consoleService.InitializeTestData();
                        break;
                    case "2":
                        consoleService.AddSpecificItem();
                        break;
                    case "3":
                        consoleService.AddRandomItem();
                        break;
                    case "4":
                        consoleService.RemoveByTitle();
                        break;
                    case "5":
                        consoleService.ShowAll();
                        break;
                    case "6":
                        consoleService.SearchByTitle();
                        break;
                    case "7":
                        consoleService.SearchByAuthor();
                        break;
                    case "8":
                        consoleService.SearchBooksByKeywords();
                        break;
                    case "9":
                        consoleService.SearchBooksOrAlmanacs();
                        break;
                    case "0":
                        Console.WriteLine("Program finished.");
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("=== LIBRARY CATALOG MENU ===");
            Console.WriteLine("1 - Initialize test data");
            Console.WriteLine("2 - Add a specific item");
            Console.WriteLine("3 - Add a random item");
            Console.WriteLine("4 - Remove item by title");
            Console.WriteLine("5 - Show full catalog");
            Console.WriteLine("6 - Search by title");
            Console.WriteLine("7 - Search by author");
            Console.WriteLine("8 - Search books by annotation keywords");
            Console.WriteLine("9 - Search book or almanac by pages and genre/author");
            Console.WriteLine("0 - Exit");
            Console.WriteLine();
        }
    }
}
