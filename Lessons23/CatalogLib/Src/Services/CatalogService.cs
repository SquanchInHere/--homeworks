using CatalogLib.Src.Data;
using CatalogLib.Src.Factories;
using CatalogLib.Src.Lib.Abstract;
using CatalogLib.Src.Models;

namespace CatalogLib.Src.Services
{
    public class CatalogService
    {
        private readonly List<CatalogItem> items = new();
        private readonly RandomCatalogItemFactory randomFactory = new();

        public void InitializeTestData()
        {
            items.Clear();
            items.AddRange(CatalogSeedData.CreateTestItems());
        }

        public void AddItem(CatalogItem item)
        {
            items.Add(item);
        }

        public void AddRandomItem()
        {
            AddItem(randomFactory.CreateRandomItem());
        }

        public bool RemoveByTitle(string title)
        {
            CatalogItem? item = items.FirstOrDefault(i =>
                i.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (item == null)
                return false;

            items.Remove(item);
            return true;
        }

        public List<CatalogItem> GetAll()
        {
            return new List<CatalogItem>(items);
        }

        public List<CatalogItem> SearchByTitle(string title)
        {
            return items.Where(i => i.MatchesTitle(title)).ToList();
        }

        public List<CatalogItem> SearchByAuthor(string author)
        {
            return items.Where(i => i.MatchesAuthor(author)).ToList();
        }

        public List<Book> SearchBooksByAnnotationKeywords(string[] keywords)
        {
            return items
                .OfType<Book>()
                .Where(book => book.ContainsAllKeywords(keywords))
                .ToList();
        }

        public List<CatalogItem> SearchBooksOrAlmanacs(int maxPages, string? genre, string? author)
        {
            List<CatalogItem> result = new();

            foreach (CatalogItem item in items)
            {
                if (item is Book book)
                {
                    bool pagesMatch = book.Pages <= maxPages;
                    bool genreMatch = string.IsNullOrWhiteSpace(genre) ||
                                      book.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase);
                    bool authorMatch = string.IsNullOrWhiteSpace(author) ||
                                       book.Author.Contains(author, StringComparison.OrdinalIgnoreCase);

                    if (pagesMatch && genreMatch && authorMatch)
                        result.Add(book);
                }
                else if (item is Almanac almanac)
                {
                    bool pagesMatch = almanac.TotalPages <= maxPages;
                    bool genreMatch = string.IsNullOrWhiteSpace(genre) || almanac.MatchesGenre(genre);
                    bool authorMatch = string.IsNullOrWhiteSpace(author) || almanac.MatchesAuthorInside(author);

                    if (pagesMatch && genreMatch && authorMatch)
                        result.Add(almanac);
                }
            }

            return result;
        }
    }
}
