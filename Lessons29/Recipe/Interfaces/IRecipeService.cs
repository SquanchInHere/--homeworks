using Recipe.Models;
using Recipe.Records;

namespace Recipe.Interfaces;

public interface IRecipeService
{
    List<Recipe.Models.Recipe> GetAll();
    Recipe.Models.Recipe Add(RecipeCreateRecord record);
    bool Delete(Guid id);
    bool Update(Guid id, RecipeCreateRecord record);
    List<Recipe.Models.Recipe> Search(RecipeSearchRecord record);
    Recipe.Models.Recipe? GetById(Guid id);
    void ReplaceAll(List<Recipe.Models.Recipe> recipes);
}
