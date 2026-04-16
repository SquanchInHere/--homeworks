using Recipe.Interfaces;
using Recipe.Models;

namespace Recipe.Services;

public class ReportService : IReportService
{
    public ReportResult GetByCuisine(List<Recipe.Models.Recipe> recipes)
    {
        var result = new ReportResult
        {
            Title = "=== Report by Cuisine Type ==="
        };

        if (recipes.Count == 0)
        {
            result.Lines.Add("No recipes available.");
            return result;
        }

        foreach (var group in recipes.GroupBy(r => r.CuisineName))
        {
            result.Lines.Add(string.Empty);
            result.Lines.Add($"Cuisine: {group.Key}");
            foreach (var recipe in group)
            {
                result.Lines.Add($"- {recipe.RecipeName}");
            }
        }

        return result;
    }

    public ReportResult GetByCookingTime(List<Recipe.Models.Recipe> recipes)
    {
        var result = new ReportResult
        {
            Title = "=== Report by Cooking Time ==="
        };

        if (recipes.Count == 0)
        {
            result.Lines.Add("No recipes available.");
            return result;
        }

        foreach (var recipe in recipes.OrderBy(r => r.CookingTimeMinutes))
        {
            result.Lines.Add($"{recipe.RecipeName} - {recipe.CookingTimeMinutes} minutes");
        }

        return result;
    }

    public ReportResult GetByIngredients(List<Recipe.Models.Recipe> recipes)
    {
        var result = new ReportResult
        {
            Title = "=== Report by Ingredient Names ==="
        };

        if (recipes.Count == 0)
        {
            result.Lines.Add("No recipes available.");
            return result;
        }

        foreach (var recipe in recipes)
        {
            result.Lines.Add(string.Empty);
            result.Lines.Add($"{recipe.RecipeName}:");
            foreach (var ingredient in recipe.Ingredients)
            {
                result.Lines.Add($"- {ingredient.Name} ({ingredient.Calories} cal)");
            }
        }

        return result;
    }

    public ReportResult GetByRecipeName(List<Recipe.Models.Recipe> recipes)
    {
        var result = new ReportResult
        {
            Title = "=== Report by Recipe Name ==="
        };

        if (recipes.Count == 0)
        {
            result.Lines.Add("No recipes available.");
            return result;
        }

        foreach (var recipe in recipes.OrderBy(r => r.RecipeName))
        {
            result.Lines.Add(recipe.RecipeName);
        }

        return result;
    }

    public ReportResult GetByCalories(List<Recipe.Models.Recipe> recipes)
    {
        var result = new ReportResult
        {
            Title = "=== Report by Total Calories ==="
        };

        if (recipes.Count == 0)
        {
            result.Lines.Add("No recipes available.");
            return result;
        }

        foreach (var recipe in recipes.OrderBy(r => r.TotalCalories))
        {
            result.Lines.Add($"{recipe.RecipeName} - {recipe.TotalCalories} cal");
        }

        return result;
    }

    public ReportResult GetByDishType(List<Recipe.Models.Recipe> recipes)
    {
        var result = new ReportResult
        {
            Title = "=== Report by Dish Type ==="
        };

        if (recipes.Count == 0)
        {
            result.Lines.Add("No recipes available.");
            return result;
        }

        foreach (var group in recipes.GroupBy(r => r.DishType))
        {
            result.Lines.Add(string.Empty);
            result.Lines.Add($"Dish Type: {group.Key}");
            foreach (var recipe in group)
            {
                result.Lines.Add($"- {recipe.RecipeName}");
            }
        }

        return result;
    }
}
