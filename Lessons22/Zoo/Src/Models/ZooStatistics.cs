namespace Zoo.Src.Models
{
    public class ZooStatistics
    {
        public int TotalBorn { get; set; }
        public int TotalDied { get; set; }
        public int TotalVisitors { get; set; }
        public decimal TotalIncomeFromVisitors { get; set; }
        public decimal TotalExpenses { get; set; }
        public int TotalDays { get; set; }

        public override string ToString()
        {
            return $"Total days: {TotalDays}\n" +
                   $"Total born: {TotalBorn}\n" +
                   $"Total died: {TotalDied}\n" +
                   $"Total visitors: {TotalVisitors}\n" +
                   $"Total visitor income: {TotalIncomeFromVisitors:C}\n" +
                   $"Total expenses: {TotalExpenses:C}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not ZooStatistics other)
                return false;

            return TotalBorn == other.TotalBorn &&
                   TotalDied == other.TotalDied &&
                   TotalVisitors == other.TotalVisitors &&
                   TotalIncomeFromVisitors == other.TotalIncomeFromVisitors &&
                   TotalExpenses == other.TotalExpenses &&
                   TotalDays == other.TotalDays;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TotalBorn, TotalDied, TotalVisitors, TotalIncomeFromVisitors, TotalExpenses, TotalDays);
        }
    }
}
