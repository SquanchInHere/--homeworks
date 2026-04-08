namespace RecipeBook.Models;

public class Recipe
{
    public string Name { get; set; } = string.Empty;
    public string Cuisine { get; set; } = string.Empty;
    public List<Ingredient> Ingredients { get; set; } = new();
    public int CookingTimeMinutes { get; set; }
    public List<string> Steps { get; set; } = new();
    public DishType DishType { get; set; }

    public int TotalCalories => Ingredients.Sum(i => i.Calories);

    public override string ToString()
    {
        return
            $"Name: {Name}\n" +
            $"Cuisine: {Cuisine}\n" +
            $"Dish type: {DishType}\n" +
            $"Cooking time: {CookingTimeMinutes} minutes\n" +
            $"Ingredients: {string.Join(", ", Ingredients.Select(i => i.ToString()))}\n" +
            $"Total calories: {TotalCalories} kcal\n" +
            $"Steps:\n - {string.Join("\n - ", Steps)}";
    }
}