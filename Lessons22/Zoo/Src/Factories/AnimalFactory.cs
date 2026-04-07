using Zoo.Src.Abstractions;
using Zoo.Src.Enums;
using Zoo.Src.Models;

namespace Zoo.Src.Factories
{
    public static class AnimalFactory
    {
        public static Animal CreateTiger(string name, int age, double weight, GenderType gender, double speed)
        {
            return new Tiger(name, age, weight, gender, speed);
        }

        public static Animal CreateCrocodile(string name, int age, double weight, GenderType gender, bool needsWaterPool)
        {
            return new Crocodile(name, age, weight, gender, needsWaterPool);
        }

        public static Animal CreateKangaroo(string name, int age, double weight, GenderType gender, double jumpLength)
        {
            return new Kangaroo(name, age, weight, gender, jumpLength);
        }

        public static Animal? CreateBaby(string species, GenderType gender)
        {
            switch (species)
            {
                case "Tiger":
                    return new Tiger("Tiger Cub", 0, 12, gender, 20);

                case "Crocodile":
                    return new Crocodile("Baby Crocodile", 0, 8, gender, true);

                case "Kangaroo":
                    return new Kangaroo("Baby Kangaroo", 0, 6, gender, 2);

                default:
                    return null;
            }
        }
    }
}
