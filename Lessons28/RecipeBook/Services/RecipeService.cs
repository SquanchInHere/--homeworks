using RecipeBook.Models;

namespace RecipeBook.Services;

public class RecipeService
{
    private readonly List<Recipe> _recipes = new();

    public IReadOnlyList<Recipe> GetAll()
    {
        return _recipes;
    }

    public void ReplaceAll(IEnumerable<Recipe> recipes)
    {
        _recipes.Clear();
        _recipes.AddRange(recipes);
    }

    public void Add(Recipe recipe)
    {
        _recipes.Add(recipe);
    }

    public bool Remove(string recipeName)
    {
        Recipe? recipe = _recipes.FirstOrDefault(r =>
            r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

        if (recipe is null)
        {
            return false;
        }

        _recipes.Remove(recipe);
        return true;
    }

    public bool Update(string existingRecipeName, Recipe updatedRecipe)
    {
        Recipe? recipe = _recipes.FirstOrDefault(r =>
            r.Name.Equals(existingRecipeName, StringComparison.OrdinalIgnoreCase));

        if (recipe is null)
        {
            return false;
        }

        recipe.Name = updatedRecipe.Name;
        recipe.Cuisine = updatedRecipe.Cuisine;
        recipe.Ingredients = updatedRecipe.Ingredients;
        recipe.CookingTimeMinutes = updatedRecipe.CookingTimeMinutes;
        recipe.Steps = updatedRecipe.Steps;
        recipe.DishType = updatedRecipe.DishType;

        return true;
    }
}