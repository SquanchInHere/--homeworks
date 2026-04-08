using RecipeBook.Models;

namespace RecipeBook.Services;

public class RecipeSearchService
{
    public List<Recipe> SearchByName(IEnumerable<Recipe> recipes, string name)
    {
        return recipes
            .Where(recipe => recipe.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Recipe> SearchByCuisine(IEnumerable<Recipe> recipes, string cuisine)
    {
        return recipes
            .Where(recipe => recipe.Cuisine.Contains(cuisine, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Recipe> SearchByIngredient(IEnumerable<Recipe> recipes, string ingredientName)
    {
        return recipes
            .Where(recipe => recipe.Ingredients.Any(ingredient =>
                ingredient.Name.Contains(ingredientName, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }

    public List<Recipe> SearchByCookingTime(IEnumerable<Recipe> recipes, int maxCookingTimeMinutes)
    {
        return recipes
            .Where(recipe => recipe.CookingTimeMinutes <= maxCookingTimeMinutes)
            .ToList();
    }

    public List<Recipe> SearchByCalories(IEnumerable<Recipe> recipes, int maxCalories)
    {
        return recipes
            .Where(recipe => recipe.TotalCalories <= maxCalories)
            .ToList();
    }

    public List<Recipe> SearchByDishType(IEnumerable<Recipe> recipes, DishType dishType)
    {
        return recipes
            .Where(recipe => recipe.DishType == dishType)
            .ToList();
    }
}
