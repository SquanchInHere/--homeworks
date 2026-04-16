using Recipe.Enums;

namespace Recipe.Records;

public record RecipeCreateRecord(
    string RecipeName,
    string CuisineName,
    List<IngredientRecord> Ingredients,
    int CookingTimeMinutes,
    List<string> CookingSteps,
    DishType DishType
);
