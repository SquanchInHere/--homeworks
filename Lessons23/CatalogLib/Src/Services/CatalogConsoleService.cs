using CatalogLib.Src.Lib.Abstract;
using CatalogLib.Src.Models;
using CatalogLib.Src.Helpers;

namespace CatalogLib.Src.Services
{
    public class CatalogConsoleService
    {
        private readonly CatalogService catalogService;

        public CatalogConsoleService(CatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        public void InitializeTestData()
        {
            catalogService.InitializeTestData();
            Console.WriteLine("Test data initialized.");
        }

        public void AddSpecificItem()
        {
            Console.WriteLine("Choose item type:");
            Console.WriteLine("1 - Book");
            Console.WriteLine("2 - Newspaper");
            Console.WriteLine("3 - Almanac");
            Console.Write("Your choice: ");
            string? typeChoice = Console.ReadLine();

            try
            {
                switch (typeChoice)
                {
                    case "1":
                        catalogService.AddItem(new Book(
                            InputHelper.ReadString("Title: "),
                            InputHelper.ReadString("Author: "),
                            InputHelper.ReadString("Genre: "),
                            InputHelper.ReadInt("Pages: "),
                            InputHelper.ReadString("Annotation: "),
                            InputHelper.ReadDate("Publish date (yyyy-mm-dd): ")
                        ));
                        Console.WriteLine("Book added.");
                        break;

                    case "2":
                        catalogService.AddItem(new Newspaper(
                            InputHelper.ReadString("Newspaper title: "),
                            InputHelper.ReadDate("Issue date (yyyy-mm-dd): "),
                            InputHelper.ReadStringList("Enter headlines separated by comma: ")
                        ));
                        Console.WriteLine("Newspaper added.");
                        break;

                    case "3":
                        string title = InputHelper.ReadString("Almanac title: ");
                        string annotation = InputHelper.ReadString("Annotation: ");
                        string focus = InputHelper.ReadString("Focus: ");
                        DateTime publishDate = InputHelper.ReadDate("Publish date (yyyy-mm-dd): ");

                        List<Book> works = new();
                        int count = InputHelper.ReadInt("How many books inside the almanac? ");

                        for (int i = 0; i < count; i++)
                        {
                            Console.WriteLine($"Book #{i + 1}");
                            works.Add(new Book(
                                InputHelper.ReadString("Title: "),
                                InputHelper.ReadString("Author: "),
                                InputHelper.ReadString("Genre: "),
                                InputHelper.ReadInt("Pages: "),
                                InputHelper.ReadString("Annotation: "),
                                InputHelper.ReadDate("Publish date (yyyy-mm-dd): ")
                            ));
                        }

                        catalogService.AddItem(new Almanac(title, annotation, focus, works, publishDate));
                        Console.WriteLine("Almanac added.");
                        break;

                    default:
                        Console.WriteLine("Invalid item type.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Input error: {ex.Message}");
            }
        }

        public void AddRandomItem()
        {
            catalogService.AddRandomItem();
            Console.WriteLine("Random item added.");
        }

        public void RemoveByTitle()
        {
            string title = InputHelper.ReadString("Enter title to remove: ");
            bool removed = catalogService.RemoveByTitle(title);

            Console.WriteLine(removed ? "Item removed." : "Item not found.");
        }

        public void ShowAll()
        {
            ShowSearchResults(catalogService.GetAll());
        }

        public void SearchByTitle()
        {
            string title = InputHelper.ReadString("Enter title: ");
            ShowSearchResults(catalogService.SearchByTitle(title));
        }

        public void SearchByAuthor()
        {
            string author = InputHelper.ReadString("Enter author: ");
            ShowSearchResults(catalogService.SearchByAuthor(author));
        }

        public void SearchBooksByKeywords()
        {
            string[] keywords = InputHelper
                .ReadStringList("Enter keywords separated by comma: ")
                .ToArray();

            List<Book> result = catalogService.SearchBooksByAnnotationKeywords(keywords);

            if (result.Count == 0)
            {
                Console.WriteLine("Nothing found.");
                return;
            }

            Console.WriteLine("=== Results ===");
            foreach (Book book in result)
            {
                Console.WriteLine(book);
            }
        }

        public void SearchBooksOrAlmanacs()
        {
            try
            {
                int maxPages = InputHelper.ReadInt("Enter maximum number of pages: ");
                string genre = InputHelper.ReadOptionalString("Enter genre (or leave empty): ");
                string author = InputHelper.ReadOptionalString("Enter author (or leave empty): ");

                ShowSearchResults(catalogService.SearchBooksOrAlmanacs(maxPages, genre, author));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Input error: {ex.Message}");
            }
        }

        private void ShowSearchResults(List<CatalogItem> items)
        {
            if (items.Count == 0)
            {
                Console.WriteLine("Nothing found.");
                return;
            }

            Console.WriteLine("=== Results ===");
            foreach (CatalogItem item in items)
            {
                Console.WriteLine(item);
            }
        }
    }
}
