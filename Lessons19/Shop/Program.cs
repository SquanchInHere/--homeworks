using Shop.Src.Enums;
using Shop.Src.Models;

Person author1 = new Person("Jon", "Skeet", new DateTime(1974, 3, 1));
Person author2 = new Person("Joseph", "Albahari", new DateTime(1963, 12, 29));
Person author3 = new Person("Robert C.", "Martin", new DateTime(1952, 12, 5));

Article article1 = new Article(author1, "C# in Depth", 5);
Article article2 = new Article(author2, "C# 12 in a Nutshell", 5);
Article article3 = new Article(author3, "A Craftsman's Guide to Software Structure and Design", 4.7);

Magazine magazine = new Magazine(
    "A Craftsman's Guide to Software Structure and Design",
    Frequency.Monthly,
    new DateTime(2017, 5, 12),
    20000
);

magazine.AddArticles(article1, article2, article3);

Console.WriteLine(magazine);
