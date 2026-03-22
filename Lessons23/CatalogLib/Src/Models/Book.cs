using CatalogLib.Src.Lib.Abstract;

namespace CatalogLib.Src.Models
{
    public class Book : CatalogItem
    {
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Pages { get; set; }
        public string Annotation { get; set; }

        public Book(string title, string author, string genre, int pages, string annotation, DateTime publishDate)
            :base (title, publishDate) 
        {
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author cannot be empty.");

            if (string.IsNullOrWhiteSpace(genre))
                throw new ArgumentException("Genre cannot be empty.");

            if (pages <= 0)
                throw new ArgumentException("Pages must be greater than zero.");

            if (string.IsNullOrWhiteSpace(annotation))
                throw new ArgumentException("Annotation cannot be empty.");

            Author = author;
            Genre = genre;
            Pages = pages;
            Annotation = annotation;
        }

        public override string GetItemType()
        {
            return "Book";
        }

        public override bool MatchesAuthor(string author)
        {
            return Author.Contains(author, StringComparison.OrdinalIgnoreCase);
        }

        public bool ContainsAllKeywords(string[] keywords)
        {
            foreach (string keyword in keywords)
            {
                if (!Annotation.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            return true;
        }

        public override string ToString()
        {
            return $"Type: Book | Title: {Title} | Author: {Author} | Genre: {Genre} | Pages: {Pages} | Publish date: {PublishDate:yyyy-MM-dd} | Annotation: {Annotation}";
        }
    }
}
