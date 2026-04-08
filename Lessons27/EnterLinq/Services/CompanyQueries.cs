using EnterLinq.Models;
using EnterLinq.Records;

namespace EnterLinq.Services;

public class CompanyQueries
{
    public IEnumerable<Company> GetAllCompaniesSortedByEmployees(List<Company> companies)
    {
        var query =
            from company in companies
            orderby company.EmployeesCount
            select company;

        return query;
    }

    public IEnumerable<Company> GetCompaniesWithFoodInName(List<Company> companies)
    {
        var query =
            from company in companies
            where company.Name.Contains("Food", StringComparison.OrdinalIgnoreCase)
            select company;

        return query;
    }

    public IEnumerable<Company> GetMarketingCompanies(List<Company> companies)
    {
        var query =
            from company in companies
            where company.BusinessProfile.Equals("Marketing", StringComparison.OrdinalIgnoreCase)
            select company;

        return query;
    }

    public IEnumerable<Company> GetMarketingOrItCompanies(List<Company> companies)
    {
        var query =
            from company in companies
            where company.BusinessProfile.Equals("Marketing", StringComparison.OrdinalIgnoreCase)
               || company.BusinessProfile.Equals("IT", StringComparison.OrdinalIgnoreCase)
            select company;

        return query;
    }

    public IEnumerable<Company> GetCompaniesWithEmployeesMoreThan100(List<Company> companies)
    {
        var query =
            from company in companies
            where company.EmployeesCount > 100
            select company;

        return query;
    }

    public IEnumerable<Company> GetCompaniesWithEmployeesBetween100And300(List<Company> companies)
    {
        var query =
            from company in companies
            where company.EmployeesCount >= 100 && company.EmployeesCount <= 300
            select company;

        return query;
    }

    public IEnumerable<Company> GetCompaniesInLondon(List<Company> companies)
    {
        var query =
            from company in companies
            where company.Address.Contains("London", StringComparison.OrdinalIgnoreCase)
            select company;

        return query;
    }

    public IEnumerable<Company> GetCompaniesWithDirectorSurnameWhite(List<Company> companies)
    {
        var query =
            from company in companies
            where company.DirectorFullName.Split(' ').Last()
                .Equals("White", StringComparison.OrdinalIgnoreCase)
            select company;

        return query;
    }

    public IEnumerable<Company> GetCompaniesFoundedMoreThanTwoYearsAgo(List<Company> companies)
    {
        DateTime limitDate = DateTime.Today.AddYears(-2);

        var query =
            from company in companies
            where company.FoundationDate < limitDate
            select company;

        return query;
    }

    public IEnumerable<Company> GetCompaniesFounded123DaysAgo(List<Company> companies)
    {
        var query =
            from company in companies
            let daysPassed = (DateTime.Today - company.FoundationDate.Date).Days
            where daysPassed == 123
            select company;

        return query;
    }

    public IEnumerable<Company> GetCompaniesWithDirectorBlackAndWhiteInName(List<Company> companies)
    {
        var query =
            from company in companies
            where company.DirectorFullName.Split(' ').Last()
                    .Equals("Black", StringComparison.OrdinalIgnoreCase)
               && company.Name.Contains("White", StringComparison.OrdinalIgnoreCase)
            select company;

        return query;
    }

    public IEnumerable<Company> GetTop3ByEmployees(List<Company> companies)
    {
        var query =
            (from company in companies
             orderby company.EmployeesCount descending
             select company)
            .Take(3);

        return query;
    }

    public IEnumerable<Company> GetBottom2ByEmployees(List<Company> companies)
    {
        var query =
            (from company in companies
             orderby company.EmployeesCount
             select company)
            .Take(2);

        return query;
    }

    public IEnumerable<ManagementEmployees> GetAllEmployeesOfCompany(List<Company> companies, string companyName)
    {
        var query =
            from company in companies
            where company.Name.Equals(companyName, StringComparison.OrdinalIgnoreCase)
            from employee in company.ManagementEmployees
            select employee;

        return query;
    }

    public IEnumerable<ManagementEmployees> GetEmployeesOfCompanyWithSalaryMoreThan(
        List<Company> companies,
        string companyName,
        decimal salary)
    {
        var query =
            from company in companies
            where company.Name.Equals(companyName, StringComparison.OrdinalIgnoreCase)
            from employee in company.ManagementEmployees
            where employee.Salary > salary
            select employee;

        return query;
    }

    public IEnumerable<ManagementEmployees> GetAllManagersFromAllCompanies(List<Company> companies)
    {
        var query =
            from company in companies
            from employee in company.ManagementEmployees
            where employee.Position.Contains("Manager", StringComparison.OrdinalIgnoreCase)
            select employee;

        return query;
    }

    public IEnumerable<ManagementEmployees> GetEmployeesWith23InPhone(List<Company> companies)
    {
        var query =
            from company in companies
            from employee in company.ManagementEmployees
            where employee.Phone.Contains("23")
            select employee;

        return query;
    }

    public IEnumerable<ManagementEmployees> GetEmployeesUsingGoogleEmail(List<Company> companies)
    {
        var query =
            from company in companies
            from employee in company.ManagementEmployees
            where employee.Email.Contains("google", StringComparison.OrdinalIgnoreCase)
               || employee.Email.Contains("gmail", StringComparison.OrdinalIgnoreCase)
            select employee;

        return query;
    }
}