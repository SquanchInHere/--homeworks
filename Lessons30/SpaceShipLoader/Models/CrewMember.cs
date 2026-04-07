using SpaceShipLoader.Enums;

namespace SpaceShipLoader.Models;

public class CrewMember
{
    public string Name { get; set; }
    public string Role { get; set; }
    public CrewStatusType Status { get; set; }

    public CrewMember(string name, string role, CrewStatusType status)
    {
        Name = name;
        Role = role;
        Status = status;
    }
}
