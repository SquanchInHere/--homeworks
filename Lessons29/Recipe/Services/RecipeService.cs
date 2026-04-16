using Recipe.Interfaces;
using Recipe.Models;
using Recipe.Records;

namespace Recipe.Services;

public class RecipeService : IRecipeService
{
    private List<Recipe.Models.Recipe> _recipes;

    public RecipeService(List<Recipe.Models.Recipe>? recipes = null)
    {
        _recipes = recipes ?? new List<Recipe.Models.Recipe>();
    }

    public List<Recipe.Models.Recipe> GetAll()
    {
        return _recipes;
    }

    public Recipe.Models.Recipe Add(RecipeCreateRecord record)
    {
        var recipe = new Recipe.Models.Recipe
        {
            Id = Guid.NewGuid(),
            RecipeName = record.RecipeName,
            CuisineName = record.CuisineName,
            CookingTimeMinutes = record.CookingTimeMinutes,
            CookingSteps = record.CookingSteps,
            DishType = record.DishType,
            Ingredients = record.Ingredients
                .Select(i => new Ingredient
                {
                    Name = i.Name,
                    Calories = i.Calories
                })
                .ToList()
        };

        _recipes.Add(recipe);
        return recipe;
    }

    public bool Delete(Guid id)
    {
        var recipe = _recipes.FirstOrDefault(r => r.Id == id);
        if (recipe == null)
        {
            return false;
        }

        _recipes.Remove(recipe);
        return true;
    }

    public bool Update(Guid id, RecipeCreateRecord record)
    {
        var recipe = _recipes.FirstOrDefault(r => r.Id == id);
        if (recipe == null)
        {
            return false;
        }

        recipe.RecipeName = record.RecipeName;
        recipe.CuisineName = record.CuisineName;
        recipe.CookingTimeMinutes = record.CookingTimeMinutes;
        recipe.CookingSteps = record.CookingSteps;
        recipe.DishType = record.DishType;
        recipe.Ingredients = record.Ingredients
            .Select(i => new Ingredient
            {
                Name = i.Name,
                Calories = i.Calories
            })
            .ToList();

        return true;
    }

    public List<Recipe.Models.Recipe> Search(RecipeSearchRecord record)
    {
        IEnumerable<Recipe.Models.Recipe> query = _recipes;

        if (!string.IsNullOrWhiteSpace(record.RecipeName))
        {
            query = query.Where(r =>
                r.RecipeName.Contains(record.RecipeName, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(record.CuisineName))
        {
            query = query.Where(r =>
                r.CuisineName.Contains(record.CuisineName, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(record.IngredientName))
        {
            query = query.Where(r =>
                r.Ingredients.Any(i =>
                    i.Name.Contains(record.IngredientName, StringComparison.OrdinalIgnoreCase)));
        }

        if (record.MaxCookingTime.HasValue)
        {
            query = query.Where(r => r.CookingTimeMinutes <= record.MaxCookingTime.Value);
        }

        if (record.MaxCalories.HasValue)
        {
            query = query.Where(r => r.TotalCalories <= record.MaxCalories.Value);
        }

        if (record.DishType.HasValue)
        {
            query = query.Where(r => r.DishType == record.DishType.Value);
        }

        return query.ToList();
    }

    public Recipe.Models.Recipe? GetById(Guid id)
    {
        return _recipes.FirstOrDefault(r => r.Id == id);
    }

    public void ReplaceAll(List<Recipe.Models.Recipe> recipes)
    {
        _recipes = recipes;
    }
}
