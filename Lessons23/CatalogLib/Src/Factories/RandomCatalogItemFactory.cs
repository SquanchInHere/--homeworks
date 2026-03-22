using CatalogLib.Src.Lib.Abstract;
using CatalogLib.Src.Models;

namespace CatalogLib.Src.Factories
{
    public class RandomCatalogItemFactory
    {
        private readonly Random random = new Random();

        public CatalogItem CreateRandomItem()
        {
            int type = random.Next(3);

            switch (type)
            {
                case 0:
                    return new Book(
                        "White Fang",
                        "Jack London",
                        "Adventure",
                        220,
                        "A story about wilderness, survival, and the life of a wolf-dog.",
                        new DateTime(1906, 1, 1));

                case 1:
                    return new Newspaper(
                        "Morning Times",
                        DateTime.Today,
                        new List<string>
                        {
                            "Global news update",
                            "Technology market trends",
                            "Weather forecast"
                        });

                default:
                    Book b1 = new Book(
                        "Tom Sawyer",
                        "Mark Twain",
                        "Adventure",
                        180,
                        "A novel about youth, friendship, and adventures.",
                        new DateTime(1876, 1, 1));

                    Book b2 = new Book(
                        "Treasure Island",
                        "Robert Louis Stevenson",
                        "Adventure",
                        240,
                        "A classic adventure novel about treasure hunting and pirates.",
                        new DateTime(1883, 1, 1));

                    return new Almanac(
                        "Classic Adventure Almanac",
                        "A selection of classic adventure works.",
                        "Classic fiction",
                        new List<Book> { b1, b2 },
                        new DateTime(2022, 9, 15));
            }
        }
    }
}
