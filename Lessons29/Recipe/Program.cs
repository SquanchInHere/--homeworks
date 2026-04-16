using Recipe.Interfaces;
using Recipe.Services;
using Recipe.Storages;



string baseDirectory = AppContext.BaseDirectory;
string dataDirectory = Path.Combine(baseDirectory, "Data");
string inputFilePath = Path.Combine(dataDirectory, "recipes.json");

IRecipeStorage storage = new JsonRecipeStorage();
IRecipeService recipeService = new RecipeService(storage.Load(inputFilePath));
IReportService reportService = new ReportService();

var menuService = new ConsoleMenuService(recipeService, storage, reportService);
menuService.Run();
