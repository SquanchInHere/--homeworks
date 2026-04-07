using SpaceShipLoader.Enums;

namespace SpaceShipLoader.Providers;

public class SpaceShipVisualProvider
{
    public string GetDestinationDisplayName(DestinationType destination)
    {
        switch (destination)
        {
            case DestinationType.Mars:
                return "🔴 Mars";

            case DestinationType.Moon:
                return "🌕 Moon";

            case DestinationType.AsteroidBelt:
                return "☄ Asteroid Belt";

            case DestinationType.Europa:
                return "🧊 Europa";

            case DestinationType.Titan:
                return "🪐 Titan";

            default:
                return "Unknown";
        }
    }

    public string GetCrewStatusDisplayName(CrewStatusType status)
    {
        switch (status)
        {
            case CrewStatusType.Ready:
                return "[green]✔ Ready[/]";

            case CrewStatusType.Busy:
                return "[yellow]● Busy[/]";

            case CrewStatusType.Offline:
                return "[red]✖ Offline[/]";

            default:
                return "[grey]Unknown[/]";
        }
    }

    public string GetSystemStateText(bool state)
    {
        if (state)
        {
            return "[green]ONLINE[/]";
        }

        return "[red]OFFLINE[/]";
    }

    public string GetLaunchStatusColor(string launchStatus)
    {
        switch (launchStatus)
        {
            case "Ready for launch":
                return "green";
            case "Attention required":
                return "yellow";
            default:
                return "red";
        }
    }
}
