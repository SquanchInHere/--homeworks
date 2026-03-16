namespace Shop.Src.Models
{
    public class Article
    {
        public Person Author { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }

        public Article()
        {
            Author = new Person();
            Title = "Untitled article";
            Rating = 0.0;
        }

        public Article(Person author, string title, double rating)
        {
            Author = author;
            Title = title;
            Rating = rating;
        }

        public override string ToString()
        {
            return $"Title: {Title}, Rating: {Rating}, Author: [{Author}]";
        }
    }
}
