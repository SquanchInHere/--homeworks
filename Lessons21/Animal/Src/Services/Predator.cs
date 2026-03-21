using Animal.Src.Lib;

namespace Animal.Src.Services
{
    public class Predator : WildAnimal
    {
        public double PopulationChangeCoefficient { get; set; }

        public Predator(
            string name,
            int minAge,
            int maxAge,
            string location,
            int averagePopulation,
            double populationChangeCoefficient)
            : base(name, minAge, maxAge, location, averagePopulation)
        {
            PopulationChangeCoefficient = populationChangeCoefficient;
        }

        public void ChangePopulationCoefficient(double newCoefficient)
        {
            PopulationChangeCoefficient = newCoefficient;
        }

        public override string GetPopulationSuccess()
        {
            if (AveragePopulation < 1000 || PopulationChangeCoefficient < 0.9)
                return "Population is critical and needs protection.";

            if (PopulationChangeCoefficient >= 1.05 && AveragePopulation >= 3000)
                return "Population is growing successfully.";

            if (PopulationChangeCoefficient >= 1.0)
                return "Population is stable.";

            return "Population is declining.";
        }

        public override string ToString()
        {
            return $"Type: Predator | Name: {Name} | Age range: {MinAge}-{MaxAge} | Location: {Location} | Average population: {AveragePopulation} | Population change coefficient: {PopulationChangeCoefficient:F2}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Predator other)
                return false;

            return base.Equals(other) &&
                   PopulationChangeCoefficient == other.PopulationChangeCoefficient;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), PopulationChangeCoefficient);
        }
    }
}
