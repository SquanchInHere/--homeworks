using CatalogLib.Src.Lib.Abstract;
using CatalogLib.Src.Models;

namespace CatalogLib.Src.Data
{
    public static class CatalogSeedData
    {
        public static List<CatalogItem> CreateTestItems()
        {
            Book book1 = new Book(
                "1984",
                "George Orwell",
                "Dystopian",
                328,
                "A dystopian novel about total surveillance, control, and freedom.",
                new DateTime(1949, 6, 8));

            Book book2 = new Book(
                "The Hound of the Baskervilles",
                "Arthur Conan Doyle",
                "Detective",
                256,
                "A detective story about Sherlock Holmes, mystery, and investigation.",
                new DateTime(1902, 4, 1));

            Book book3 = new Book(
                "Twenty Thousand Leagues Under the Sea",
                "Jules Verne",
                "Adventure",
                300,
                "An adventure novel about travel, the sea, and scientific discoveries.",
                new DateTime(1870, 3, 20));

            Newspaper newspaper = new Newspaper(
                "Daily News",
                new DateTime(2025, 3, 1),
                new List<string>
                {
                    "Elections and political debates",
                    "Economic growth this quarter",
                    "Sports championship results"
                });

            Almanac almanac = new Almanac(
                "World Adventure Collection",
                "A collection of adventure stories by different authors.",
                "Adventure literature",
                new List<Book> { book2, book3 },
                new DateTime(2020, 5, 10));

            return new List<CatalogItem>
            {
                book1,
                book2,
                book3,
                newspaper,
                almanac
            };
        }
    }
}
