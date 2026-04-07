using Zoo.Src.Abstractions;
using Zoo.Src.Enums;

namespace Zoo.Src.Models
{
    public class Crocodile : Animal
    {
        public bool NeedsWaterPool { get; set; }

        public Crocodile(string name, int age, double weight, GenderType gender, bool needsWaterPool)
            : base(name, age, weight, gender)
        {
            NeedsWaterPool = needsWaterPool;
        }

        public override string Species => "Crocodile";
        public override AnimalDietType DietType => AnimalDietType.Carnivore;
        public override string FoodType => "Fish";
        public override double DailyFoodAmountKg => 6.0;
        public override bool CanBreedInCaptivity => true;
        public override bool IsDangerous => true;
        public override int PopularityScore => 8;

        public void Swim()
        {
            Console.WriteLine($"{Name} swims in the water.");
        }

        public override string ToString()
        {
            return base.ToString() + $", Needs water pool: {(NeedsWaterPool ? "Yes" : "No")}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Crocodile other)
                return false;

            return base.Equals(other) &&
                   NeedsWaterPool == other.NeedsWaterPool;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), NeedsWaterPool);
        }
    }
}
