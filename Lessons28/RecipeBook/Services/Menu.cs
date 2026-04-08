using RecipeBook.Models;
using RecipeBook.Storage;
using Task3.RecipeBook.Services;

namespace RecipeBook.Services
{
    public static class Menu
    {
        public static void Run()
        {
            string baseDirectory = AppContext.BaseDirectory;
            string dataDirectory = Path.Combine(baseDirectory, "Data");
            string recipesFilePath = Path.Combine(dataDirectory, "Recipes.json");
            string reportFilePath = Path.Combine(dataDirectory, "Report.txt");

            var recipeStorage = new RecipeFileStorage();
            var recipeService = new RecipeService();
            var recipeSearchService = new RecipeSearchService();
            var reportService = new ReportService();

            try
            {
                List<Recipe> recipesFromFile = recipeStorage.Load(recipesFilePath);
                recipeService.ReplaceAll(recipesFromFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: failed to load recipes from file. {ex.Message}");
            }

            while (true)
            {
                Console.WriteLine("=== Task 3: Recipe Book ===");
                Console.WriteLine($"Recipes file: {recipesFilePath}");
                Console.WriteLine($"Report file:  {reportFilePath}");
                Console.WriteLine();
                Console.WriteLine("1. Show all recipes");
                Console.WriteLine("2. Add recipe");
                Console.WriteLine("3. Remove recipe");
                Console.WriteLine("4. Update recipe");
                Console.WriteLine("5. Search recipes");
                Console.WriteLine("6. Save recipes to file");
                Console.WriteLine("7. Reload recipes from file");
                Console.WriteLine("8. Generate report");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine() ?? string.Empty;
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowRecipes(recipeService.GetAll());
                        break;

                    case "2":
                        AddRecipe(recipeService);
                        break;

                    case "3":
                        RemoveRecipe(recipeService);
                        break;

                    case "4":
                        UpdateRecipe(recipeService);
                        break;

                    case "5":
                        SearchRecipes(recipeService, recipeSearchService);
                        break;

                    case "6":
                        SaveRecipes(recipeService, recipeStorage, recipesFilePath);
                        break;

                    case "7":
                        ReloadRecipes(recipeService, recipeStorage, recipesFilePath);
                        break;

                    case "8":
                        GenerateReport(recipeService, reportService, reportFilePath);
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void ShowRecipes(IEnumerable<Recipe> recipes)
        {
            List<Recipe> recipeList = recipes.ToList();

            if (recipeList.Count == 0)
            {
                Console.WriteLine("No recipes found.");
                return;
            }

            foreach (Recipe recipe in recipeList)
            {
                Console.WriteLine(recipe);
                Console.WriteLine(new string('-', 50));
            }
        }

        static void AddRecipe(RecipeService recipeService)
        {
            Recipe recipe = ReadRecipeFromConsole();
            recipeService.Add(recipe);
            Console.WriteLine("Recipe added successfully.");
        }

        static void RemoveRecipe(RecipeService recipeService)
        {
            Console.Write("Enter the recipe name to remove: ");
            string recipeName = Console.ReadLine() ?? string.Empty;

            bool removed = recipeService.Remove(recipeName);
            Console.WriteLine(removed ? "Recipe removed successfully." : "Recipe not found.");
        }

        static void UpdateRecipe(RecipeService recipeService)
        {
            Console.Write("Enter the existing recipe name to update: ");
            string existingRecipeName = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter updated recipe data:");
            Recipe updatedRecipe = ReadRecipeFromConsole();

            bool updated = recipeService.Update(existingRecipeName, updatedRecipe);
            Console.WriteLine(updated ? "Recipe updated successfully." : "Recipe not found.");
        }

        private static void SearchRecipes(RecipeService recipeService, RecipeSearchService recipeSearchService)
        {
            IReadOnlyList<Recipe> recipes = recipeService.GetAll();

            Console.WriteLine("=== Search Menu ===");
            Console.WriteLine("1. Search by name");
            Console.WriteLine("2. Search by cuisine");
            Console.WriteLine("3. Search by ingredient");
            Console.WriteLine("4. Search by cooking time");
            Console.WriteLine("5. Search by calories");
            Console.WriteLine("6. Search by dish type");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine() ?? string.Empty;
            List<Recipe> results = new();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter recipe name: ");
                    results = recipeSearchService.SearchByName(recipes, Console.ReadLine() ?? string.Empty);
                    break;

                case "2":
                    Console.Write("Enter cuisine: ");
                    results = recipeSearchService.SearchByCuisine(recipes, Console.ReadLine() ?? string.Empty);
                    break;

                case "3":
                    Console.Write("Enter ingredient name: ");
                    results = recipeSearchService.SearchByIngredient(recipes, Console.ReadLine() ?? string.Empty);
                    break;

                case "4":
                    Console.Write("Enter max cooking time in minutes: ");
                    results = recipeSearchService.SearchByCookingTime(
                        recipes,
                        int.TryParse(Console.ReadLine(), out int time) ? time : int.MaxValue);
                    break;

                case "5":
                    Console.Write("Enter max calories: ");
                    results = recipeSearchService.SearchByCalories(
                        recipes,
                        int.TryParse(Console.ReadLine(), out int calories) ? calories : int.MaxValue);
                    break;

                case "6":
                    DishType dishType = ReadDishType();
                    results = recipeSearchService.SearchByDishType(recipes, dishType);
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    return;
            }

            Console.WriteLine();
            ShowRecipes(results);
        }

        private static void SaveRecipes(RecipeService recipeService, RecipeFileStorage recipeStorage, string recipesFilePath)
        {
            recipeStorage.Save(recipesFilePath, recipeService.GetAll());
            Console.WriteLine("Recipes saved successfully.");
        }

        private static void ReloadRecipes(RecipeService recipeService, RecipeFileStorage recipeStorage, string recipesFilePath)
        {
            try
            {
                List<Recipe> recipes = recipeStorage.Load(recipesFilePath);
                recipeService.ReplaceAll(recipes);
                Console.WriteLine("Recipes reloaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void GenerateReport(RecipeService recipeService, ReportService reportService, string reportFilePath)
        {
            IReadOnlyList<Recipe> recipes = recipeService.GetAll();

            Console.WriteLine("=== Report Menu ===");
            Console.WriteLine("1. Report by cuisine");
            Console.WriteLine("2. Report by cooking time");
            Console.WriteLine("3. Report by ingredients");
            Console.WriteLine("4. Report by recipe name");
            Console.WriteLine("5. Report by calories");
            Console.WriteLine("6. Report by dish type");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine() ?? string.Empty;

            string report = choice switch
            {
                "1" => reportService.GenerateByCuisine(recipes),
                "2" => reportService.GenerateByCookingTime(recipes),
                "3" => reportService.GenerateByIngredients(recipes),
                "4" => reportService.GenerateByRecipeName(recipes),
                "5" => reportService.GenerateByCalories(recipes),
                "6" => reportService.GenerateByDishType(recipes),
                _ => "Invalid option."
            };

            Console.WriteLine();
            Console.WriteLine("=== Generated Report ===");
            Console.WriteLine(report);

            File.WriteAllText(reportFilePath, report);
            Console.WriteLine();
            Console.WriteLine($"Report saved to file: {reportFilePath}");
        }

        private static Recipe ReadRecipeFromConsole()
        {
            var recipe = new Recipe();

            Console.Write("Enter recipe name: ");
            recipe.Name = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter cuisine: ");
            recipe.Cuisine = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter cooking time in minutes: ");
            recipe.CookingTimeMinutes = int.TryParse(Console.ReadLine(), out int cookingTime)
                ? cookingTime
                : 0;

            recipe.DishType = ReadDishType();

            Console.Write("Enter ingredient count: ");
            int ingredientCount = int.TryParse(Console.ReadLine(), out int ingredientsCountParsed)
                ? ingredientsCountParsed
                : 0;

            for (int i = 0; i < ingredientCount; i++)
            {
                Console.Write($"Enter ingredient #{i + 1} name: ");
                string ingredientName = Console.ReadLine() ?? string.Empty;

                Console.Write($"Enter ingredient #{i + 1} calories: ");
                int ingredientCalories = int.TryParse(Console.ReadLine(), out int caloriesParsed)
                    ? caloriesParsed
                    : 0;

                recipe.Ingredients.Add(new Ingredient
                {
                    Name = ingredientName,
                    Calories = ingredientCalories
                });
            }

            Console.Write("Enter number of cooking steps: ");
            int stepCount = int.TryParse(Console.ReadLine(), out int stepCountParsed)
                ? stepCountParsed
                : 0;

            for (int i = 0; i < stepCount; i++)
            {
                Console.Write($"Enter step #{i + 1}: ");
                recipe.Steps.Add(Console.ReadLine() ?? string.Empty);
            }

            return recipe;
        }

        private static DishType ReadDishType()
        {
            Console.WriteLine("Choose dish type:");
            foreach (DishType dishType in Enum.GetValues<DishType>())
            {
                Console.WriteLine($"{(int)dishType}. {dishType}");
            }

            Console.Write("Enter dish type number: ");
            int dishTypeValue = int.TryParse(Console.ReadLine(), out int parsedValue)
                ? parsedValue
                : (int)DishType.MainCourse;

            return Enum.IsDefined(typeof(DishType), dishTypeValue)
                ? (DishType)dishTypeValue
                : DishType.MainCourse;
        }

    }
}
