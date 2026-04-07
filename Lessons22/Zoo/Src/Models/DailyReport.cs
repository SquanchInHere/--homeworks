namespace Zoo.Src.Models
{
    public class DailyReport
    {
        public int DayNumber { get; set; }
        public int Visitors { get; set; }
        public decimal Income { get; set; }
        public decimal Expenses { get; set; }
        public int BornAnimals { get; set; }
        public int DiedAnimals { get; set; }
        public int AliveAnimals { get; set; }

        public override string ToString()
        {
            return $"Day: {DayNumber}\n" +
                   $"Visitors: {Visitors}\n" +
                   $"Income: {Income:C}\n" +
                   $"Expenses: {Expenses:C}\n" +
                   $"Born animals: {BornAnimals}\n" +
                   $"Died animals: {DiedAnimals}\n" +
                   $"Alive animals: {AliveAnimals}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not DailyReport other)
                return false;

            return DayNumber == other.DayNumber &&
                   Visitors == other.Visitors &&
                   Income == other.Income &&
                   Expenses == other.Expenses &&
                   BornAnimals == other.BornAnimals &&
                   DiedAnimals == other.DiedAnimals &&
                   AliveAnimals == other.AliveAnimals;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DayNumber, Visitors, Income, Expenses, BornAnimals, DiedAnimals, AliveAnimals);
        }
    }

}
