using Zoo.Src.Abstractions;

namespace Zoo.Src.Services
{
    public class VisitorService
    {
        private readonly Random _random = new Random();

        public int GenerateVisitors(List<Animal> animals)
        {
            int popularity = CalculatePopularity(animals);
            return _random.Next(20, 81) + popularity;
        }

        private int CalculatePopularity(List<Animal> animals)
        {
            return animals.Where(a => a.IsAlive).Sum(a => a.PopularityScore);
        }
    }
}
