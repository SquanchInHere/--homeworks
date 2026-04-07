namespace Zoo.Src.Models
{
    public class FoodStorage
    {
        public string FoodType { get; set; }
        public double AmountKg { get; set; }

        public FoodStorage(string foodType, double amountKg)
        {
            FoodType = foodType;
            AmountKg = amountKg;
        }

        public void AddFood(double amountKg)
        {
            AmountKg += amountKg;
        }

        public bool UseFood(double amountKg)
        {
            if (AmountKg < amountKg)
                return false;

            AmountKg -= amountKg;
            return true;
        }

        public override string ToString()
        {
            return $"{FoodType}: {AmountKg:F2} kg";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not FoodStorage other)
                return false;

            return FoodType == other.FoodType &&
                   AmountKg == other.AmountKg;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FoodType, AmountKg);
        }
    }
}
