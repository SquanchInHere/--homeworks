namespace Zoo.Src.Interfaces
{
    public interface IFeedable
    {
        string FoodType { get; }
        double DailyFoodAmountKg { get; }
        void Eat();
    }
}
