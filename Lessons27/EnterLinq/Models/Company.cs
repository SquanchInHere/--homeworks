using EnterLinq.Records;
using System.Text;

namespace EnterLinq.Models;

public class Company
{
    public string Name { get; set; }
    public DateTime FoundationDate { get; set; }
    public string BusinessProfile { get; set; }
    public string DirectorFullName { get; set; }
    public int EmployeesCount { get; set; }
    public string Address { get; set; }
    public List<ManagementEmployees> ManagementEmployees { get; set; }

    public Company(
        string name,
        DateTime foundationDate,
        string businessProfile,
        string directorFullName,
        int employeesCount,
        string address,
        List<ManagementEmployees> managementEmployees)
    {
        Name = name;
        FoundationDate = foundationDate;
        BusinessProfile = businessProfile;
        DirectorFullName = directorFullName;
        EmployeesCount = employeesCount;
        Address = address;
        ManagementEmployees = managementEmployees;
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine($"Name: {Name}");
        builder.AppendLine($"Foundation date: {FoundationDate:dd.MM.yyyy}");
        builder.AppendLine($"Business profile: {BusinessProfile}");
        builder.AppendLine($"Director: {DirectorFullName}");
        builder.AppendLine($"Employees count: {EmployeesCount}");
        builder.AppendLine($"Address: {Address}");
        return builder.ToString();
    }
}