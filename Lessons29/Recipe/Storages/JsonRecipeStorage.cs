using System.Text.Json;
using System.Text.Json.Serialization;
using Recipe.Interfaces;

namespace Recipe.Storages;

public class JsonRecipeStorage : IRecipeStorage
{
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public void Save(string filePath, List<Recipe.Models.Recipe> recipes)
    {
        string json = JsonSerializer.Serialize(recipes, _options);
        File.WriteAllText(filePath, json);
    }

    public List<Recipe.Models.Recipe> Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<Recipe.Models.Recipe>();
        }

        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<Recipe.Models.Recipe>>(json, _options) ?? new List<Recipe.Models.Recipe>();
    }
}
