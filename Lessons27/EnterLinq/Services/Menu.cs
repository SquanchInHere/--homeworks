using EnterLinq.Data;
using EnterLinq.Helpers;
using EnterLinq.Models;
using EnterLinq.Records;

namespace EnterLinq.Services;

public class Menu
{
    private readonly CompanySeeder _companySeeder;
    private readonly LaptopSeeder _laptopSeeder;
    private readonly CompanyQueries _companyQueries;
    private readonly LaptopQueries _laptopQueries;
    private readonly NumberQueries _numberQueries;

    public Menu()
    {
        _companySeeder = new CompanySeeder();
        _laptopSeeder = new LaptopSeeder();
        _companyQueries = new CompanyQueries();
        _laptopQueries = new LaptopQueries();
        _numberQueries = new NumberQueries();
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
                    RunTask1And2();
                    break;
                case "2":
                    RunTask3();
                    break;
                case "3":
                    RunTask4();
                    break;
                case "4":
                    RunTask5();
                    break;
                case "0":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    private void ShowMenu()
    {
        Console.WriteLine("=== Intro to LINQ ===");
        Console.WriteLine("1 - Tasks 1 and 2 (Companies)");
        Console.WriteLine("2 - Task 3 (Management employees)");
        Console.WriteLine("3 - Task 4 (Custom number sorting)");
        Console.WriteLine("4 - Task 5 (Laptops)");
        Console.WriteLine("0 - Exit");
        Console.Write("Choose an option: ");
    }

    private void RunTask1And2()
    {
        List<Company> companies = _companySeeder.GetCompanies();

        Printer.PrintCollection(
            "All companies sorted by employees",
            _companyQueries.GetAllCompaniesSortedByEmployees(companies));

        Printer.PrintCollection(
            "Companies with 'Food' in name",
            _companyQueries.GetCompaniesWithFoodInName(companies));

        Printer.PrintCollection(
            "Marketing companies",
            _companyQueries.GetMarketingCompanies(companies));

        Printer.PrintCollection(
            "Marketing or IT companies",
            _companyQueries.GetMarketingOrItCompanies(companies));

        Printer.PrintCollection(
            "Companies with employees > 100",
            _companyQueries.GetCompaniesWithEmployeesMoreThan100(companies));

        Printer.PrintCollection(
            "Companies with employees from 100 to 300",
            _companyQueries.GetCompaniesWithEmployeesBetween100And300(companies));

        Printer.PrintCollection(
            "Companies in London",
            _companyQueries.GetCompaniesInLondon(companies));

        Printer.PrintCollection(
            "Companies with director surname White",
            _companyQueries.GetCompaniesWithDirectorSurnameWhite(companies));

        Printer.PrintCollection(
            "Companies founded more than 2 years ago",
            _companyQueries.GetCompaniesFoundedMoreThanTwoYearsAgo(companies));

        Printer.PrintCollection(
            "Companies founded 123 days ago",
            _companyQueries.GetCompaniesFounded123DaysAgo(companies));

        Printer.PrintCollection(
            "Companies with director surname Black and 'White' in name",
            _companyQueries.GetCompaniesWithDirectorBlackAndWhiteInName(companies));

        Printer.PrintCollection(
            "Top 3 companies by employees",
            _companyQueries.GetTop3ByEmployees(companies));

        Printer.PrintCollection(
            "Bottom 2 companies by employees",
            _companyQueries.GetBottom2ByEmployees(companies));
    }

    private void RunTask3()
    {
        List<Company> companies = _companySeeder.GetCompanies();

        IEnumerable<ManagementEmployees> employees1 =
            _companyQueries.GetAllEmployeesOfCompany(companies, "White Star Marketing");

        IEnumerable<ManagementEmployees> employees2 =
            _companyQueries.GetEmployeesOfCompanyWithSalaryMoreThan(companies, "Black IT Solutions", 4000);

        IEnumerable<ManagementEmployees> employees3 =
            _companyQueries.GetAllManagersFromAllCompanies(companies);

        IEnumerable<ManagementEmployees> employees4 =
            _companyQueries.GetEmployeesWith23InPhone(companies);

        IEnumerable<ManagementEmployees> employees5 =
            _companyQueries.GetEmployeesUsingGoogleEmail(companies);

        Printer.PrintCollection("All employees of White Star Marketing", employees1);
        Printer.PrintCollection("Employees of Black IT Solutions with salary > 4000", employees2);
        Printer.PrintCollection("All managers from all companies", employees3);
        Printer.PrintCollection("Employees with '23' in phone", employees4);
        Printer.PrintCollection("Employees using Google email", employees5);
    }

    private void RunTask4()
    {
        int[] numbers = { 121, 75, 81, 999, 44, 305, 12, 88, 17 };

        Console.WriteLine("Original array:");
        Console.WriteLine(string.Join(", ", numbers));
        Console.WriteLine();

        IEnumerable<int> ascending = _numberQueries.SortByDigitSumAscending(numbers);
        IEnumerable<int> descending = _numberQueries.SortByDigitSumDescending(numbers);

        Console.WriteLine("Sorted by digit sum ascending:");
        Console.WriteLine(string.Join(", ", ascending));
        Console.WriteLine();

        Console.WriteLine("Sorted by digit sum descending:");
        Console.WriteLine(string.Join(", ", descending));
        Console.WriteLine();
    }

    private void RunTask5()
    {
        List<Laptop> laptops = _laptopSeeder.GetLaptops();

        Console.WriteLine($"Laptop count: {_laptopQueries.GetLaptopCount(laptops)}");
        Console.WriteLine($"Laptop count with price > 1500: {_laptopQueries.GetLaptopCountWithPriceMoreThan(laptops, 1500)}");
        Console.WriteLine($"Laptop count with price in range 900..1800: {_laptopQueries.GetLaptopCountWithPriceInRange(laptops, 900, 1800)}");
        Console.WriteLine($"Laptop count by manufacturer Apple: {_laptopQueries.GetLaptopCountByManufacturer(laptops, "Apple")}");
        Console.WriteLine();

        Printer.PrintSingle("Laptop with min price", _laptopQueries.GetLaptopWithMinPrice(laptops));
        Printer.PrintSingle("Laptop with max price", _laptopQueries.GetLaptopWithMaxPrice(laptops));
        Printer.PrintSingle("Laptop with min CPU frequency", _laptopQueries.GetLaptopWithMinCpuFrequency(laptops));
        Printer.PrintSingle("Newest laptop model", _laptopQueries.GetNewestLaptopModel(laptops));

        Console.WriteLine($"Average laptop price: {_laptopQueries.GetAveragePrice(laptops)}");
        Console.WriteLine();

        Printer.PrintCollection("Manufacturer statistics", _laptopQueries.GetManufacturerStatistics(laptops));
        Printer.PrintCollection("Model statistics", _laptopQueries.GetModelStatistics(laptops));
        Printer.PrintCollection("Year statistics", _laptopQueries.GetYearStatistics(laptops));
    }
}