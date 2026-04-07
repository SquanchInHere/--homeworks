using SpaceShipLoader.Enums;
using SpaceShipLoader.Interfaces;
using SpaceShipLoader.Models;
using SpaceShipLoader.Providers;
using Spectre.Console;

namespace SpaceShipLoader.Services;

public class SpaceShipRenderer : ISpaceShipRenderer
{
    private readonly SpaceShipVisualProvider _visualProvider;

    public SpaceShipRenderer()
    {
        _visualProvider = new SpaceShipVisualProvider();
    }

    public void ShowHeader()
    {
        AnsiConsole.Clear();

        AnsiConsole.Write(
            new FigletText("StarShip")
                .Centered()
                .Color(Color.Aqua));

        AnsiConsole.Write(
            new Spectre.Console.Rule("[yellow]Spacecraft System Loader[/]")
                .Centered()
                .RuleStyle("grey"));

        AnsiConsole.WriteLine();
    }

    public void ShowInitializationStatus()
    {
        AnsiConsole.Status()
            .Spinner(Spinner.Known.Earth)
            .SpinnerStyle(Style.Parse("green"))
            .Start("Initializing systems...", context =>
            {
                Thread.Sleep(1600);
            });

        AnsiConsole.MarkupLine("[green]✔ Core modules initialized.[/]");
        AnsiConsole.WriteLine();
    }

    public void ShowLoadingProgress(List<ShipLoadTask> tasks)
    {
        AnsiConsole.Write(new Spectre.Console.Rule("[yellow]Loading Core Resources[/]").Centered());

        AnsiConsole.Progress()
            .AutoClear(false)
            .HideCompleted(false)
            .Columns(new ProgressColumn[]
            {
                new TaskDescriptionColumn(),
                new ProgressBarColumn(),
                new PercentageColumn(),
                new RemainingTimeColumn(),
                new SpinnerColumn()
            })
            .Start(context =>
            {
                List<ProgressTask> progressTasks = new List<ProgressTask>();

                foreach (ShipLoadTask task in tasks)
                {
                    ProgressTask progressTask = context.AddTask(
                        $"[cyan]{task.Name}[/]",
                        maxValue: task.MaxValue);

                    progressTasks.Add(progressTask);
                }

                while (!context.IsFinished)
                {
                    for (int i = 0; i < tasks.Count; i++)
                    {
                        if (!progressTasks[i].IsFinished)
                        {
                            progressTasks[i].Increment(tasks[i].Step);
                        }
                    }

                    Thread.Sleep(90);
                }
            });

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[bold green]✔ All resources loaded successfully.[/]");
        AnsiConsole.WriteLine();
    }

