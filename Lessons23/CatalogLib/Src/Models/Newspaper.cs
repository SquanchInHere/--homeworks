using CatalogLib.Src.Lib.Abstract;

namespace CatalogLib.Src.Models
{
    public class Newspaper : CatalogItem
    {
        public List<string> Headlines { get; set; }

        public Newspaper(string title, DateTime publishDate, List<string> headlines)
            : base(title, publishDate)
        {
            if (headlines == null || headlines.Count == 0)
                throw new ArgumentException("Headlines cannot be empty.");

            Headlines = headlines;
        }

        public override string GetItemType()
        {
            return "Newspaper";
        }

        public override string ToString()
        {
            return $"Type: Newspaper | Title: {Title} | Issue date: {PublishDate:yyyy-MM-dd} | Headlines: {string.Join("; ", Headlines)}";
        }
    }
}
