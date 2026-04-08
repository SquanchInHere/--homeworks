using EnterLinq.Models;
using EnterLinq.Records;

namespace EnterLinq.Data;

public class CompanySeeder
{
    public List<Company> GetCompanies()
    {
        return new List<Company>
        {
            new Company(
                "White Food Market",
                DateTime.Today.AddYears(-5),
                "Food",
                "John White",
                180,
                "London, Baker Street 12",
                new List<ManagementEmployees>
                {
                    new("John White", "Director", "+44-123-456-23", "john.white@google.com", 7000),
                    new("Emma Stone", "Manager", "+44-555-123-45", "emma.stone@gmail.com", 4000),
                    new("Michael Green", "HR Manager", "+44-222-233-23", "m.green@yahoo.com", 3500)
                }),

            new Company(
                "Black IT Solutions",
                DateTime.Today.AddYears(-3).AddDays(-123),
                "IT",
                "Andrew Black",
                320,
                "Manchester, King Street 5",
                new List<ManagementEmployees>
                {
                    new("Andrew Black", "Director", "+44-777-123-23", "a.black@google.com", 9000),
                    new("Olivia Miles", "Manager", "+44-888-555-11", "olivia@outlook.com", 4500),
                    new("Robert Fox", "CTO", "+44-333-223-23", "r.fox@company.com", 8000)
                }),

            new Company(
                "Prime Marketing Group",
                DateTime.Today.AddYears(-1).AddDays(-123),
                "Marketing",
                "Sarah White",
                95,
                "London, Oxford Street 77",
                new List<ManagementEmployees>
                {
                    new("Sarah White", "Director", "+44-100-230-00", "s.white@google.com", 6500),
                    new("Nina Black", "Manager", "+44-234-567-23", "n.black@gmail.com", 3900)
                }),

            new Company(
                "Future Food Logistics",
                DateTime.Today.AddYears(-8),
                "Logistics",
                "James Brown",
                240,
                "Liverpool, River Road 17",
                new List<ManagementEmployees>
                {
                    new("James Brown", "Director", "+44-990-111-23", "j.brown@google.com", 6800),
                    new("Helen Brook", "Manager", "+44-123-000-88", "helen@company.com", 4100)
                }),

            new Company(
                "White Star Marketing",
                DateTime.Today.AddDays(-123),
                "Marketing",
                "David Black",
                130,
                "London, Piccadilly 9",
                new List<ManagementEmployees>
                {
                    new("David Black", "Director", "+44-777-888-23", "d.black@google.com", 7200),
                    new("Kate Moore", "Manager", "+44-444-555-66", "kate.moore@gmail.com", 4200),
                    new("Alan Kent", "Sales Manager", "+44-231-222-23", "alan.kent@company.com", 3900)
                })
        };
    }
}