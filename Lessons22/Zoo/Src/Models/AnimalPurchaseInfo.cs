namespace Zoo.Src.Models
{
    public class AnimalPurchaseInfo
    {
        public string Species { get; set; }
        public decimal Price { get; set; }

        public AnimalPurchaseInfo(string species, decimal price)
        {
            Species = species;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Species} - {Price:C}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not AnimalPurchaseInfo other)
                return false;

            return Species == other.Species &&
                   Price == other.Price;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Species, Price);
        }
    }
}
