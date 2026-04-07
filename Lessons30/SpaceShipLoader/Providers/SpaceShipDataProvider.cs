using SpaceShipLoader.Enums;
using SpaceShipLoader.Models;

namespace SpaceShipLoader.Providers;

public class SpaceShipDataProvider
{
    public List<CrewMember> GetCrew()
    {
        List<CrewMember> crew = new List<CrewMember>();

        crew.Add(new CrewMember("Alex Rayne", "Captain", CrewStatusType.Ready));
        crew.Add(new CrewMember("Mila Thorn", "Pilot", CrewStatusType.Ready));
        crew.Add(new CrewMember("Leo Kane", "Engineer", CrewStatusType.Ready));
        crew.Add(new CrewMember("Sarah Voss", "Medic", CrewStatusType.Ready));

        return crew;
    }

    public List<ShipLoadTask> GetLoadTasks()
    {
        List<ShipLoadTask> tasks = new List<ShipLoadTask>();

        tasks.Add(new ShipLoadTask("Fuel", 100, 2.4));
        tasks.Add(new ShipLoadTask("Oxygen", 100, 1.9));
        tasks.Add(new ShipLoadTask("Reactor", 100, 2.1));

        return tasks;
    }

    public SystemStatus GetSystemStatus()
    {
        SystemStatus status = new SystemStatus();
        status.NavigationOnline = true;
        status.LifeSupportOnline = true;
        status.ReactorStable = true;
        status.CommunicationOnline = true;

        return status;
    }

    public List<DestinationType> GetDestinations()
    {
        List<DestinationType> destinations = new List<DestinationType>();

        destinations.Add(DestinationType.Mars);
        destinations.Add(DestinationType.Moon);
        destinations.Add(DestinationType.AsteroidBelt);
        destinations.Add(DestinationType.Europa);
        destinations.Add(DestinationType.Titan);

        return destinations;
    }
}
