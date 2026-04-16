using System.Text.Json.Serialization;
using Recipe.Enums;

namespace Recipe.Models;

public class Recipe
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string RecipeName { get; set; } = string.Empty;
    public string CuisineName { get; set; } = string.Empty;
    public List<Ingredient> Ingredients { get; set; } = new();
    public int CookingTimeMinutes { get; set; }
    public List<string> CookingSteps { get; set; } = new();

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DishType DishType { get; set; }

    public int TotalCalories => Ingredients.Sum(i => i.Calories);

    public override string ToString()
    {
        string ingredientsText = Ingredients.Count == 0
            ? "No ingredients"
            : string.Join(", ", Ingredients.Select(i => i.ToString()));

        string stepsText = CookingSteps.Count == 0
            ? "No steps"
            : string.Join(Environment.NewLine, CookingSteps.Select((step, index) => $"{index + 1}. {step}"));

        return
            $"ID: {Id}{Environment.NewLine}" +
            $"Recipe Name: {RecipeName}{Environment.NewLine}" +
            $"Cuisine: {CuisineName}{Environment.NewLine}" +
            $"Dish Type: {DishType}{Environment.NewLine}" +
            $"Cooking Time: {CookingTimeMinutes} minutes{Environment.NewLine}" +
            $"Ingredients: {ingredientsText}{Environment.NewLine}" +
            $"Total Calories: {TotalCalories}{Environment.NewLine}" +
            $"Cooking Steps:{Environment.NewLine}{stepsText}";
    }
}
