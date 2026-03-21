namespace MoneyProduct.Src.Lib
{
    public class Money
    {
        protected int WholePart { get; set; }
        protected int FractionalPart { get; set; }

        public string Currency { get; set; }

        public Money()
        {
            WholePart = 0;
            FractionalPart = 0;
            Currency = "UAH";
        }

        public Money(int wholePart, int fractionalPart, string currency)
        {
            SetAmount(wholePart, fractionalPart);
            Currency = currency;
        }

        public void SetAmount(int wholePart, int fractionalPart)
        {
            if (wholePart < 0 || fractionalPart < 0)
            {
                throw new ArgumentException("Money amount cannot be negative.");
            }

            WholePart = wholePart + fractionalPart / 100;
            FractionalPart = fractionalPart % 100;
        }

        public override string ToString()
        {
            return $"{WholePart}.{FractionalPart:D2} {Currency}";
        }
    }
}
