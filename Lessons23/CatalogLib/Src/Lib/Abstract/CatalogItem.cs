namespace CatalogLib.Src.Lib.Abstract
{
    public abstract class CatalogItem
    {
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }

        protected CatalogItem(string title, DateTime publishDate)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");

            Title = title;
            PublishDate = publishDate;
        }

        public abstract string GetItemType();

        public virtual bool MatchesTitle(string title)
        {
            return Title.Contains(title, StringComparison.OrdinalIgnoreCase);
        }

        public virtual bool MatchesAuthor(string author)
        {
            return false;
        }

        public abstract override string ToString();
    }
}
