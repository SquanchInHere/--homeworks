namespace HappyTime.SRC
{
    public static class Discount
    {
        public static void Run(DayTime dayTime)
        {
            decimal price = CalculatePriceByTime(Params.basePrice, dayTime);
            Console.WriteLine(price);

            decimal happyHours = CalculatePriceWithWeekendHappyHours(Params.basePrice, dayTime, MyDayOfWeek.Saturday);
            Console.WriteLine(happyHours);

            happyHours = CalculatePriceWithWeekendHappyHours(Params.basePrice, dayTime, MyDayOfWeek.Sunday);
            Console.WriteLine(happyHours);
        }

        private static decimal CalculatePriceByTime(decimal basePrice, DayTime time)
        {
            decimal price = basePrice;

            if (time == DayTime.Morning)
                price = ApplyDiscount(price, Params.MORNING_DISCOUNT);

            if (time == DayTime.Evening)
                price = ApplyMarkup(price, Params.EVNING_MARK_UP);

            return price;
        }

        private static decimal CalculatePriceWithWeekendHappyHours(decimal basePrice, DayTime time, MyDayOfWeek day)
        {
            decimal price = CalculatePriceByTime(basePrice, time);

            bool weekend = (day == MyDayOfWeek.Saturday || day == MyDayOfWeek.Sunday);

            if (weekend && time == DayTime.Evening)
                price = ApplyDiscount(price, Params.HAPPY_TIME);

            return price;
        }

        private static decimal ApplyDiscount(decimal price, decimal percent)
        {
            return price * (1m - percent / 100m);
        }

        private static decimal ApplyMarkup(decimal price, decimal percent)
        {
            return price * (1m + percent / 100m);
        }
    }
}
