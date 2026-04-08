using BaseLinq.Handlers;
using BaseLinq.Services;

namespace BaseLinq.Menus;

public class Menu
{
    private readonly EventLogger _logger;
    private readonly DateTimeService _dateTimeService;
    private readonly GeometryService _geometryService;
    private readonly ArrayLambdaService _arrayLambdaService;
    private readonly IntegerQueryService _integerQueryService;
    private readonly CityQueryService _cityQueryService;

    public Menu()
    {
        _logger = new EventLogger();
        _dateTimeService = new DateTimeService();
        _geometryService = new GeometryService();
        _arrayLambdaService = new ArrayLambdaService();
        _integerQueryService = new IntegerQueryService();
        _cityQueryService = new CityQueryService();

        SubscribeToEvents();
    }

    public void Run()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Clear();

        while (true)
        {
            ShowMenu();
            string? choice = Console.ReadLine();

            Console.Clear();

            switch (choice)
            {
                case "1":
                    RunTask1();
                    break;

                case "2":
                    RunTask2();
                    break;

                case "3":
                    RunTask3();
                    break;

                case "4":
                    RunTask4();
                    break;

                case "0":
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    private void SubscribeToEvents()
    {
        _dateTimeService.OperationPerformed += _logger.OnOperationPerformed;
        _geometryService.OperationPerformed += _logger.OnOperationPerformed;
        _arrayLambdaService.OperationPerformed += _logger.OnOperationPerformed;
        _integerQueryService.OperationPerformed += _logger.OnOperationPerformed;
        _cityQueryService.OperationPerformed += _logger.OnOperationPerformed;
    }

    private void ShowMenu()
    {
        Console.WriteLine("=== Delegates, Events, Basic LINQ to Objects ===");
        Console.WriteLine("1 - Task 1: Action / Func");
        Console.WriteLine("2 - Task 2: Lambda expressions");
        Console.WriteLine("3 - Task 3: LINQ for integers");
        Console.WriteLine("4 - Task 4: LINQ for cities");
        Console.WriteLine("0 - Exit");
        Console.Write("Choose an option: ");
    }

    private void RunTask1()
    {
        Console.WriteLine("=== Task 1: Action / Func ===");
        Console.WriteLine();

        Action showTime = _dateTimeService.ShowCurrentTime;
        Action showDate = _dateTimeService.ShowCurrentDate;
        Action showDayOfWeek = _dateTimeService.ShowCurrentDayOfWeek;

        Func<double, double, double> triangleAreaFunc = _geometryService.CalculateTriangleArea;
        Func<double, double, double> rectangleAreaFunc = _geometryService.CalculateRectangleArea;

        showTime();
        showDate();
        showDayOfWeek();

        Console.WriteLine();

        double triangleBase = 10;
        double triangleHeight = 6;
        double triangleArea = triangleAreaFunc(triangleBase, triangleHeight);
        Console.WriteLine($"Triangle area (base = {triangleBase}, height = {triangleHeight}) = {triangleArea}");

        double rectangleWidth = 8;
        double rectangleHeight = 5;
        double rectangleArea = rectangleAreaFunc(rectangleWidth, rectangleHeight);
        Console.WriteLine($"Rectangle area (width = {rectangleWidth}, height = {rectangleHeight}) = {rectangleArea}");
    }

    private void RunTask2()
    {
        Console.WriteLine("=== Task 2: Lambda expressions ===");
        Console.WriteLine();

        int[] numbers = { 7, 14, -7, 5, 10, -3, 21, 14, -3, 8, 0, 11, -12, 28 };
        string text = "C# delegates and LINQ are very useful in modern programming.";
        string wordToFind = "LINQ";

        Console.WriteLine("Array:");
        Console.WriteLine(string.Join(", ", numbers));
        Console.WriteLine();

        int divisibleBySevenCount = _arrayLambdaService.CountDivisibleBySeven(numbers);
        Console.WriteLine($"Count of numbers divisible by 7: {divisibleBySevenCount}");

        int positiveCount = _arrayLambdaService.CountPositiveNumbers(numbers);
        Console.WriteLine($"Count of positive numbers: {positiveCount}");

        IEnumerable<int> uniqueNegativeNumbers = _arrayLambdaService.GetUniqueNegativeNumbers(numbers);
        Console.WriteLine("Unique negative numbers:");
        Console.WriteLine(string.Join(", ", uniqueNegativeNumbers));

        bool containsWord = _arrayLambdaService.ContainsWord(text, wordToFind);
        Console.WriteLine($"Text contains word \"{wordToFind}\": {containsWord}");
    }

    private void RunTask3()
    {
        Console.WriteLine("=== Task 3: LINQ for integers ===");
        Console.WriteLine();

        List<int> numbers = _integerQueryService.GenerateNumbers(1, 50);

        PrintCollection("All integers", _integerQueryService.GetAll(numbers));
        PrintCollection("Even integers", _integerQueryService.GetEven(numbers));
        PrintCollection("Odd integers", _integerQueryService.GetOdd(numbers));
        PrintCollection("Numbers in range [10..25]", _integerQueryService.GetInRange(numbers, 10, 25));
        PrintCollection("Numbers divisible by 7 (sorted ascending)", _integerQueryService.GetDivisibleBySevenSorted(numbers));
        PrintCollection("Numbers divisible by 8", _integerQueryService.GetDivisibleByEight(numbers));
        PrintCollection("All integers sorted descending", _integerQueryService.GetDescending(numbers));
    }

    private void RunTask4()
    {
        Console.WriteLine("=== Task 4: LINQ for cities ===");
        Console.WriteLine();

        string[] cities =
        {
            "Amsterdam",
            "Athens",
            "Berlin",
            "New York",
            "Nizhnekamsk",
            "London",
            "Naples",
            "Arkham",
            "Stockholm",
            "Abu Dhabi",
            "Novokuznetsk",
            "Rome",
            "Oslo",
            "Alicante"
        };

        PrintCollection("All cities", _cityQueryService.GetAllCities(cities));
        PrintCollection("Cities with length 6", _cityQueryService.GetCitiesByLength(cities, 6));
        PrintCollection("Cities starting with A", _cityQueryService.GetCitiesStartingWithA(cities));
        PrintCollection("Cities ending with M", _cityQueryService.GetCitiesEndingWithM(cities));
        PrintCollection("Cities starting with N and ending with K", _cityQueryService.GetCitiesStartingWithNAndEndingWithK(cities));
    }

    private void PrintCollection<T>(string title, IEnumerable<T> collection)
    {
        Console.WriteLine(title + ":");
        Console.WriteLine(string.Join(", ", collection));
        Console.WriteLine();
    }
}