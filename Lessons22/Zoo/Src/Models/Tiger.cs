using Zoo.Src.Abstractions;
using Zoo.Src.Enums;

namespace Zoo.Src.Models
{
    public class Tiger : Animal
    {
        public double RunningSpeed { get; set; }

        public Tiger(string name, int age, double weight, GenderType gender, double runningSpeed)
            : base(name, age, weight, gender)
        {
            RunningSpeed = runningSpeed;
        }

        public override string Species => "Tiger";
        public override AnimalDietType DietType => AnimalDietType.Carnivore;
        public override string FoodType => "Meat";
        public override double DailyFoodAmountKg => 8.0;
        public override bool CanBreedInCaptivity => true;
        public override bool IsDangerous => true;
        public override int PopularityScore => 10;

        public void Hunt()
        {
            Console.WriteLine($"{Name} hunts at {RunningSpeed} km/h.");
        }

        public override string ToString()
        {
            return base.ToString() + $", Running speed: {RunningSpeed} km/h";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Tiger other)
                return false;

            return base.Equals(other) &&
                   RunningSpeed == other.RunningSpeed;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), RunningSpeed);
        }
    }
}
