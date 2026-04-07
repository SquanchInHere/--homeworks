using Zoo.Src.Abstractions;
using Zoo.Src.Models;

namespace Zoo.Src.Services
{
    public class FoodService
    {
        public List<FoodStorage> FoodStorages { get; private set; }

        public FoodService(List<FoodStorage> foodStorages)
        {
            FoodStorages = foodStorages;
        }

        public bool FeedAnimal(Animal animal)
        {
            var storage = FindStorage(animal.FoodType);

            if (storage == null)
                return false;

            if (!storage.UseFood(animal.DailyFoodAmountKg))
                return false;

            animal.Eat();
            return true;
        }

        public void BuyFood(string foodType, double amountKg)
        {
            var storage = FindStorage(foodType);

            if (storage == null)
                FoodStorages.Add(new FoodStorage(foodType, amountKg));
            else
                storage.AddFood(amountKg);
        }

        public string GetFoodStatus()
        {
            if (FoodStorages.Count == 0)
                return "Food storage is empty.";

            return string.Join("\n", FoodStorages.Select(f => f.ToString()));
        }

        private FoodStorage? FindStorage(string foodType)
        {
            return FoodStorages.FirstOrDefault(f => f.FoodType == foodType);
        }
    }
}
