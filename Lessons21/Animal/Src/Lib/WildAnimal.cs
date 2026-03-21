namespace Animal.Src.Lib
{
    public class WildAnimal
    {
        public string Name { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string Location { get; set; }
        public int AveragePopulation { get; set; }

        public WildAnimal(string name, int minAge, int maxAge, string location, int averagePopulation)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Animal name cannot be empty.");

            if (minAge < 0 || maxAge < 0 || minAge > maxAge)
                throw new ArgumentException("Invalid age range.");

            if (string.IsNullOrWhiteSpace(location))
                throw new ArgumentException("Location cannot be empty.");

            if (averagePopulation < 0)
                throw new ArgumentException("Average population cannot be negative.");

            Name = name;
            MinAge = minAge;
            MaxAge = maxAge;
            Location = location;
            AveragePopulation = averagePopulation;
        }

        public void ChangeName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Animal name cannot be empty.");

            Name = newName;
        }

        public void ChangeLocation(string newLocation)
        {
            if (string.IsNullOrWhiteSpace(newLocation))
                throw new ArgumentException("Location cannot be empty.");

            Location = newLocation;
        }

        public void ChangePopulation(int newPopulation)
        {
            if (newPopulation < 0)
                throw new ArgumentException("Population cannot be negative.");

            AveragePopulation = newPopulation;
        }

        public virtual string GetPopulationSuccess()
        {
            if (AveragePopulation >= 10000)
                return "Population is very successful and stable.";

            if (AveragePopulation >= 3000)
                return "Population is stable.";

            if (AveragePopulation >= 1000)
                return "Population is vulnerable.";

            return "Population is critical and needs protection.";
        }

        public override string ToString()
        {
            return $"Type: WildAnimal | Name: {Name} | Age range: {MinAge}-{MaxAge} | Location: {Location} | Average population: {AveragePopulation}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not WildAnimal other)
                return false;

            return Name == other.Name &&
                   MinAge == other.MinAge &&
                   MaxAge == other.MaxAge &&
                   Location == other.Location &&
                   AveragePopulation == other.AveragePopulation;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, MinAge, MaxAge, Location, AveragePopulation);
        }
    }
}
