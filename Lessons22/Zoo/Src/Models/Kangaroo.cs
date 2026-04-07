using System;
using Zoo.Src.Abstractions;
using Zoo.Src.Enums;

namespace Zoo.Src.Models
{
    public class Kangaroo : Animal
    {
        public double JumpLength { get; set; }

        public Kangaroo(string name, int age, double weight, GenderType gender, double jumpLength)
            : base(name, age, weight, gender)
        {
            JumpLength = jumpLength;
        }

        public override string Species => "Kangaroo";
        public override AnimalDietType DietType => AnimalDietType.Herbivore;
        public override string FoodType => "Grass";
        public override double DailyFoodAmountKg => 4.0;
        public override bool CanBreedInCaptivity => true;
        public override bool IsDangerous => false;
        public override int PopularityScore => 7;

        public void Jump()
        {
            Console.WriteLine($"{Name} jumps {JumpLength} meters.");
        }

        public override string ToString()
        {
            return base.ToString() + $", Jump length: {JumpLength} m";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Kangaroo other)
                return false;

            return base.Equals(other) &&
                   JumpLength == other.JumpLength;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), JumpLength);
        }
    }
}
