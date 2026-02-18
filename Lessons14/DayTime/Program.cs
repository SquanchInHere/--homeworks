using HappyTime.SRC;

namespace HappyTime
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Discount.Run(DayTime.Morning);
            Discount.Run(DayTime.Night);
            Discount.Run(DayTime.Afternoon);
            Discount.Run(DayTime.Evening);
        }
    }
}
