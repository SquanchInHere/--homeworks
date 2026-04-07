using Zoo.Src.Enums;
using Zoo.Src.Factories;
using Zoo.Src.Models;
using Zoo.Src.Services;

namespace Zoo.Src.Data
{
    public static class DataSeed
    {
        public static ZooService CreateZoo()
        {
            ZooService zoo = new ZooService("Mega Zoo", 20000m, 150m);

            SeedEnclosures(zoo);
            SeedFood(zoo);
            SeedAnimalPrices(zoo);
            SeedAnimals(zoo);

            return zoo;
        }

        private static void SeedEnclosures(ZooService zoo)
        {
            zoo.AddEnclosure(new Enclosure("Herbivore Enclosure", EnclosureType.Herbivores, 15));
            zoo.AddEnclosure(new Enclosure("Predator Enclosure", EnclosureType.Predators, 10));
            zoo.AddEnclosure(new Enclosure("Dangerous Enclosure", EnclosureType.Dangerous, 10));
            zoo.AddEnclosure(new Enclosure("Reptile Enclosure", EnclosureType.Reptiles, 10));
            zoo.AddEnclosure(new Enclosure("Safe Enclosure", EnclosureType.Safe, 15));
        }

        private static void SeedFood(ZooService zoo)
        {
            zoo.AddFoodStorage(new FoodStorage("Meat", 400));
            zoo.AddFoodStorage(new FoodStorage("Fish", 300));
            zoo.AddFoodStorage(new FoodStorage("Grass", 500));
        }

        private static void SeedAnimalPrices(ZooService zoo)
        {
            zoo.AddAnimalPrice("Tiger", 5000m);
            zoo.AddAnimalPrice("Crocodile", 3500m);
            zoo.AddAnimalPrice("Kangaroo", 2500m);
        }

        private static void SeedAnimals(ZooService zoo)
        {
            zoo.AddAnimalToEnclosure(AnimalFactory.CreateTiger("Shere Khan", 5, 220, GenderType.Male, 65));
            zoo.AddAnimalToEnclosure(AnimalFactory.CreateTiger("Lea", 4, 180, GenderType.Female, 58));

            zoo.AddAnimalToEnclosure(AnimalFactory.CreateCrocodile("Gena", 12, 500, GenderType.Male, true));
            zoo.AddAnimalToEnclosure(AnimalFactory.CreateCrocodile("Laguna", 10, 430, GenderType.Female, true));

            zoo.AddAnimalToEnclosure(AnimalFactory.CreateKangaroo("Jack", 3, 85, GenderType.Male, 9.5));
            zoo.AddAnimalToEnclosure(AnimalFactory.CreateKangaroo("Kira", 4, 72, GenderType.Female, 8.7));
        }
    }
}
