using Shop.Src.Enums;

namespace Shop.Src.Models
{
    public class Magazine
    {
        private string name;
        private Frequency frequency;
        private DateTime releaseDate;
        private int circulation;
        private List<Article> articles;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public Frequency Frequency
        {
            get => frequency;
            set => frequency = value;
        }

        public DateTime ReleaseDate
        {
            get => releaseDate;
            set => releaseDate = value;
        }

        public int Circulation
        {
            get => circulation;
            set => circulation = value;
        }

        public List<Article> Articles
        {
            get => articles;
            set => articles = value ?? new List<Article>();
        }

        public double AverageRating
        {
            get
            {
                if (articles == null || articles.Count == 0)
                    return 0.0;

                return articles.Average(article => article.Rating);
            }
        }

        public bool this[Frequency frequencyIndex]
        {
            get => frequency == frequencyIndex;
        }

        public Magazine()
        {
            name = "Default Magazine";
            frequency = Frequency.Monthly;
            releaseDate = DateTime.Now;
            circulation = 1000;
            articles = new List<Article>();
        }

        public Magazine(string name, Frequency frequency, DateTime releaseDate, int circulation)
        {
            this.name = name;
            this.frequency = frequency;
            this.releaseDate = releaseDate;
            this.circulation = circulation;
            this.articles = new List<Article>();
        }

        public void AddArticles(params Article[] newArticles)
        {
            if (newArticles == null || newArticles.Length == 0)
                return;

            articles.AddRange(newArticles);
        }

        public override string ToString()
        {
            string articlesInfo = articles.Count == 0
                ? "No articles"
                : string.Join("\n", articles);

            return $"Magazine name: {name}\n" +
                   $"Frequency: {frequency}\n" +
                   $"Release date: {releaseDate.ToShortDateString()}\n" +
                   $"Circulation: {circulation}\n" +
                   $"Average rating: {AverageRating:F2}\n" +
                   $"Articles:\n{articlesInfo}";
        }
    }
}
