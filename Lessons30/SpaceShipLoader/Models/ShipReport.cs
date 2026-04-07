using SpaceShipLoader.Enums;

namespace SpaceShipLoader.Models;

public class ShipReport
{
    public DestinationType Destination { get; set; }
    public int FuelPercentage { get; set; }
    public int OxygenPercentage { get; set; }
    public int ReactorPercentage { get; set; }
    public string LaunchStatus { get; set; }

    public ShipReport(
        DestinationType destination,
        int fuelPercentage,
        int oxygenPercentage,
        int reactorPercentage,
        string launchStatus)
    {
        Destination = destination;
        FuelPercentage = fuelPercentage;
        OxygenPercentage = oxygenPercentage;
        ReactorPercentage = reactorPercentage;
        LaunchStatus = launchStatus;
    }
}
