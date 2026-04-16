using Recipe.Enums;

namespace Recipe.Records;

public record RecipeSearchRecord(
    string? RecipeName = null,
    string? CuisineName = null,
    string? IngredientName = null,
    int? MaxCookingTime = null,
    int? MaxCalories = null,
    DishType? DishType = null
);
