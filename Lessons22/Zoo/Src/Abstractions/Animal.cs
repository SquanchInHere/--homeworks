using Zoo.Src.Enums;
using Zoo.Src.Interfaces;

namespace Zoo.Src.Abstractions
{
    public abstract class Animal : IFeedable, IReproducible, IDangerous, IVisitorAttraction
    {
        protected static readonly Random Random = new Random();

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Weight { get; set; }
        public GenderType Gender { get; set; }
        public bool IsAlive { get; protected set; }
        public int DaysInZoo { get; protected set; }

        public abstract string Species { get; }
        public abstract AnimalDietType DietType { get; }
        public abstract string FoodType { get; }
        public abstract double DailyFoodAmountKg { get; }
        public abstract bool CanBreedInCaptivity { get; }
        public abstract bool IsDangerous { get; }
        public abstract int PopularityScore { get; }

        protected Animal(string name, int age, double weight, GenderType gender)
        {
            Id = Guid.NewGuid();
            Name = name;
            Age = age;
            Weight = weight;
            Gender = gender;
            IsAlive = true;
            DaysInZoo = 0;
        }

        public virtual void Eat()
        {
            Console.WriteLine($"{Species} {Name} eats {FoodType}.");
        }

        public virtual bool TryReproduce()
        {
            if (!CanBreedInCaptivity || !IsAlive)
                return false;

            return GenerateChance(7);
        }

        public virtual bool TryDie()
        {
            if (!IsAlive)
                return false;

            int deathChance = GetDeathChance();

            if (GenerateChance(deathChance))
            {
                MarkAsDead();
                return true;
            }

            return false;
        }

        public virtual void LiveOneDay()
        {
            if (!IsAlive)
                return;

            DaysInZoo++;
            IncreaseAgeIfNeeded();
        }

        protected bool GenerateChance(int percent)
        {
            return Random.Next(1, 101) <= percent;
        }

        protected virtual int GetDeathChance()
        {
            return Age >= 15 ? 12 : 3;
        }

        protected void MarkAsDead()
        {
            IsAlive = false;
        }

        protected void IncreaseAgeIfNeeded()
        {
            if (DaysInZoo % 365 == 0)
                Age++;
        }

        public override string ToString()
        {
            return $"[{Species}] Name: {Name}, Age: {Age}, Weight: {Weight} kg, Gender: {Gender}, " +
                   $"Diet: {DietType}, Food: {FoodType} ({DailyFoodAmountKg} kg/day), " +
                   $"Dangerous: {(IsDangerous ? "Yes" : "No")}, " +
                   $"Breeds in captivity: {(CanBreedInCaptivity ? "Yes" : "No")}, " +
                   $"Alive: {(IsAlive ? "Yes" : "No")}, Popularity: {PopularityScore}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Animal other)
                return false;

            return Id == other.Id &&
                   Name == other.Name &&
                   Species == other.Species;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Species);
        }
    }
}
