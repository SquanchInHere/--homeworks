using MoneyProduct.Src.Lib;

namespace MoneyProduct.Src.Services
{
    public class Product : Money
    {
        public string Name { get; set; }

        public Product(string name, int wholePart, int fractionalPart, string currency)
            : base(wholePart, fractionalPart, currency)
        {
            Name = name;
        }

        public void ReducePrice(int wholePart, int fractionalPart)
        {
            if (wholePart < 0 || fractionalPart < 0)
            {
                throw new ArgumentException("Reduction value cannot be negative.");
            }

            int currentAmount = WholePart * 100 + FractionalPart;
            int reductionAmount = wholePart * 100 + fractionalPart;

            if (reductionAmount > currentAmount)
            {
                throw new ArgumentException("Reduction cannot be greater than the current price.");
            }

            int result = currentAmount - reductionAmount;
            WholePart = result / 100;
            FractionalPart = result % 100;
        }

        public override string ToString()
        {
            return $"Product: {Name}, Price: {WholePart}.{FractionalPart:D2} {Currency}";
        }
    }
}
