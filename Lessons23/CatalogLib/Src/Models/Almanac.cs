using CatalogLib.Src.Lib.Abstract;

namespace CatalogLib.Src.Models
{
    public class Almanac : CatalogItem
    {
        public string Annotation { get; set; }
        public string Focus { get; set; }
        public List<Book> Works { get; set; }

        public Almanac(string title, string annotation, string focus, List<Book> works, DateTime publishDate)
            : base(title, publishDate)
        {
            if (string.IsNullOrWhiteSpace(annotation))
                throw new ArgumentException("Annotation cannot be empty.");

            if (string.IsNullOrWhiteSpace(focus))
                throw new ArgumentException("Focus cannot be empty.");

            if (works == null || works.Count == 0)
                throw new ArgumentException("Works list cannot be empty.");

            Annotation = annotation;
            Focus = focus;
            Works = works;
        }

        public int TotalPages => Works.Sum(book => book.Pages);

        public override string GetItemType()
        {
            return "Almanac";
        }

        public override bool MatchesAuthor(string author)
        {
            return Works.Any(book => book.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
        }

        public bool MatchesGenre(string genre)
        {
            return Works.Any(book => book.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase));
        }

        public bool MatchesAuthorInside(string author)
        {
            return Works.Any(book => book.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
        }

        public override string ToString()
        {
            string worksInfo = string.Join(" | ", Works.Select(book => $"{book.Title} ({book.Author})"));

            return $"Type: Almanac | Title: {Title} | Focus: {Focus} | Publish date: {PublishDate:yyyy-MM-dd} | Total pages: {TotalPages} | Annotation: {Annotation} | Works: {worksInfo}";
        }
    }
}
