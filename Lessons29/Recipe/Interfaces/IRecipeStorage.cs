using Recipe.Models;

namespace Recipe.Interfaces;

public interface IRecipeStorage
{
    void Save(string filePath, List<Recipe.Models.Recipe> recipes);
    List<Recipe.Models.Recipe> Load(string filePath);
}
