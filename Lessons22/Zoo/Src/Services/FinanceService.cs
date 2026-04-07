using Zoo.Src.Models;

namespace Zoo.Src.Services
{
    public class FinanceService
    {
        public decimal Money { get; set; }

        public FinanceService(decimal startMoney)
        {
            Money = startMoney;
        }

        public void AddIncome(decimal amount)
        {
            Money += amount;
        }

        public void AddExpense(decimal amount)
        {
            Money -= amount;
        }

        public bool CanAfford(decimal amount)
        {
            return Money >= amount;
        }

        public bool BuyFood(FoodService foodService, string foodType, double amountKg, decimal pricePerKg)
        {
            decimal totalPrice = CalculateFoodPrice(amountKg, pricePerKg);

            if (!CanAfford(totalPrice))
                return false;

            AddExpense(totalPrice);
            foodService.BuyFood(foodType, amountKg);
            return true;
        }

        public bool BuyAnimal(List<AnimalPurchaseInfo> priceList, string species)
        {
            var item = priceList.FirstOrDefault(p => p.Species == species);

            if (item == null)
                return false;

            if (!CanAfford(item.Price))
                return false;

            AddExpense(item.Price);
            return true;
        }

        private decimal CalculateFoodPrice(double amountKg, decimal pricePerKg)
        {
            return (decimal)amountKg * pricePerKg;
        }

        public override string ToString()
        {
            return $"Zoo balance: {Money:C}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not FinanceService other)
                return false;

            return Money == other.Money;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Money);
        }
    }
}
