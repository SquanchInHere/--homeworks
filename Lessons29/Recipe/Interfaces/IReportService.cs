using Recipe.Models;

namespace Recipe.Interfaces;

public interface IReportService
{
    ReportResult GetByCuisine(List<Recipe.Models.Recipe> recipes);
    ReportResult GetByCookingTime(List<Recipe.Models.Recipe> recipes);
    ReportResult GetByIngredients(List<Recipe.Models.Recipe> recipes);
    ReportResult GetByRecipeName(List<Recipe.Models.Recipe> recipes);
    ReportResult GetByCalories(List<Recipe.Models.Recipe> recipes);
    ReportResult GetByDishType(List<Recipe.Models.Recipe> recipes);
}
