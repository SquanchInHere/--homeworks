using Recipe.Enums;
using Recipe.Helpers;
using Recipe.Interfaces;
using Recipe.Models;
using Recipe.Reports;
using Recipe.Records;

namespace Recipe.Services;

public class ConsoleMenuService
{
    private readonly IRecipeService _recipeService;
    private readonly IRecipeStorage _storage;
    private readonly IReportService _reportService;

    private const string StorageFilePath = "Data/recipes.json";
    private const string ReportsDirectory = "Data/reports";

    public ConsoleMenuService(
        IRecipeService recipeService,
        IRecipeStorage storage,
        IReportService reportService)
    {
        _recipeService = recipeService;
        _storage = storage;
        _reportService = reportService;
    }

    public void Run()
    {
        while (true)
        {
            PrintMainMenu();
            string input = ConsoleHelper.ReadString("Choose an option: ");

            switch (input)
            {
                case "1":
                    AddRecipe();
                    break;
                case "2":
                    DeleteRecipe();
                    break;
                case "3":
                    UpdateRecipe();
                    break;
                case "4":
                    SearchRecipes();
                    break;
                case "5":
                    ShowAllRecipes();
                    break;
                case "6":
                    SaveRecipes();
                    break;
                case "7":
                    LoadRecipes();
                    break;
                case "8":
                    GenerateReport();
                    break;
                case "0":
                    Console.WriteLine("Goodbye.");
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    private static void PrintMainMenu()
    {
        Console.WriteLine();
        Console.WriteLine("===== RECIPE COLLECTION APP =====");
        Console.WriteLine("1. Add recipe");
        Console.WriteLine("2. Delete recipe");
        Console.WriteLine("3. Update recipe");
        Console.WriteLine("4. Search recipes");
        Console.WriteLine("5. Show all recipes");
        Console.WriteLine("6. Save recipes to file");
        Console.WriteLine("7. Load recipes from file");
        Console.WriteLine("8. Generate report");
        Console.WriteLine("0. Exit");
    }

    private void AddRecipe()
    {
        RecipeCreateRecord record = ReadRecipeCreateRecord();
        Recipe.Models.Recipe recipe = _recipeService.Add(record);

        Console.WriteLine("Recipe added successfully.");
        Console.WriteLine(recipe);
    }

    private void DeleteRecipe()
    {
        ShowAllRecipesShort();

        string idText = ConsoleHelper.ReadString("Enter recipe ID to delete: ");
        if (!Guid.TryParse(idText, out Guid id))
        {
            Console.WriteLine("Invalid GUID.");
            return;
        }

        bool result = _recipeService.Delete(id);
        Console.WriteLine(result ? "Recipe deleted." : "Recipe not found.");
    }

    private void UpdateRecipe()
    {
        ShowAllRecipesShort();

        string idText = ConsoleHelper.ReadString("Enter recipe ID to update: ");
        if (!Guid.TryParse(idText, out Guid id))
        {
            Console.WriteLine("Invalid GUID.");
            return;
        }

        Recipe.Models.Recipe? existingRecipe = _recipeService.GetById(id);
        if (existingRecipe == null)
        {
            Console.WriteLine("Recipe not found.");
            return;
        }

        Console.WriteLine("Enter new recipe data:");
        RecipeCreateRecord record = ReadRecipeCreateRecord();

        bool result = _recipeService.Update(id, record);
        Console.WriteLine(result ? "Recipe updated." : "Recipe not found.");
    }

    private void SearchRecipes()
    {
        Console.WriteLine();
        Console.WriteLine("===== SEARCH MENU =====");
        Console.WriteLine("1. By recipe name");
        Console.WriteLine("2. By cuisine");
        Console.WriteLine("3. By ingredient");
        Console.WriteLine("4. By maximum cooking time");
        Console.WriteLine("5. By maximum calories");
        Console.WriteLine("6. By dish type");

        string input = ConsoleHelper.ReadString("Choose search type: ");

        RecipeSearchRecord searchRecord = input switch
        {
            "1" => new RecipeSearchRecord(
                RecipeName: ConsoleHelper.ReadString("Enter recipe name: ")),
            "2" => new RecipeSearchRecord(
                CuisineName: ConsoleHelper.ReadString("Enter cuisine name: ")),
            "3" => new RecipeSearchRecord(
                IngredientName: ConsoleHelper.ReadString("Enter ingredient name: ")),
            "4" => new RecipeSearchRecord(
                MaxCookingTime: ConsoleHelper.ReadInt("Enter maximum cooking time in minutes: ")),
            "5" => new RecipeSearchRecord(
                MaxCalories: ConsoleHelper.ReadInt("Enter maximum total calories: ")),
            "6" => new RecipeSearchRecord(
                DishType: ReadDishType()),
            _ => new RecipeSearchRecord()
        };

        List<Recipe.Models.Recipe> results = _recipeService.Search(searchRecord);

        if (results.Count == 0)
        {
            Console.WriteLine("No recipes found.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("===== SEARCH RESULTS =====");
        foreach (var recipe in results)
        {
            Console.WriteLine(recipe);
            Console.WriteLine(new string('-', 50));
        }
    }

    private void ShowAllRecipes()
    {
        List<Recipe.Models.Recipe> recipes = _recipeService.GetAll();

        if (recipes.Count == 0)
        {
            Console.WriteLine("No recipes available.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("===== ALL RECIPES =====");
        foreach (var recipe in recipes)
        {
            Console.WriteLine(recipe);
            Console.WriteLine(new string('-', 50));
        }
    }

    private void ShowAllRecipesShort()
    {
        List<Recipe.Models.Recipe> recipes = _recipeService.GetAll();

        if (recipes.Count == 0)
        {
            Console.WriteLine("No recipes available.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("===== RECIPES =====");
        foreach (var recipe in recipes)
        {
            Console.WriteLine($"{recipe.Id} | {recipe.RecipeName} | {recipe.CuisineName} | {recipe.TotalCalories} cal");
        }
    }

    private void SaveRecipes()
    {
        try
        {
            _storage.Save(StorageFilePath, _recipeService.GetAll());
            Console.WriteLine($"Recipes saved to '{StorageFilePath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Save error: {ex.Message}");
        }
    }

    private void LoadRecipes()
    {
        try
        {
            List<Recipe.Models.Recipe> recipes = _storage.Load(StorageFilePath);
            _recipeService.ReplaceAll(recipes);
            Console.WriteLine($"Recipes loaded from '{StorageFilePath}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Load error: {ex.Message}");
        }
    }

    private void GenerateReport()
    {
        Console.WriteLine();
        Console.WriteLine("===== REPORT MENU =====");
        Console.WriteLine("1. Report by cuisine type");
        Console.WriteLine("2. Report by cooking time");
        Console.WriteLine("3. Report by ingredient names");
        Console.WriteLine("4. Report by recipe name");
        Console.WriteLine("5. Report by total calories");
        Console.WriteLine("6. Report by dish type");

        string input = ConsoleHelper.ReadString("Choose report type: ");
        List<Recipe.Models.Recipe> recipes = _recipeService.GetAll();

        var report = input switch
        {
            "1" => _reportService.GetByCuisine(recipes),
            "2" => _reportService.GetByCookingTime(recipes),
            "3" => _reportService.GetByIngredients(recipes),
            "4" => _reportService.GetByRecipeName(recipes),
            "5" => _reportService.GetByCalories(recipes),
            "6" => _reportService.GetByDishType(recipes),
            _ => null
        };

        if (report == null)
        {
            Console.WriteLine("Invalid option.");
            return;
        }

        ReportPrinter.Print(report);

        string saveChoice = ConsoleHelper.ReadString("Save report to file? (y/n): ");
        if (saveChoice.Equals("y", StringComparison.OrdinalIgnoreCase))
        {
            string fileName = ConsoleHelper.ReadString("Enter report file name (without extension): ");
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = "report";
            }

            string path = Path.Combine(ReportsDirectory, $"{fileName}.txt");
            ReportPrinter.SaveToFile(path, report);
            Console.WriteLine($"Report saved to '{path}'.");
        }
    }

    private static RecipeCreateRecord ReadRecipeCreateRecord()
    {
        string recipeName = ConsoleHelper.ReadString("Recipe name: ");
        string cuisineName = ConsoleHelper.ReadString("Cuisine name: ");
        int cookingTime = ConsoleHelper.ReadInt("Cooking time in minutes: ");
        DishType dishType = ReadDishType();

        int ingredientCount = ConsoleHelper.ReadInt("Number of ingredients: ");
        var ingredients = new List<IngredientRecord>();

        for (int i = 0; i < ingredientCount; i++)
        {
            Console.WriteLine($"Ingredient #{i + 1}");
            string name = ConsoleHelper.ReadString("  Name: ");
            int calories = ConsoleHelper.ReadInt("  Calories: ");
            ingredients.Add(new IngredientRecord(name, calories));
        }

        int stepsCount = ConsoleHelper.ReadInt("Number of cooking steps: ");
        var steps = new List<string>();

        for (int i = 0; i < stepsCount; i++)
        {
            string step = ConsoleHelper.ReadString($"Step #{i + 1}: ");
            steps.Add(step);
        }

        return new RecipeCreateRecord(
            recipeName,
            cuisineName,
            ingredients,
            cookingTime,
            steps,
            dishType
        );
    }

    private static DishType ReadDishType()
    {
        Console.WriteLine("Dish Types:");
        Console.WriteLine("1. Salad");
        Console.WriteLine("2. First Course");
        Console.WriteLine("3. Main Course");
        Console.WriteLine("4. Appetizer");
        Console.WriteLine("5. Dessert");

        while (true)
        {
            int value = ConsoleHelper.ReadInt("Choose dish type: ");
            if (Enum.IsDefined(typeof(DishType), value))
            {
                return (DishType)value;
            }

            Console.WriteLine("Invalid dish type.");
        }
    }
}
