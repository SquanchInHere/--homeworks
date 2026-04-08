using RecipeBook.Models;

namespace Task3.RecipeBook.Services;

public class ReportService
{
    public string GenerateByCuisine(IEnumerable<Recipe> recipes)
    {
        var groups = recipes.GroupBy(recipe => recipe.Cuisine);

        return string.Join(
            "\n\n",
            groups.Select(group =>
                $"Cuisine: {group.Key}\n" +
                string.Join('\n', group.Select(recipe => $" - {recipe.Name}"))));
    }

    public string GenerateByCookingTime(IEnumerable<Recipe> recipes)
    {
        return string.Join(
            '\n',
            recipes
                .OrderBy(recipe => recipe.CookingTimeMinutes)
                .Select(recipe => $"{recipe.Name} - {recipe.CookingTimeMinutes} minutes"));
    }

    public string GenerateByIngredients(IEnumerable<Recipe> recipes)
    {
        return string.Join(
            "\n\n",
            recipes.Select(recipe =>
                $"{recipe.Name}: {string.Join(", ", recipe.Ingredients.Select(i => i.Name))}"));
    }

    public string GenerateByRecipeName(IEnumerable<Recipe> recipes)
    {
        return string.Join(
            '\n',
            recipes
                .OrderBy(recipe => recipe.Name)
                .Select(recipe => recipe.Name));
    }

    public string GenerateByCalories(IEnumerable<Recipe> recipes)
    {
        return string.Join(
            '\n',
            recipes
                .OrderBy(recipe => recipe.TotalCalories)
                .Select(recipe => $"{recipe.Name} - {recipe.TotalCalories} kcal"));
    }

    public string GenerateByDishType(IEnumerable<Recipe> recipes)
    {
        var groups = recipes.GroupBy(recipe => recipe.DishType);

        return string.Join(
            "\n\n",
            groups.Select(group =>
                $"Dish type: {group.Key}\n" +
                string.Join('\n', group.Select(recipe => $" - {recipe.Name}"))));
    }
}