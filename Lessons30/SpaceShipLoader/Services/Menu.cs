using SpaceShipLoader.Enums;
using SpaceShipLoader.Interfaces;
using SpaceShipLoader.Models;
using SpaceShipLoader.Providers;

namespace SpaceShipLoader.Services
{
    public class Menu
    {
        private readonly ISpaceShipRenderer _renderer;
        private readonly SpaceShipDataProvider _dataProvider;

        public Menu()
        {
            _renderer = new SpaceShipRenderer();
            _dataProvider = new SpaceShipDataProvider();
        }

        public void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            _renderer.ShowHeader();
            _renderer.ShowInitializationStatus();

            List<ShipLoadTask> loadTasks = _dataProvider.GetLoadTasks();
            _renderer.ShowLoadingProgress(loadTasks);

            List<CrewMember> crew = _dataProvider.GetCrew();
            SystemStatus systemStatus = _dataProvider.GetSystemStatus();

            _renderer.ShowMainLayout(crew, systemStatus);

            DestinationType destination = _renderer.AskDestination();

            ShipReport report = BuildReport(destination, systemStatus);

            _renderer.ShowFinalReport(report);
        }

        private ShipReport BuildReport(DestinationType destination, SystemStatus systemStatus)
        {
            string launchStatus;

            if (systemStatus.IsAllSystemsNormal())
            {
                launchStatus = "Ready for launch";
            }
            else
            {
                launchStatus = "Attention required";
            }

            ShipReport report = new ShipReport(
                destination,
                100,
                100,
                100,
                launchStatus);

            return report;
        }
    }
}