    public void ShowMainLayout(List<CrewMember> crew, SystemStatus status)
    {
        Table crewTable = BuildCrewTable(crew);
        Panel systemsPanel = BuildSystemsPanel(status);

        Layout layout = new Layout("Root");
        layout.SplitColumns(
            new Layout("Left"),
            new Layout("Right"));

        layout["Left"].Update(
            new Panel(crewTable)
                .Header("[bold white]Crew Manifest[/]")
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Cyan1)));

        layout["Right"].Update(systemsPanel);

        AnsiConsole.Write(layout);
        AnsiConsole.WriteLine();
    }

    public DestinationType AskDestination()
    {
        string selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]🧭 Where are we heading?[/]")
                .PageSize(10)
                .HighlightStyle(new Style(Color.GreenYellow, null, Decoration.Bold))
                .AddChoices(
                    "🔴 Mars",
                    "🌕 Moon",
                    "☄ Asteroid Belt",
                    "🧊 Europa",
                    "🪐 Titan"));

        switch (selected)
        {
            case "🔴 Mars":
                return DestinationType.Mars;

            case "🌕 Moon":
                return DestinationType.Moon;

            case "☄ Asteroid Belt":
                return DestinationType.AsteroidBelt;

            case "🧊 Europa":
                return DestinationType.Europa;

            case "🪐 Titan":
                return DestinationType.Titan;

            default:
                throw new InvalidOperationException("Unknown destination.");
        }
    }

    public void ShowFinalReport(ShipReport report)
    {
        string destinationName = _visualProvider.GetDestinationDisplayName(report.Destination);
        string launchStatusColor = _visualProvider.GetLaunchStatusColor(report.LaunchStatus);

        Table summaryTable = new Table();
        summaryTable.Border = TableBorder.Rounded;
        summaryTable.BorderColor(Color.Grey);
        summaryTable.AddColumn("[bold yellow]Metric[/]");
        summaryTable.AddColumn("[bold green]Value[/]");

        summaryTable.AddRow("🧭 Destination", destinationName);
        summaryTable.AddRow("⛽ Fuel", $"[green]{report.FuelPercentage}%[/]");
        summaryTable.AddRow("🫁 Oxygen", $"[green]{report.OxygenPercentage}%[/]");
        summaryTable.AddRow("⚛ Reactor", $"[green]{report.ReactorPercentage}%[/]");
        summaryTable.AddRow("🚀 Launch Status", $"[{launchStatusColor}]{report.LaunchStatus}[/]");

        Panel panel = new Panel(summaryTable);
        panel.Header = new PanelHeader("[bold yellow]Final Mission Report[/]");
        panel.Border = BoxBorder.Double;
        panel.BorderStyle = new Style(Color.Orange1);
        panel.Padding = new Padding(1, 1, 1, 1);

        AnsiConsole.WriteLine();
        AnsiConsole.Write(panel);
        AnsiConsole.WriteLine();

        if (report.LaunchStatus == "Ready for launch")
        {
            AnsiConsole.MarkupLine("[bold green]✔ Spacecraft is ready for departure.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[bold yellow]⚠ Some systems require attention before launch.[/]");
        }
    }

    private Table BuildCrewTable(List<CrewMember> crew)
    {
        Table table = new Table();
        table.Border = TableBorder.Rounded;
        table.BorderColor(Color.Grey);

        table.AddColumn("[bold yellow]Name[/]");
        table.AddColumn("[bold yellow]Role[/]");
        table.AddColumn("[bold yellow]Status[/]");

        foreach (CrewMember crewMember in crew)
        {
            table.AddRow(
                crewMember.Name,
                crewMember.Role,
                _visualProvider.GetCrewStatusDisplayName(crewMember.Status));
        }

        return table;
    }

    private Panel BuildSystemsPanel(SystemStatus status)
    {
        Table systemsTable = new Table();
        systemsTable.Border = TableBorder.Rounded;
        systemsTable.BorderColor(Color.Grey);

        systemsTable.AddColumn("[bold yellow]System[/]");
        systemsTable.AddColumn("[bold yellow]State[/]");

        systemsTable.AddRow("🧭 Navigation", _visualProvider.GetSystemStateText(status.NavigationOnline));
        systemsTable.AddRow("🫁 Life Support", _visualProvider.GetSystemStateText(status.LifeSupportOnline));
        systemsTable.AddRow("⚛ Reactor", _visualProvider.GetSystemStateText(status.ReactorStable));
        systemsTable.AddRow("📡 Communication", _visualProvider.GetSystemStateText(status.CommunicationOnline));

        string summaryText;

        if (status.IsAllSystemsNormal())
        {
            summaryText = "[bold green]All systems operational[/]\n[grey]Ship is stable and ready for departure.[/]";
        }
        else
        {
            summaryText = "[bold yellow]Attention required[/]\n[grey]Some modules need inspection.[/]";
        }

        Rows rows = new Rows(
            systemsTable,
            new Markup(summaryText));

        Panel panel = new Panel(rows);
        panel.Header = new PanelHeader("[bold white]System Report[/]");
        panel.Border = BoxBorder.Double;
        panel.BorderStyle = new Style(Color.HotPink);
        panel.Padding = new Padding(1, 1, 1, 1);
        panel.Expand = true;

        return panel;
    }
}
