using SpaceShipLoader.Enums;
using SpaceShipLoader.Models;

namespace SpaceShipLoader.Interfaces;

public interface ISpaceShipRenderer
{
    void ShowHeader();
    void ShowInitializationStatus();
    void ShowLoadingProgress(List<ShipLoadTask> tasks);
    void ShowMainLayout(List<CrewMember> crew, SystemStatus status);
    DestinationType AskDestination();
    void ShowFinalReport(ShipReport report);
}
