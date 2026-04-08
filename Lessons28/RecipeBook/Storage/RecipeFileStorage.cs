using RecipeBook.Models;
using System.Text.Json;

namespace RecipeBook.Storage;

public class RecipeFileStorage
{
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        WriteIndented = true
    };

    public List<Recipe> Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Recipes file was not found.", filePath);
        }

        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<Recipe>>(json) ?? new List<Recipe>();
    }

    public void Save(string filePath, IEnumerable<Recipe> recipes)
    {
        string? directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string json = JsonSerializer.Serialize(recipes, _serializerOptions);
        File.WriteAllText(filePath, json);
    }
}